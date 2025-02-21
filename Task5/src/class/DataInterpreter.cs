using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;

namespace Task5
{
    public class DataInterpreter
    {
        private Dictionary<string, DeviceData> _configuration;
        public DataInterpreter(Dictionary<string, DeviceData> configuration)
        {
            _configuration = configuration;
        }

        public DataTable InterpretData(DataTable rawData)
        {
            var interpretedData = new DataTable();
            interpretedData.Columns.Add("Time", typeof(double));
            interpretedData.Columns.Add("DeviceID", typeof(string));

            foreach (var device in _configuration.Values)
            {
                foreach (var dataItem in device.Data)
                {
                    if (!interpretedData.Columns.Contains(dataItem.Variable))
                    {
                        interpretedData.Columns.Add(dataItem.Variable, typeof(double));
                    }
                }
            }

            foreach (DataRow row in rawData.Rows)
            {
                var time = row[0].ToString();
                var deviceId = row[1].ToString();

                if (_configuration.ContainsKey(deviceId))
                {
                    var deviceData = _configuration[deviceId];
                    var newRow = interpretedData.NewRow();
                    newRow["Time"] = double.TryParse(time, out double timeValue) ? timeValue : 0; 
                    newRow["DeviceID"] = deviceId;

                    foreach (var dataItem in deviceData.Data)
                    {
                        byte[] bytes = new byte[dataItem.Byte.Count];

                        for (int i = 0; i < dataItem.Byte.Count; i++)
                        {
                            var cellValue = row[dataItem.Byte[i] + 1].ToString();
                            byte.TryParse(cellValue, out bytes[i]);
                        }

                        double value = ConvertBytesToValue(bytes, dataItem.Format, dataItem.ByteOrder, dataItem.Coefficient, dataItem.Offset);
                        newRow[dataItem.Variable] = value;
                    }

                    interpretedData.Rows.Add(newRow);
                }
            }

            return interpretedData;
        }

        private double ConvertBytesToValue(byte[] bytes, string format, string byteOrder, double? coefficient, double? offset)
        {
            if (bytes == null || bytes.Length == 0)
                return 0;

            if (byteOrder == "LittleEndian")
            {
                Array.Reverse(bytes);
            }

            double value = 0;
            switch (format)
            {
                case "Uint8":
                    value = bytes[0];
                    break;
                case "Uint16":
                    value = BitConverter.ToUInt16(bytes, 0);
                    break;
                case "Uint32":
                    value = BitConverter.ToUInt32(bytes, 0);
                    break;
                case "Int16":
                    value = BitConverter.ToInt16(bytes, 0);
                    break;
                case "Int32":
                    value = BitConverter.ToInt32(bytes, 0);
                    break;
                case "Float":
                    value = BitConverter.ToSingle(bytes, 0);
                    break;
                case "Double":
                    value = BitConverter.ToDouble(bytes, 0);
                    break;
                default:
                    return 0;
            }
            Console.WriteLine($"Преобразование: формат = {format}, значение = {value}, коэффициент = {coefficient}, смещение = {offset}");

            if (coefficient.HasValue)
                value *= coefficient.Value;
            if (offset.HasValue)
                value += offset.Value;

            return value;
        }
    }
}

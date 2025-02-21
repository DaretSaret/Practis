using OfficeOpenXml;
using System.Data;
using System.IO;

namespace Task5
{
    public class ExcelExporter
    {
        public void ExportDataTableToExcel(DataTable dataTable, string filePath)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Data");
                worksheet.Cells["A1"].LoadFromDataTable(dataTable, true);
                package.SaveAs(new FileInfo(filePath));
            }
        }
    }
}

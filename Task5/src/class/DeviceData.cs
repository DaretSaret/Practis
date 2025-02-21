using System.Text.Json.Serialization;
using System.Collections.Generic;

namespace Task5
{

    public class DeviceData
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("data")]
        public List<DataItem> Data { get; set; }
    }
}

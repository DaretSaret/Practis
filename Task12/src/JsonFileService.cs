using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Json;

namespace Task12
{
    public class JsonFileService : IFileService
    {
        public List<TaskItem> Open(string filename)
        {
            using (FileStream fs = new FileStream(filename, FileMode.OpenOrCreate))
            {
                var serializer = new DataContractJsonSerializer(typeof(List<TaskItem>));
                return (List<TaskItem>)serializer.ReadObject(fs);
            }
        }

        public void Save(string filename, List<TaskItem> tasks)
        {
            using (FileStream fs = new FileStream(filename, FileMode.Create))
            {
                var serializer = new DataContractJsonSerializer(typeof(List<TaskItem>));
                serializer.WriteObject(fs, tasks);
            }
        }
    }
}

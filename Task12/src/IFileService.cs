using System.Collections.Generic;

namespace Task12
{
    public interface IFileService
    {
        List<TaskItem> Open(string filename);
        void Save(string filename, List<TaskItem> tasks);
    }
}

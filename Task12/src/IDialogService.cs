using Microsoft.Win32;

namespace Task12
{
    public interface IDialogService
    {
        string FilePath { get; set; }
        bool OpenFileDialog();
        bool SaveFileDialog();
        void ShowMessage(string message);
    }
}

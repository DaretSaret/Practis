using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;

namespace Task12
{
    public class ApplicationViewModel : INotifyPropertyChanged
    {
        private TaskItem selectedTask;
        private readonly IFileService fileService;
        private readonly IDialogService dialogService;
        private string filter;

        public ObservableCollection<TaskItem> Tasks { get; set; }
        public ObservableCollection<TaskItem> FilteredTasks { get; set; }

        public ICommand AddCommand { get; }
        public ICommand RemoveCommand { get; }
        public ICommand SaveCommand { get; }
        public ICommand OpenCommand { get; }
        public ICommand FilterCommand { get; }

        public TaskItem SelectedTask
        {
            get { return selectedTask; }
            set
            {
                selectedTask = value;
                OnPropertyChanged();
            }
        }

        public string Filter
        {
            get { return filter; }
            set
            {
                filter = value;
                OnPropertyChanged();
                ApplyFilter();
            }
        }

        public ApplicationViewModel(IDialogService dialogService, IFileService fileService)
        {
            this.dialogService = dialogService;
            this.fileService = fileService;

            Tasks = new ObservableCollection<TaskItem>();
            FilteredTasks = new ObservableCollection<TaskItem>();

            AddCommand = new RelayCommand(_ => AddTask());
            RemoveCommand = new RelayCommand(_ => RemoveTask(), _ => SelectedTask != null);
            SaveCommand = new RelayCommand(_ => SaveTasks());
            OpenCommand = new RelayCommand(_ => OpenTasks());
            FilterCommand = new RelayCommand(_ => ApplyFilter());
        }

        private void AddTask()
        {
            var newTask = new TaskItem { Title = "Новая задача", IsCompleted = false };
            Tasks.Add(newTask);
            ApplyFilter();
            SelectedTask = newTask;
        }

        private void RemoveTask()
        {
            if (SelectedTask != null)
            {
                Tasks.Remove(SelectedTask);
                ApplyFilter();
            }
        }

        private void SaveTasks()
        {
            if (dialogService.SaveFileDialog())
            {
                fileService.Save(dialogService.FilePath, Tasks.ToList());
                dialogService.ShowMessage("Файл сохранен");
            }
        }

        private void OpenTasks()
        {
            if (dialogService.OpenFileDialog())
            {
                var tasks = fileService.Open(dialogService.FilePath);
                Tasks.Clear();
                foreach (var task in tasks)
                {
                    Tasks.Add(task);
                }
                ApplyFilter();
                dialogService.ShowMessage("Файл открыт");
            }
        }

        private void ApplyFilter()
        {
            FilteredTasks.Clear();

            IEnumerable<TaskItem> filtered;
            if (string.IsNullOrEmpty(Filter) || Filter == "Все")
            {
                filtered = Tasks;
            }
            else if (Filter == "Выполненные")
            {
                filtered = Tasks.Where(t => t.IsCompleted);
                var completedTasks = filtered.ToList();
            }
            else if (Filter == "Не выполненные")
            {
                filtered = Tasks.Where(t => !t.IsCompleted);
                var notCompletedTasks = filtered.ToList();
            }
            else
            {
                filtered = Enumerable.Empty<TaskItem>();
            }
            foreach (var task in filtered)
            {
                FilteredTasks.Add(task);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

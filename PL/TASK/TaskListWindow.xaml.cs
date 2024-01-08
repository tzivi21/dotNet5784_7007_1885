using BO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PL
{
    /// <summary>
    /// Interaction logic for TaskListWindow.xaml
    /// </summary>
    public partial class TaskListWindow : Window
    {
        static readonly BlApi.IBl s_bl = BlApi.Factory.Get();
        public TaskListWindow()
        {
            InitializeComponent();
            var temp1 = s_bl?.Task.ReadAll();
            var temp = temp1?.Select(task => new TaskInList
            {
                Id = task.Id,
                Description = task.Description,
                Alias = task.Alias,
                Status = task.Status
            }).ToList();
            TaskList = temp == null ? new() : new(temp);

        }
        public static readonly DependencyProperty TaskListProperty =
           DependencyProperty.Register("TaskList", typeof(ObservableCollection<BO.TaskInList>), typeof(TaskListWindow), new PropertyMetadata(null));
        public ObservableCollection<BO.TaskInList> TaskList
        {
            get { return (ObservableCollection<BO.TaskInList>)GetValue(TaskListProperty); }
            set { SetValue(TaskListProperty, value); }
        }

       


        private void InitializeComponent()
        {
        }
    }
}

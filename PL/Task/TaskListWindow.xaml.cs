using PL.Engineer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace PL.Task
{
    /// <summary>
    /// Interaction logic for TaskListWindow.xaml
    /// </summary>
    public partial class TaskListWindow : Window
    {
        static readonly BlApi.IBl s_bl = BlApi.Factory.Get();
        public BO.Status? Status { get; set; } = BO.Status.None;
        public int EngineerId
        {
            get { return (int)GetValue(EngineerIdProperty); }
            set { SetValue(EngineerIdProperty, value); }
        }
        public IEnumerable<BO.TaskInList>? TasksList
        {
            get { return (IEnumerable<BO.TaskInList>)GetValue(TaskListProperty); }
            set { SetValue(TaskListProperty, value); }
        }
        public string? Alias
        {
            get { return (string)GetValue(AliasProperty); }
            set { SetValue(AliasProperty, value); }
        }
        public static readonly DependencyProperty EngineerIdProperty =
                DependencyProperty.Register("EngineerId", typeof(int), typeof(TaskListWindow), new PropertyMetadata(null));

        public static readonly DependencyProperty AliasProperty =
                DependencyProperty.Register("Alias", typeof(string), typeof(TaskListWindow), new PropertyMetadata(null));

        public static readonly DependencyProperty TaskListProperty =
           DependencyProperty.Register("TasksList", typeof(IEnumerable<BO.TaskInList>), typeof(TaskListWindow), new PropertyMetadata(null));
        private void cbStatusSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var TasksList_temp = (Status == BO.Status.None) ?
                s_bl?.Task.ReadAll()! : s_bl?.Task.ReadAll(item => item.Status == Status!)!;
            TasksList=Convert_tasks_to_taskinlist(TasksList_temp);
        }

        
        private void Handle_Select_Alias(object sender, RoutedEventArgs e)
        {
            if (Alias != "")
            {
                TasksList=Convert_tasks_to_taskinlist(s_bl?.Task.ReadAll(task=>task.Alias==Alias));
            }
        }
        public void Handle_Add_Click(object sender, RoutedEventArgs e)
        {
            TaskWindow addTaskWindow = new TaskWindow();
            addTaskWindow.Closed += AddTaskWindow_Closed; // Subscribe to the Closed event
            addTaskWindow.ShowDialog();
        }
        private IEnumerable<BO.TaskInList>? Convert_tasks_to_taskinlist(IEnumerable<BO.Task>? tasks)
        {
            if (tasks != null)
            {
                return tasks.Select(t =>
                {
                    return new BO.TaskInList
                    {
                        Id = t.Id,
                        Description = t.Description,
                        Alias = t.Alias,
                        Status = t.Status
                    };
                });
            }
            return null;
            

        }
        private void TasksList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

            // Get the selected engineer item
            BO.TaskInList? selectedTask = (sender as ListView)?.SelectedItem as BO.TaskInList ?? null;
            if (selectedTask != null)
            {
                TaskWindow addTaskWindow = new TaskWindow(selectedTask.Id);
                addTaskWindow.Closed += AddTaskWindow_Closed; // Subscribe to the Closed event
                addTaskWindow.ShowDialog();
            }
        }
        private void AddTaskWindow_Closed(object sender, EventArgs e)
        {
            TasksList =Convert_tasks_to_taskinlist( s_bl?.Task.ReadAll());
        }
        public TaskListWindow(int id=0)
        {
            InitializeComponent();
            EngineerId = id;
            if (EngineerId!= 0)
            {
                TasksList = Convert_tasks_to_taskinlist(s_bl?.Task.ReadAll(t =>
                {
                    if (t.Engineer == null)
                    {
                        return false;
                    }
                    return t.Engineer.Id== EngineerId;
                }));
            }
            TasksList = Convert_tasks_to_taskinlist(s_bl?.Task.ReadAll());

        }
    }
}

using PL.Engineer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for TaskWindow.xaml
    /// </summary>
    public partial class TaskWindow : Window
    {
        public PageMode Mode { get; private set; }


        static readonly BlApi.IBl s_bl = BlApi.Factory.Get();
        public BO.Task? CurrentTask
        {
            get { return (BO.Task)GetValue(CurrentTaskProperty); }
            set { SetValue(CurrentTaskProperty, value); }
        }
        public ObservableCollection<BO.TaskInList> Dependencies
        {
            get { return (ObservableCollection<BO.TaskInList>)GetValue(DependenciesProperty); }
            set { SetValue(DependenciesProperty, value); }
        }
        public static readonly DependencyProperty DependenciesProperty =
                DependencyProperty.Register("Dependencies", typeof(ObservableCollection<BO.TaskInList>), typeof(TaskWindow), new PropertyMetadata(null));

        public static readonly DependencyProperty CurrentTaskProperty =
                DependencyProperty.Register("CurrentTask", typeof(BO.Task), typeof(TaskWindow), new PropertyMetadata(null));
        public IEnumerable<BO.TaskInList> AllDependencies
        {
            get { return (IEnumerable<BO.TaskInList>)GetValue(AllDependenciesProperty); }
            set { SetValue(AllDependenciesProperty, value); }
        }

        public static readonly DependencyProperty AllDependenciesProperty =
                DependencyProperty.Register("AllDependencies", typeof(IEnumerable<BO.TaskInList>), typeof(TaskWindow), new PropertyMetadata(null));
        public IEnumerable<int> EngineersIds
        {
            get { return (IEnumerable<int>)GetValue(EngineersIdsProperty); }
            set { SetValue(EngineersIdsProperty, value); }
        }
        public static readonly DependencyProperty EngineersIdsProperty =
                DependencyProperty.Register("EngineersIds", typeof(IEnumerable<int>), typeof(TaskWindow), new PropertyMetadata(null));
        private void AddUpdateButton_Click(object sender, RoutedEventArgs e)
        {
            if (Mode == PageMode.Add)
            {
                try
                {
                    CurrentTask!.Engineer!.Name = s_bl.Engineer.Read((int)CurrentTask.Engineer.Id!)?.Name??null;
                    s_bl.Task.Create(CurrentTask!);
                    MessageBox.Show("Add action was successful.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error:  {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }

            }
            else
            {
                try
                {
                    s_bl.Task.Update(CurrentTask!);
                    MessageBox.Show("Update action was successful.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    Close();

                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
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
        private void ComboBoxDependencies_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Ensure the selected item is not null and is of the expected type
            if (sender is ComboBox comboBox && comboBox.SelectedItem is BO.TaskInList selectedItem)
            {
                if(selectedItem.Id == CurrentTask.Id)
                {
                    MessageBox.Show("Error:can't had self dependency", "Error", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }
                if (CurrentTask!.Dependencies!.Find(t=>t.Id == selectedItem.Id) == null)
                {
                    Dependencies.Add(selectedItem);
                    // Update the CurrentTask.Dependencies list as well
                    CurrentTask.Dependencies = Dependencies.ToList();

                }
            }
        }

        public TaskWindow(int id=0)
        {
            InitializeComponent();
            EngineersIds=s_bl.Engineer.ReadAll().Select(x => x.Id);
            AllDependencies =Convert_tasks_to_taskinlist( s_bl.Task.ReadAll());
            if (id == 0)
            {
                Mode = PageMode.Add;
                // Create a new engineer object with default values for each property
                CurrentTask = new BO.Task();
                CurrentTask.Id = id;
                if (CurrentTask.Dependencies != null)
                    Dependencies = new ObservableCollection<BO.TaskInList>(CurrentTask.Dependencies);
                else
                    Dependencies = new ObservableCollection<BO.TaskInList>();
            }
            else
            {
                Mode = PageMode.Update;
                try
                {
                    // Fetch the existing engineer object from the BL by id
                    CurrentTask = s_bl.Task.Read(id);
                    if (CurrentTask.Dependencies != null)
                        Dependencies = new ObservableCollection<BO.TaskInList>(CurrentTask.Dependencies);
                    else
                        Dependencies = new ObservableCollection<BO.TaskInList>();
                }
                catch (Exception ex)
                {
                    // Handle exceptions (e.g., engineer not found)
                    MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}

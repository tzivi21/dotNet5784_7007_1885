using PL.Engineer;
using PL.Task;
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

namespace PL.Administrator
{
    /// <summary>
    /// Interaction logic for AdministratorMainWindow.xaml
    /// </summary>
    public partial class AdministratorMainWindow : Window
    {
        static readonly BlApi.IBl s_bl = BlApi.Factory.Get();

        public AdministratorMainWindow()
        {
            InitializeComponent();
            CurrentDateTime= DateTime.Now;
        }
        public DateTime CurrentDateTime
        {
            get { return (DateTime)GetValue(CurrentTimeProperty); }
            set { SetValue(CurrentTimeProperty, value); }
        }
        public static readonly DependencyProperty CurrentTimeProperty =
            DependencyProperty.Register("CurrentDateTime", typeof(DateTime), typeof(AdministratorMainWindow), new PropertyMetadata(null));
        private void Initialization_Click(object sender, RoutedEventArgs e)
        {
            s_bl.InitializeDB();
            MessageBox.Show("the data base was initialized successfully", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
        }
        private void Reset_Click(object sender, RoutedEventArgs e)
        {
            s_bl.ResetDB();
            s_bl.ResetConfiguration();
            MessageBox.Show("the data base reset successfully", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
        }
        private void BtnEngineer_Click(object sender, RoutedEventArgs e)
        {
            new EngineerListWindow().Show();
        }
        private void BtnTasks_Click(object sender, RoutedEventArgs e)
        {
            new TaskListWindow().Show();
        }
        private void AdvanceHourButton_Click(object sender, RoutedEventArgs e)
        {
            CurrentDateTime = CurrentDateTime.AddHours(1);
        }

        private void AdvanceDayButton_Click(object sender, RoutedEventArgs e)
        {
            CurrentDateTime = CurrentDateTime.AddDays(1);
        }
        private void Create_Project_Time_Line(object sender, RoutedEventArgs e)
        {
            try
            {
                s_bl.Milestone.CreateProjectTimeLine();
                MessageBox.Show("the project timeline created successfully", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error:{ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Information);
            }


        }

    }
}

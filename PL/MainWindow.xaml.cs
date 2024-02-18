using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using PL.Engineer;
namespace PL
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static readonly BlApi.IBl s_bl = BlApi.Factory.Get();

        public MainWindow()
        {
            InitializeComponent();
        }
        private void btnEngineer_Click(object sender, RoutedEventArgs e)
        {
            new EngineerListWindow().Show();
        }
        private void initialization_Click(object sender, RoutedEventArgs e)
        {
            s_bl.InitializeDB();
            MessageBox.Show("the data base was initialized successfully", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
        }
        private void reset_Click(object sender, RoutedEventArgs e)
        {
            s_bl.ResetDB();
            MessageBox.Show("the data base reset successfully", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
        }

    }
}
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using PL.Administrator;
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
        private void Initialization_Click(object sender, RoutedEventArgs e)
        {
            s_bl.InitializeDB();
            MessageBox.Show("the data base was initialized successfully", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
        }
        private void Administrator_Click(object sender, RoutedEventArgs e)
        {
            new AdministratorMainWindow().Show();
            Close();
        }
        private void Engineer_click(object sender, RoutedEventArgs e)
        {
            
        }
        private void Reset_Click(object sender, RoutedEventArgs e)
        {
            s_bl.ResetDB();
            MessageBox.Show("the data base reset successfully", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
        }

    }
}
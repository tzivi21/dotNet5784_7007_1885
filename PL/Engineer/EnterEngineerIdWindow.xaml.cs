using System;
using System.Collections.Generic;
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

namespace PL.Engineer
{
    /// <summary>
    /// Interaction logic for EnterEngineerIdWindow.xaml
    /// </summary>
    public partial class EnterEngineerIdWindow : Window
    {
        static readonly BlApi.IBl s_bl = BlApi.Factory.Get();
        public int Id
        {
            get { return (int)GetValue(IdProperty); }
            set { SetValue(IdProperty, value); }
        }
        public static readonly DependencyProperty IdProperty =
                DependencyProperty.Register("Id", typeof(int), typeof(EngineerWindow), new PropertyMetadata(null));
        private void ContinueButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                BO.Engineer? engineer = s_bl.Engineer.Read(Id);
                new SpecificEngineerWindow(Id).Show();
                Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show($"Error:{ex.Message} ", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        public EnterEngineerIdWindow()
        {
            InitializeComponent();
        }
    }
}

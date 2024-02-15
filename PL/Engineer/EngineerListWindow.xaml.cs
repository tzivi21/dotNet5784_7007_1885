using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;


namespace PL.Engineer
{
    /// <summary>
    /// Interaction logic for TaskListWindow.xaml
    /// </summary>
    public partial class EngineerListWindow : Window
    {
        static readonly BlApi.IBl s_bl = BlApi.Factory.Get();
        public DO.EngineerExperience? Experience { get; set; } = DO.EngineerExperience.None;
        public IEnumerable<BO.Engineer> EngineerList
        {
            get { return (IEnumerable<BO.Engineer>)GetValue(EngineerListProperty); }
            set { SetValue(EngineerListProperty, value); }
        }
        private void cbExperienceSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            EngineerList = (Experience == DO.EngineerExperience.None) ?
                s_bl?.Engineer.ReadAll()! : s_bl?.Engineer.ReadAll(item => item.Level == Experience)!;
        }


        public static readonly DependencyProperty EngineerListProperty =
            DependencyProperty.Register("EngineerList", typeof(IEnumerable<BO.Engineer>), typeof(EngineerListWindow), new PropertyMetadata(null));
        public void Handle_Add_Click(object sender, RoutedEventArgs e)
        {
           EngineerWindow addEngineerWindow=new EngineerWindow();
            addEngineerWindow.Closed += AddEngineerWindow_Closed; // Subscribe to the Closed event
            addEngineerWindow.ShowDialog();
        }

        private void EngineersList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
                // Get the selected engineer item
                BO.Engineer? selectedEngineer = (sender as ListView)?.SelectedItem as BO.Engineer?? null;
                if (selectedEngineer != null)
                {
                    EngineerWindow addEngineerWindow = new EngineerWindow(selectedEngineer.Id);
                    addEngineerWindow.Closed += AddEngineerWindow_Closed; // Subscribe to the Closed event
                    addEngineerWindow.ShowDialog();
                }
           

        }
        private void AddEngineerWindow_Closed(object sender, EventArgs e)
        {
            EngineerList = s_bl?.Engineer.ReadAll();
        }

        public EngineerListWindow()
        {
            InitializeComponent();
            EngineerList = s_bl?.Engineer.ReadAll()!;

        }
    }
}

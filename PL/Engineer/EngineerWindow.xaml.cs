
using System.Windows;
namespace PL.Engineer;

/// <summary>
/// Interaction logic for TaskWindow.xaml
/// </summary>
public partial class EngineerWindow : Window
{
    public PageMode Mode { get; private set; }

    static readonly BlApi.IBl s_bl = BlApi.Factory.Get();
    public BO.Engineer? CurrentEngineer
    {
        get { return (BO.Engineer)GetValue(CurrentEngineerProperty); }
        set { SetValue(CurrentEngineerProperty, value); }
    }
    public static readonly DependencyProperty CurrentEngineerProperty =
            DependencyProperty.Register("CurrentEngineer", typeof(BO.Engineer), typeof(EngineerWindow), new PropertyMetadata(null));
    public EngineerWindow(int id=0)
    {
        InitializeComponent();

        // Check if id is default value (e.g., 0)
        if (id == 0)
        {
            Mode = PageMode.Add;
            // Create a new engineer object with default values for each property
            CurrentEngineer = new BO.Engineer();
        }
        else
        {
            Mode = PageMode.Update;
            try
            {
                // Fetch the existing engineer object from the BL by id
                CurrentEngineer = s_bl.Engineer.Read(id);
            }
            catch (Exception ex)
            {
                // Handle exceptions (e.g., engineer not found)
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
    private void AddUpdateButton_Click(object sender, RoutedEventArgs e)
    {
        if (Mode== PageMode.Add)
        {
            try
            {
                s_bl.Engineer.Add(CurrentEngineer!);
                MessageBox.Show("Add action was successful.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                Close();
            }
            catch (Exception ex) {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        else
        {
            try
            {
                s_bl.Engineer.Update(CurrentEngineer!);
                MessageBox.Show("Update action was successful.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}

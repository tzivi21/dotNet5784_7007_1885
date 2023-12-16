
namespace BO;
/// <summary>
/// Engineer Details 
/// <param name="Id">The id of the engineer</param>
/// <param name="Name">the name of the engineer</param>
/// <param name="Email">the email address of the engineer</param>
/// <param name="Level">the experience level of the engineer</param>
///  <param name="Task">id and alias of the current task</param>

/// </summary>
public class Engineer
{
    public int Id { get; init; }
    public string Name { get; set; } = "";
    public string Email { get; set; } = "";
    public DO.EngineerExperience? Level {  get; set; }
    public double Cost { get; set; } = 0;
    public TaskInEngineer? Task { get; set; }

     

}

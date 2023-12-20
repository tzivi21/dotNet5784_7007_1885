
namespace BO;
/// <summary>
/// task details
/// <param name="id">the identifier number of the task</param>
/// <param name="Description">a string with the description of the task</param>
/// <param name="Alias">the nickname of the task</param>
/// <param name="CreatedAt">the time the task has been created</param>
/// <param name="Status">the status of the task</param>
///  <param name="Milestone">the mile stone of the task</param>
///   <param name="BaselineStartDate">the baseline of the task</param>
/// <param name="StartDate">the start date of the task</param>
/// <param name="ScheduledStartDate">the scheduled time to start the task</param>
/// <param name="ForecastDate">fore cast date of teh task </param>
/// <param name="DeadlineDate">the last date to finish the task</param>
/// <param name="CompleteDate">the actual finish date of the CompleteDate</param>
///  <param name="Deliverables">the product</param>
/// <param name="Remarks">highlights on the milestone</param>
/// <param name="Dependencies">a list of tasks the are dependent on the milestone </param>
/// <param name="Deliverables">the product</param>
/// <param name="Remarks">highlights on the task</param>
/// <param name="Engineer">The  engineer that is assigned to the mission</param>
/// <param name="ComplexityLevel">the level of complexity of the task</param>
/// </summary>
public class Task
{
    public int Id { get; init; }
    public string? Description { get; set; } = "";
    public string? Alias { get; set; } = "";
    public DateTime CreatedAtDate { get; set; } = DateTime.Now;
    public Status? Status { get; set; }
    public List<TaskInList>? Dependencies { get; set; } = new();
    public MilestoneInTask? Milestone { get; set; } = new();
    public DateTime? StartDate { get; set; }
    public DateTime? ScheduledStartDate { get; set; }
    public DateTime? ForeCastDate { get; set; } = DateTime.MinValue;
    public DateTime? DeadlineDate { get; set; }
    public DateTime? CompleteDate { get; set; }
    public string? Deliverables { get; set; } = "";
    public string? Remarks { get; set; } = "";
    public EngineerInTask Engineer { get; set; }= new();
    public DO.EngineerExperience? ComplexityLevel {  get; set; }







}

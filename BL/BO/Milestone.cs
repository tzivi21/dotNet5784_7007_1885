
using System.ComponentModel;

namespace BO;
/// <summary>
/// Milestone details
/// <param name="id">the identifier number of the milestone</param>
/// <param name="Description">a string with the description of the milestone</param>
/// <param name="Alias">the nickname of the milestone</param>
/// <param name="CreatedAt">the time the milestone has been created</param>
/// <param name="Status">the status of the milestone</param>
/// <param name="ForecastDate">fore cast date of teh milestone </param>
/// <param name="DeadlineDate">the last date to finish the milestone</param>
/// <param name="CompleteDate">the actual finish date of the CompleteDate</param>
/// <param name="Remarks">highlights on the milestone</param>
/// <param name="Dependencies">a list of tasks the are dependent on the milestone </param>
/// </summary>
public class Milestone
{
    public int Id { get; init; }
    public string Description { get; set; } = "";
    public string Alias { get; set; } = "";
    public DateTime CreatedAtDate { get; set; }= DateTime.Now;
    public Status? Status { get; set; }
    public DateTime? ForecastDate { get; set; }
    public DateTime? DeadlineDate { get; set; }
    public DateTime? CompleteDate { get; set; }
    public double CompletionPercentage { get; set; } = 0;
    public string Remarks { get; set; } = "";
    public List<TaskInList> Dependencies { get; set; } = new();



}

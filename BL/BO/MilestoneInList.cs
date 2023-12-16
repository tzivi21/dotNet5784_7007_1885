
namespace BO;
/// <summary>
/// Milestone in list details
/// <param name="id">the identifier number of the milestone</param>
/// <param name="Description">a string with the description of the milestone</param>
/// <param name="Alias">the nickname of the milestone</param>
/// <param name="Status">the status of the milestone</param>
/// <param name="CompletionPercentage">the percentages of progress of the milestone </param>
/// </summary>
public class MilestoneInList
{
    public int Id { get; init; }
    public string Description { get; set; } = "";
    public string Alias { get; set; } = "";
    public Status? Status { get; set; }
    public double CompletionPercentage { get; set; } = 0;

}



namespace BO;
/// <summary>
/// Milestone in task details
/// <param name="id">the identifier number of the milestone</param>
/// </summary>
public class MilestoneInTask
{ 
    public int Id { get; init; }
    public string? Alias { get; init; } = "";
    public override string ToString() => Tools.ToStringProperty(this);
}

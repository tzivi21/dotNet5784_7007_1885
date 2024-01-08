
namespace BO;
///<summary>
/// Engineer in task details
/// <param name="Id">The id of the engineer</param>
/// <param name="Name">the name of the engineer</param>
/// </summary>
public class EngineerInTask
{
    public int? Id { get; set; }

    public string? Name { get; set; } = "";
    public override string ToString() => Tools.ToStringProperty(this);
}

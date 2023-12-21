﻿
namespace BO;
/// <summary>
/// task in list details
/// <param name="id">the identifier number of the task</param>
/// <param name="Description">a string with the description of the task</param>
/// <param name="Alias">the nickname of the task</param>
///  <param name="Status">the status of the task</param>
///  </summary>

public class TaskInList
{
    public int Id { get; init; }
    public string Description { get; set; } = "";
    public string Alias { get; set; } = "";
    public Status? Status { get; set; }
    public override string ToString() => Tools.ToStringProperty(this);

}

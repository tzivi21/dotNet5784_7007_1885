
using System.Runtime.CompilerServices;

namespace DO;
/// <summary>
/// managing Task class with all the task properties
/// <param name="id">the identifier number of the task</param>
/// <param name="Description">a string with the description of the task</param>
/// <param name="Alias">the nickname of the task</param>
/// <param name="Milestone">the mile stone of the task</param>
/// <param name="CreatedAt">the time the task has been created</param>
/// <param name="Start">the starting time of the task</param>
/// <param name="ScheduleDate">Estimated date for completion of the task </param>
/// <param name="DeadLine">the last date to finish the task</param>
/// <param name="Complete">the actual finish date of the task</param>
/// <param name="Deliverables">the product</param>
/// <param name="Remarks">highlights on the task</param>
/// <param name="Engineerid">The identification number of the engineer assigned to the mission</param>
/// <param name="ComplexityLevel">the level of complexity of the task</param>
/// </summary>
public record Task
{

    public int Id { set; get; } = 0;
    public string? Description { set; get; } 
    public string? Alias { set; get; } 
    public DateTime? ForCastDate { set; get; }
    public bool? Milestone { set; get; } = false;
    public DateTime CreatedAt { set; get; } = DateTime.Now;
    public DateTime? Start { set; get; }
    public DateTime? ScheduleDate { set; get; }
    public DateTime? DeadLine { set; get; }
    public DateTime? Complete { set; get; }
    public string? Deliverables { set; get; }
    public string? Remarks { set; get; } 
    public int? Engineerid { set; get; }
    public EngineerExperience? ComplexityLevel { set; get; } 

    public Task():this("","", DateTime.Now, DateTime.Now, DateTime.Now, DateTime.Now, DateTime.Now,"","",0,EngineerExperience.Expert)
    {

    }
    public override string ToString()
    {
        return $"Task ID: {Id}\n" +
               $"Description: {Description}\n" +
               $"Alias: {Alias}\n" +
               $"ForCastDate: {ForCastDate}\n" +
               $"Milestone: {Milestone}\n" +
               $"Created At: {CreatedAt}\n" +
               $"Start: {Start}\n" +
               $"Schedule Date: {ScheduleDate}\n" +
               $"Deadline: {DeadLine}\n" +
               $"Complete: {Complete}\n" +
               $"Deliverables: {Deliverables}\n" +
               $"Remarks: {Remarks}\n" +
               $"Engineer ID: {Engineerid}\n" +
               $"Complexity Level: {ComplexityLevel}\n";
    }
    public Task(string? myDescription, string? myAlias, DateTime? myForCastDate, DateTime? myStart, DateTime? myScheduleDate
        , DateTime? myDeadLine, DateTime? myComplete, string? myDeliverables
        , string? myRemarks, int? myEngineerid, EngineerExperience? myComplexityLevel)
    {

        Description = myDescription;
        Alias = myAlias;
        ForCastDate = myForCastDate;
        Start = myStart;
        ScheduleDate = myScheduleDate;
        DeadLine = myDeadLine;
        Complete = myComplete;
        Deliverables = myDeliverables;
        Remarks = myRemarks;
        Engineerid = myEngineerid;
        ComplexityLevel = myComplexityLevel;

    }




}

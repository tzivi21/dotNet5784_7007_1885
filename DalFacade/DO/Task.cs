
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
     public int Id;
     string? Description  = null;
     string? Alias  = null;
     bool Milestone  = false;
     DateTime CreatedAt  = DateTime.Now;
     DateTime Start;
     DateTime ScheduleDate;
     DateTime DeadLine;
     DateTime Complete;
     string Deliverables;
     string? Remarks = null;
     int Engineerid;
     string? ComplexityLevel  = null;
     public Task(string myDescription,string myAlias, DateTime myStart, DateTime myScheduleDate
        , DateTime myDeadLine, DateTime myComplete, string myDeliverables
        , string myRemarks, int myEngineerid, string myComplexityLevel)
     {
       // Id =;
        Description=myDescription;
        Alias=myAlias;
        Start=myStart;
        ScheduleDate=myScheduleDate;
        DeadLine=myDeadLine;
        Complete=myComplete;
        Deliverables=myDeliverables;
        Remarks=myRemarks;
        Engineerid=myEngineerid;
        ComplexityLevel=myComplexityLevel;
     }
     public Task()
     {
       // Id=;
     }

}

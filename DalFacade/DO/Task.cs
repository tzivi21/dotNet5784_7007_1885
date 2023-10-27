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
    protected static int nextId=1;
    public int Id { set; get; }
    public string Description { set; get; } = null;
    public string Alias { set; get; } = null;
    public bool Milestone { set; get; } = false;
    public DateTime CreatedAt { set; get; } = DateTime.Now;
    public DateTime Start { set; get; }
    public DateTime ScheduleDate { set; get; }
    public DateTime DeadLine { set; get; }
    public DateTime Complete { set; get; }
    public string Deliverables { set; get; }
    public string Remarks { set; get; } = null;
    public int Engineerid { set; get; }
    public string ComplexityLevel { set; get; } = null;
    public Task(string myDescription,string myAlias, DateTime myStart, DateTime myScheduleDate
        , DateTime myDeadLine, DateTime myComplete, string myDeliverables
        , string myRemarks, int myEngineerid, string myComplexityLevel)
    {
        Id = nextId++;
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

       // Id = config.id;
       // config.id++;
    }
    public Task()
    {
       // Id=nextId++;
    }

}

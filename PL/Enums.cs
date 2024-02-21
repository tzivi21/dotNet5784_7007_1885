using System.Collections;

namespace PL;

public class  EngineerExperience : IEnumerable
{
    static readonly IEnumerable<BO.EngineerExperience> ex_enums =
(Enum.GetValues(typeof(BO.EngineerExperience)) as IEnumerable<BO.EngineerExperience>)!;

    public IEnumerator GetEnumerator() => ex_enums.GetEnumerator();
}
public class TaskStatus : IEnumerable
{
    static readonly IEnumerable<BO.Status> ex_enums =
(Enum.GetValues(typeof(BO.Status)) as IEnumerable<BO.Status>)!;

    public IEnumerator GetEnumerator() => ex_enums.GetEnumerator();
}

public enum PageMode
{
    Add,
    Update
}
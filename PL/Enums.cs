using System.Collections;

namespace PL;

public class  EngineerExperience : IEnumerable
{
    static readonly IEnumerable<BO.EngineerExperience> ex_enums =
(Enum.GetValues(typeof(BO.EngineerExperience)) as IEnumerable<BO.EngineerExperience>)!;

    public IEnumerator GetEnumerator() => ex_enums.GetEnumerator();
}

public enum PageMode
{
    Add,
    Update
}
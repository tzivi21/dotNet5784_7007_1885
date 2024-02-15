using System.Collections;

namespace PL;

public class  EngineerExperience : IEnumerable
{
    static readonly IEnumerable<DO.EngineerExperience> ex_enums =
(Enum.GetValues(typeof(DO.EngineerExperience)) as IEnumerable<DO.EngineerExperience>)!;

    public IEnumerator GetEnumerator() => ex_enums.GetEnumerator();
}

public enum PageMode
{
    Add,
    Update
}
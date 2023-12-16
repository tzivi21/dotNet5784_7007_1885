
namespace BIApi;

public interface IMilestone
{
    /// <summary>
    /// return a specific milestone information 
    /// </summary>
    /// <param name="id">the id of the wanted milestone</param>
    /// <returns>Milestone objects with the given id</returns>
    public BO.Milestone? Read(int id);
    /// <summary>
    /// updates a specific milestone
    /// </summary>
    /// <param name="item">the milestone with the new information to update</param>
    public void Update(BO.Milestone item);
}

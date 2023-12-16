
using BIApi;

namespace BIImplementation;

internal class TaskImplementation : ITask
{
    private readonly DalApi.IDal _dal = DalApi.Factory.Get;

    public int Create(BO.Task item)
    {
        DO.Task DOTask = new
            (item.Description,item.Alias,item.StartDate,item.ScheduledStartDate,item.DeadlineDate,item.CompleteDate,item.Deliverables,item.Remarks,item.Engineer.Id,item.ComplexityLevel);
        try
        {
            int id = _dal.Task.Create(DOTask);
            return id;
        }
        catch (DO.DalAlreadyExistException ex)
        {
            throw new BO.BlAlreadyExistException($"Student with ID={item.Id} already exists");
        }

        
    }

    public void Delete(int id)
    {
        throw new NotImplementedException();
    }

    public BO.Task? Read(int id)
    {
        DO.Task task= _dal.Task.Read(id);
        if (task == null)
            throw new BO.BlDoesNotExistException($"Student with ID={id} does Not exist");

        return new BO.Task()
        {
            Id=id,
            Description=task.Description,
            Alias=task.Alias,
            CreatedAtDate=task.CreatedAt,
            Status=null,
            Milestone=task.Milestone,
        };

    }

    public IEnumerable<BO.Task> ReadAll()
    {
        throw new NotImplementedException();
    }

    public void Update(BO.Task item)
    {
        throw new NotImplementedException();
    }
}

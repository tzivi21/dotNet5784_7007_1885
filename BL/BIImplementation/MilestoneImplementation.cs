
using BIApi;
using BO;

namespace BIImplementation;

internal class MilestoneImplementation : IMilestone
{
    private readonly DalApi.IDal _dal = DalApi.Factory.Get;

    public Milestone? Read(int id)
    {
        try
        {
            DO.Task? DOTask = _dal.Task.Read(id);
            List<BO.TaskInList> dependencies = (from d in _dal.Dependency!.ReadAll(d => d.DependentTask == id)
                                                where true
                                                select new BO.TaskInList()
                                                {
                                                    Id = d.DependsOnTask,
                                                    Alias = _dal.Task!.Read(d.DependsOnTask)?.Alias,
                                                    Description = _dal.Task!.Read(d.DependsOnTask)?.Description,
                                                    Status = DetermineStatus(_dal.Task!.Read(d.DependsOnTask))
                                                }
                                                ).ToList();
            return new BO.Milestone()
            {
                Id = id,
                Description = DOTask!.Description,
                Alias = DOTask!.Alias,
                CreatedAtDate = DOTask!.CreatedAt,
                Status = DetermineStatus(DOTask),
                ForeCastDate = DOTask!.ForCastDate,
                DeadlineDate = DOTask!.DeadLine,
                CompleteDate = DOTask!.Complete,
                CompletionPercentage = 0,//nmjvgc
                Dependencies = dependencies
            };
        }
        catch (DO.DalDoesNotExistException ex)
        {
            throw new BO.BlDoesNotExistException($"Student with ID={id} does not  exists", ex);
        }
    }

    public BO.Milestone Update(Milestone item)
    {

        DO.Task? prevTask = _dal.Task.Read(item.Id);
        if (prevTask == null)
        {
            throw new BO.BlDoesNotExistException($"Student with ID={item.Id} does not  exists");
        }

        DO.Task DOTask = new DO.Task()
        {
            Id = item.Id,
            Description = item.Description,
            Alias = item.Alias,
            ForCastDate = prevTask.ForCastDate,
            CreatedAt = prevTask.CreatedAt,
            Start = prevTask.Start,
            ScheduleDate = prevTask.ScheduleDate,
            DeadLine = prevTask.DeadLine,
            Complete = prevTask.Complete,
            Deliverables = prevTask.Deliverables,
            Remarks = item.Remarks,
            Engineerid = prevTask.Engineerid,
            ComplexityLevel = prevTask.ComplexityLevel
            
        };
        try
        {
            _dal.Task.Update(DOTask);
            List<BO.TaskInList> dependencies = (from d in _dal.Dependency!.ReadAll(d => d.DependentTask == DOTask.Id)
                                                where true
                                                select new BO.TaskInList()
                                                {
                                                    Id = d.DependsOnTask,
                                                    Alias = _dal.Task!.Read(d.DependsOnTask)?.Alias,
                                                    Description = _dal.Task!.Read(d.DependsOnTask)?.Description,
                                                    Status = DetermineStatus(_dal.Task!.Read(d.DependsOnTask))
                                                }
                                                ).ToList();
            return new BO.Milestone()
            {
                Id = DOTask.Id,
                Description = DOTask!.Description,
                Alias = DOTask!.Alias,
                CreatedAtDate = DOTask!.CreatedAt,
                Status = DetermineStatus(DOTask),
                ForeCastDate = DOTask!.ForCastDate,
                DeadlineDate = DOTask!.DeadLine,
                CompleteDate = DOTask!.Complete,
                CompletionPercentage = 0,//nmjvgc
                Dependencies = dependencies
            };
        }
        catch (DO.DalDoesNotExistException ex)
        {
            throw new BO.BlDoesNotExistException($"Student with ID={item.Id} does not  exists", ex);
        }


    }
    public Status DetermineStatus(DO.Task task)
    {
        if (task.Start == null && task.ScheduleDate == null && task.DeadLine == null)
        {
            return Status.Unscheduled;
        }
        else if (task.Start != null && task.ScheduleDate != null && task.DeadLine != null && task.Complete == null)
        {
            if (DateTime.Now < task.DeadLine)
            {
                return Status.OnTrack;
            }
            else
            {
                return Status.InJeopardy;
            }
        }
        else
        {
            return Status.Scheduled;
        }
    }
}

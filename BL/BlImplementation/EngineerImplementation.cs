
using BlApi;
using BO;
using System.Net.Mail;

namespace BlImplementation;

internal class EngineerImplementation : IEngineer
{
    private readonly DalApi.IDal _dal = DalApi.Factory.Get;//to access the dal methods
    static readonly BlApi.IBl s_bl = BlApi.Factory.Get();//to access the BL methods

    public int Add(BO.Engineer item)
    {
        //check input
        if (item.Id < 0 || item.Name == "" || item.Cost < 0)
        {
            throw new BO.BlNotValidValue("the value is not valid");
        }
        //check mail validity
        try
        {
            MailAddress mail = new MailAddress(item.Email);
        }
        catch (Exception e)
        {
            throw new BO.BlNotValidValue(e.Message);
        }

        DO.Engineer DOEnginner = new()
        {
            Id = item.Id,
            Name = item.Name,
            Email = item.Email,
            Level = item.Level,
        };
        try
        {
            int id = _dal.Engineer.Create(DOEnginner);
            return id;
        }
        catch (DO.DalAlreadyExistException ex)
        {
            throw new BO.BlAlreadyExistException($"engineer with ID={item.Id} already exists", ex);
        }
    }

    public void Delete(int id)
    {
        //check if the engineer is in a task 
        if(s_bl.Engineer.Read(id)!.Task!=null)
        {
            throw new BO.BlDeletionImpossible("can't delete an engineer that is in the middle of a task/ finished task");
        }

        try
        {
            _dal.Engineer.Delete(id);
        }
        catch (DO.DalDoesNotExistException ex)
        {
            throw new BO.BlDoesNotExistException($"engineer with ID={id} does not  exists", ex);
        }

    }

    public Engineer? Read(int id)
    {
        //check if the engineer existes
        DO.Engineer? DOEngineer = _dal.Engineer.Read(id);
        if( DOEngineer == null)
        {
            throw new BO.BlDoesNotExistException($"engineer with ID={id} does not  exists");
        }
        //search for the task that the engineer working on
        BO.TaskInEngineer? engineerTask = new BO.TaskInEngineer()
        {
            Id = _dal.Task.Read(t => t.Engineerid == id)?.Id??0,
            Alias = _dal.Task.Read(t => t.Engineerid == id)?.Alias ?? ""
        };
        //the engineer doesn't work on a task now
        if (engineerTask.Id == 0 || engineerTask.Alias == "")
        {
            engineerTask = null;
        }
        return new BO.Engineer()
        {
            Id = id,
            Name = DOEngineer.Name,
            Email = DOEngineer.Email,
            Level = DOEngineer.Level,
            Cost = 0,
            Task = engineerTask
        };
    }

    public IEnumerable<Engineer> ReadAll()
    {
        return from e in _dal.Engineer.ReadAll()
               select Read(e.Id);
    }

    public void Update(Engineer item)
    {
        if (item.Name == "" || item.Cost < 0)//check validity
        {
            throw new BO.BlNotValidValue("the value is not valid");
        }

        try
        {
            MailAddress mail = new MailAddress(item.Email);
        }
        catch (Exception e)
        {
            throw new BO.BlNotValidValue(e.Message);
        }
        try
        {
            DO.Engineer DOEnginner = new()
            {
                Id = item.Id,
                Name = item.Name,
                Email = item.Email,
                Level = item.Level,
            };
            _dal.Engineer.Update(DOEnginner);
        }
        catch(DO.DalDoesNotExistException ex)
        {
            throw new BO.BlDoesNotExistException($"engineer with ID={item.Id} does not  exists", ex);
        }
    }
}

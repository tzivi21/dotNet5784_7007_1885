
using BIApi;
using BO;
using System.Net.Mail;

namespace BIImplementation;

internal class EngineerImplementation : IEngineer
{
    private readonly DalApi.IDal _dal = DalApi.Factory.Get;
    public int Add(Engineer item)
    {
        if (item.Id < 0 || item.Name == "" || item.Cost < 0)
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
            throw new BO.BlAlreadyExistException($"Student with ID={item.Id} already exists", ex);
        }
    }

    public void Delete(int id)
    {
        if (_dal.Task.ReadAll(t => t.Engineerid == id) != null)
        {
            throw new BO.BlDeletionImpossible("can't delete an engineer that is in the middle of a task/ finished task");
        }

        try
        {
            _dal.Task.Delete(id);
        }
        catch (DO.DalDoesNotExistException ex)
        {
            throw new BO.BlDoesNotExistException($"Student with ID={id} does not  exists", ex);
        }

    }

    public Engineer? Read(int id)
    {
        DO.Engineer? DOEngineer = _dal.Engineer.Read(id);
        if( DOEngineer == null)
        {
            throw new BO.BlDoesNotExistException($"Student with ID={id} does not  exists");
        }
        return new BO.Engineer()
        {
            Id = id,
            Name = DOEngineer.Name,
            Email = DOEngineer.Email,
            Level = DOEngineer.Level,
            Cost = 0,
            Task = new BO.TaskInEngineer()
            {
                Id = _dal.Task.Read(t => t.Engineerid == id)!.Id,
                Alias = _dal.Task.Read(t => t.Engineerid == id)!.Alias ?? ""
            }
        };
    }

    public IEnumerable<Engineer> ReadAll()
    {
        throw new NotImplementedException();
    }

    public void Update(Engineer item)
    {
        throw new NotImplementedException();
    }
}

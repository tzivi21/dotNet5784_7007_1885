
using BIApi;
using BO;

namespace BIImplementation;

internal class EngineerImplementation : IEngineer
{
    private readonly DalApi.IDal _dal = DalApi.Factory.Get;
    public void Add(Engineer item)
    {
        throw new NotImplementedException();
    }

    public void Delete(int id)
    {
        throw new NotImplementedException();
    }

    public Engineer? Read(int id)
    {
        throw new NotImplementedException();
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

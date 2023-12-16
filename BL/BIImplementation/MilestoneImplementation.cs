
using BIApi;
using BO;

namespace BIImplementation;

internal class MilestoneImplementation : IMilestone
{
    private readonly DalApi.IDal _dal = DalApi.Factory.Get;

    public Milestone? Read(int id)
    {
        throw new NotImplementedException();
    }

    public void Update(Milestone item)
    {
        throw new NotImplementedException();
    }
}

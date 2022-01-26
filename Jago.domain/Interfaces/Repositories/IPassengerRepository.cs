using Jago.domain.Core.Entities;

namespace Jago.domain.Interfaces.Repositories
{
    public interface IPassengerRepository : IRepository<Passenger>
    {
        IQueryable<Passenger> GetPax();
    }
}

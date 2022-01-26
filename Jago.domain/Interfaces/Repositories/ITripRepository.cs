using Jago.Application.Services;
using Jago.domain.Core.Entities;

namespace Jago.domain.Interfaces.Repositories
{
    public interface ITripRepository : IRepository<Trip>
    {
        IQueryable<Passenger> GetPax();
        bool IsValidPassengerId(Guid passengerId);
        IQueryable<PaxListModel> GetPaxList();
    }
}

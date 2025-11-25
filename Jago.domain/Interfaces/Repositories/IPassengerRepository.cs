using Jago.domain.Entities;

namespace Jago.domain.Interfaces.Repositories
{
    public interface IPassengerRepository : IRepository<Passenger>
    {
        IQueryable<Passenger> GetPax();
        Task<bool> RemovePassengerAsync(Guid Id);
        IQueryable<Passenger> IsDocumentNumberUniqueAcrossPassengers(string document, Guid passengerId);
    }
}

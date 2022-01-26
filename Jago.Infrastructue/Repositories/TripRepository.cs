using Jago.Application.Services;
using Jago.domain.Core.Entities;
using Jago.domain.Interfaces.Repositories;
using Jago.Infrastructure.DBConfiguration;

namespace Jago.Infrastructure.Repositories
{
    public class TripRepository : Repository<Trip>, ITripRepository
    {
        public TripRepository(ApplicationContext context) : base (context) 
        {

        }

        public IQueryable<Passenger> GetPax()
        {
            return Db.Passengers ;   
        }
        public bool IsValidPassengerId(Guid passengerId)
        {
            return Db.Passengers.Any(_ => _.Id == passengerId);
        }
        public IQueryable<PaxListModel> GetPaxList()
        {
            return Db.Passengers.Select(j => new PaxListModel { Id = j.Id, Name = j.Name}).AsQueryable();
        }
            
    }
}

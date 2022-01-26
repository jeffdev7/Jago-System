using Jago.domain.Core.Entities;
using Jago.domain.Interfaces.Repositories;
using Jago.Infrastructure.DBConfiguration;

namespace Jago.Infrastructure.Repositories
{
    public class PassengerRepository : Repository<Passenger>, IPassengerRepository
    {
        public PassengerRepository(ApplicationContext context) : base(context)
        { 
        }
        public IQueryable<Passenger> GetPax()
        {
            return Db.Passengers;
        }
    }
}

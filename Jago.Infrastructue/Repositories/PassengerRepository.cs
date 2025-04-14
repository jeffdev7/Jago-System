using Jago.domain.Entities;
using Jago.domain.Interfaces.Repositories;
using Jago.Infrastructure.DBConfiguration;
using Microsoft.EntityFrameworkCore;

namespace Jago.Infrastructure.Repositories
{
    public class PassengerRepository : Repository<Passenger>, IPassengerRepository
    {
        public PassengerRepository(ApplicationContext context) : base(context)
        {
        }
        public IQueryable<Passenger> GetPax()
        {
            return _context.Passengers;
        }

        public IQueryable<Passenger> IsDocumentNumberUniqueAcrossPassengers(string document, Guid passengerId)
        {
            var t = _context.Passengers.Where(p => p.DocumentNumber == document && p.Id == passengerId);
            var y = _context.Passengers.Any(p => p.DocumentNumber == document && p.Id == passengerId);
            return t;
        }

        public async Task<bool> RemovePassengerAsync(Guid Id)
        {
            Passenger? passenger = await _context.Passengers.Where(x => x.Id == Id).FirstOrDefaultAsync();

            if (passenger == null)
                return false;
            _context.Passengers.Remove(passenger);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}

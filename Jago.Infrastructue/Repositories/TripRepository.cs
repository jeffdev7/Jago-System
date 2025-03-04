using Jago.Application.Services;
using Jago.domain.Entities;
using Jago.domain.Interfaces.Repositories;
using Jago.Infrastructure.DBConfiguration;
using Microsoft.EntityFrameworkCore;

namespace Jago.Infrastructure.Repositories
{
    public class TripRepository : Repository<Trip>, ITripRepository
    {
        public TripRepository(ApplicationContext context) : base(context)
        {
        }

        public IQueryable<Passenger> GetPax()
        {
            return _context.Passengers;
        }
        public bool IsValidPassengerId(Guid passengerId)
        {
            return _context.Passengers.Any(_ => _.Id == passengerId);
        }
        public IQueryable<PaxListModel> GetPaxList()
        {
            return _context.Passengers.Select(j => new PaxListModel 
            { 
                Id = j.Id, 
                Name = j.Name, 
                UserId = j.UserId 
            }).AsQueryable();
        }
        public async Task<bool> RemoveTripAsync(Guid Id)
        {
            Trip? trip = await _context.Trips.Where(x => x.Id == Id).FirstOrDefaultAsync();

            if (trip == null)
                return false;
            _context.Trips.Remove(trip);
            await _context.SaveChangesAsync();
            return true;
        }

    }
}

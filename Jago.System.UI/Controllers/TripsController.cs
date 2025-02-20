#nullable disable
using Jago.Application.Services;
using Jago.CrossCutting.Dto;
using Jago.domain.Core.Entities;
using Jago.Infrastructure.DBConfiguration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Jago.System.UI.Controllers
{
    public class TripsController : BaseController<TripViewModel>
    {
        private readonly ApplicationContext db;
        private readonly ITripServices _tripServices;
        private readonly IPassengerServices _passengerServices;

        public TripsController(ITripServices tripServices, IPassengerServices passengerServices,
            ApplicationContext db) : base(db)
        {
            _tripServices = tripServices;
            _passengerServices = passengerServices;
        }

        // GET: Trips
        public async Task<IActionResult> Index()
        {
            return View(GetRows());
        }

        public override IEnumerable<TripViewModel> GetRows()
        {
            return _tripServices.GetOrder();
        }
        public override TripViewModel GetRow(Guid id)
        {
            return _tripServices.GetById(id);

        }

        // GET: Trips/Details
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trip = await Db.Trips
                 .Include(t => t.Passenger)
                 .FirstOrDefaultAsync(m => m.Id == id);
            if (trip == null)
            {
                return NotFound();
            }

            return View(trip);
        }

        // GET: Trips/Create
        [HttpGet]
        public IActionResult Create()
        {
            LoadViewBags();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Trip trip)
        {
            LoadViewBags();
            if (ModelState.IsValid)
            {
                return View(trip);

            }
            Db.Trips.Add(trip);
            Db.SaveChanges();
            TempData["success"] = "Trip added successfully";
            return RedirectToAction("Index");

        }

        // GET: Trips/Edit
        [HttpGet]
        public IActionResult Edit(Guid id)
        {
            LoadViewBags();
            var item = Db.Trips.FirstOrDefault(j => j.Id == id);
            if (item == null) return BadRequest();
            return View(item);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Trip trip)
        {
            LoadViewBags();
            if (ModelState.IsValid)
            {
                return View(trip);

            }
            Db.Trips.Update(trip);
            Db.SaveChanges();
            TempData["success"] = "Trip updated successfully";
            return RedirectToAction("Index");
        }

        // GET: Trips/Delete
        [HttpGet]
        public async Task<IActionResult> Delete(Guid? id)
        {

            if (id == null)
            {
                return NotFound();
            }

            var trip = await Db.Trips
                .Include(t => t.Passenger)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (trip == null)
            {
                return NotFound();
            }

            return View(trip);
        }

        // POST: Trips/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var trip = await Db.Trips.FindAsync(id);
            Db.Trips.Remove(trip);
            await Db.SaveChangesAsync();
            TempData["success"] = "Trip deleted successfully";
            return RedirectToAction(nameof(Index));

        }

        private bool TripExists(Guid id)
        {
            return Db.Trips.Any(e => e.Id == id);
        }

        public override void LoadViewBags()
        {
            LoadAsync();
        }
        public async void LoadAsync()
        {
            ViewBag.Passengers = _tripServices.GetPaxList().ToList();
        }
    }
}

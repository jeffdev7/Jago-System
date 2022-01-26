#nullable disable
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Jago.Infrastructure.DBConfiguration;
using Jago.domain.Core.Entities;
using Jago.Application.Services;
using Jago.Application.ViewModel;

namespace Jago.System.UI.Controllers
{
    public class PassengersController : BaseController<PassengerViewModel>
    {

        private readonly ApplicationContext _context;
        private readonly IPassengerServices _paxServices;

        public PassengersController(IPassengerServices paxServices, ApplicationContext db) : base(db)
        {
            _paxServices = paxServices;
        }

        public override IEnumerable<PassengerViewModel> GetRows()
        {
            return _paxServices.GetAll();
        }
        public override PassengerViewModel GetRow(Guid id)
        {
            return _paxServices.GetById(id);

        }

        // GET: Passengers
        public IActionResult Index()
        {
            return View(GetRows());
        }

        // GET: Passengers/Details
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var passenger = await Db.Passengers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (passenger == null)
            {
                return NotFound();
            }

            return View(passenger);
        }

        // GET: Passengers/Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Passenger vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }
            Db.Passengers.Add(vm);
            Db.SaveChanges();
            TempData["success"] = "Passenger added successfully";
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit (Guid id)
        {
            var item = Db.Passengers.FirstOrDefault(j=>j.Id == id);
            if(item == null) return BadRequest();
            LoadViewBags();
            return View(item);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Passenger pax)
        {
            LoadViewBags();
            if (!ModelState.IsValid)
                return View(pax);           

            var item = Db.Passengers.AsNoTracking().Where(_ => _.Id == pax.Id);
            if (item == null) return BadRequest();
            Db.Entry(pax).State = EntityState.Modified;
            Db.SaveChanges();
            TempData["success"] = "Passenger updated successfully";
            return RedirectToAction("Index");
        }

        // GET: Passengers/Delete
        public async Task<IActionResult> Delete(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var passenger = await Db.Passengers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (passenger == null)
            {
                return NotFound();
            }

            return View(passenger);
        }

       // POST: Passengers/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var passenger = await Db.Passengers.FindAsync(id);
            Db.Passengers.Remove(passenger);
            await Db.SaveChangesAsync();
            TempData["success"] = "Passenger deleted successfully";
            return RedirectToAction(nameof(Index));
        }

        private bool PassengerExists(Guid id)
        {
            return _context.Passengers.Any(e => e.Id == id);
        }
        public override void LoadViewBags()
        {
            LoadAsync();
        }
        public async void LoadAsync() { }
    }
}

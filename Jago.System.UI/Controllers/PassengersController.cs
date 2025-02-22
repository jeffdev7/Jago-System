#nullable disable
using Jago.Application.Services;
using Jago.CrossCutting.Dto;
using Jago.domain.Core.Entities;
using Jago.Infrastructure.DBConfiguration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
        public IActionResult Create(PassengerViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }
            var result = _paxServices.Add(vm);

            if(!result.IsValid)
                TempData["success"] = "ERROR PASSENGER WAS NOT ADDED";

            else
            {
                TempData["success"] = "Passenger added successfully";
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(Guid id)
        {
            var item = Db.Passengers.FirstOrDefault(j => j.Id == id);//TODO 
            if (item == null) 
                return BadRequest();
            LoadViewBags();
            return View(item);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(PassengerViewModel pax)
        {
            var result = _paxServices.Update(pax);

            if(!result.IsValid)
                TempData["success"] = "Passenger WAS NOT UPDATED";
            else
            {
                TempData["success"] = "Passenger updated successfully";
            }

            return RedirectToAction("Index");
        }

        // GET: Passengers/Delete
        public async Task<IActionResult> Delete(Guid id)//TODO
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
            var result = _paxServices.Remove(id);
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

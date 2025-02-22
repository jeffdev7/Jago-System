#nullable disable
using Jago.Application.Services;
using Jago.CrossCutting.Dto;
using Jago.Infrastructure.DBConfiguration;
using Microsoft.AspNetCore.Mvc;

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

        // GET: Passengers
        public IActionResult Index()
        {
            return View(GetRows());
        }

        // GET: Passengers/Details
        public IActionResult Details(Guid id)
        {
            var passenger = _paxServices.GetById(id);

            if (passenger == null)
                return NotFound();

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
                return View(vm);

            var result = _paxServices.Add(vm);

            if (!result.IsValid)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.ErrorMessage);
                }

                return View(vm);
            }

            else
            {
                TempData["success"] = "Passenger added successfully";
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(Guid id)
        {
            var item = _paxServices.GetById(id);
            if (item == null)
                return BadRequest();

            LoadViewBags();

            return View(item);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(PassengerViewModel vm)
        {
            var result = _paxServices.Update(vm);

            if (!result.IsValid)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.ErrorMessage);
                }

                return View(vm);
            }
            else
            {
                TempData["success"] = "Passenger updated successfully";
            }

            return RedirectToAction("Index");
        }

        // GET: Passengers/Delete
        public IActionResult Delete(Guid id)
        {
            var passenger = _paxServices.GetById(id);
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
            var result = await _paxServices.Remove(id);
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

        public override PassengerViewModel GetRow(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}

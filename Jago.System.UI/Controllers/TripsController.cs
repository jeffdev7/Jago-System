#nullable disable
using Jago.Application.Services;
using Jago.CrossCutting.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Jago.System.UI.Controllers
{
    [Authorize]
    public class TripsController : BaseController<TripViewModel>
    {
        private readonly ITripServices _tripServices;
        private readonly IPassengerServices _passengerServices;

        public TripsController(ITripServices tripServices, IPassengerServices passengerServices)
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
            return _tripServices.GetSortedTrips();
        }

        // GET: Trips/Details
        public IActionResult Details(Guid id)
        {
            var trip = _tripServices.GetTripDetails(id);
            if (trip == null)
                return NotFound();

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
        public IActionResult Create(TripViewModel vm)
        {
            var result = _tripServices.Add(vm);

            if (!result.IsValid)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.ErrorMessage);
                }
                LoadViewBags();
                return View(vm);
            }

            else
                TempData["success"] = "Trip added successfully";

            return RedirectToAction("Index");

        }

        // GET: Trips/Edit
        [HttpGet]
        public IActionResult Edit(Guid id)
        {
            var result = _tripServices.GetById(id);

            if (result is null)
                return BadRequest();

            LoadViewBags();

            return View(result);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(TripViewModel vm)
        {
            var result = _tripServices.Update(vm);

            if (!result.IsValid)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.ErrorMessage);
                }
                LoadViewBags();
                return View(vm);
            }
            else
                TempData["success"] = "Trip updated successfully";

            return RedirectToAction("Index");
        }

        // GET: Trips/Delete
        [HttpGet]
        public IActionResult Delete(Guid id)
        {
            var trip = _tripServices.GetTripDetails(id);

            if (trip == null)
                return NotFound();

            return View(trip);
        }

        // POST: Trips/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var result = await _tripServices.Remove(id);

            TempData["success"] = "Trip deleted successfully";
            return RedirectToAction(nameof(Index));

        }

        public override void LoadViewBags()
        {
            LoadAsync();
        }
        public async void LoadAsync()
        {
            ViewBag.Passengers = _tripServices.GetPaxList().ToList();
        }

        public override TripViewModel GetRow(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}

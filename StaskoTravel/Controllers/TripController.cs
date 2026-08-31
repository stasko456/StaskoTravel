using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Query;
using StaskoTravel.Core.IService;
using StaskoTravel.ViewModels.Pagination;
using StaskoTravel.ViewModels.Trip;
using System.Security.Claims;

namespace StaskoTravel.Controllers
{
    public class TripController : Controller
    {
        private readonly ITripService tripService;
        private readonly ILogger<TripController> logger;

        public TripController(ITripService _tripService,
                              ILogger<TripController> _logger)
        {
            this.tripService = _tripService;
            this.logger = _logger;
        }

        [HttpGet]
        [Authorize(Policy = "User")]
        public async Task<IActionResult> Index(string destinationCity, int pageNumber = 1)
        {
            int pageSize = 5;
            var trips = await tripService.GetFilteredTripsAsync(destinationCity, pageNumber, pageSize);
            int totalPages = await tripService.GetTotalPagesAsync(pageSize);

            var vm = new TripPaginationViewModel
            {
                Trips = trips.ToList(),
                CurrentPage = pageNumber,
                TotalPages = totalPages
            };

            return View(vm);
        }

        [HttpGet]
        [Authorize(Policy = "User")]
        public IActionResult Create()
        {
            return View(new TripCreateViewModel());
        }

        [HttpPost]
        [Authorize(Policy = "User")]
        public async Task<IActionResult> Create(TripCreateViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            await tripService.AddTripAsync(vm, Guid.Parse(userId));
            return RedirectToAction("Index", "Trip");
        }

        [HttpGet]
        [Authorize(Policy = "User")]
        public async Task<IActionResult> Edit(Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest();
            }

            try
            {
                var trip = await tripService.GetTripByIdAsync(id);
                return View(trip);
            }
            catch (NullReferenceException ex)
            {
                logger.LogError(ex.Message);
                return RedirectToAction("Error", "Home", new { code = 404 });
            }
        }

        [HttpPost]
        [Authorize(Policy = "User")]
        public async Task<IActionResult> Edit(TripEditViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            try
            {
                await tripService.UpdateTripAsync(vm);
                return RedirectToAction("Index", "Trip");
            }
            catch (NullReferenceException ex)
            {
                logger.LogError(ex.Message);
                return RedirectToAction("Error", "Home", new { code = 404 });
            }
        }

        [HttpPost]
        [Authorize(Policy = "User")]
        public async Task<IActionResult> Delete(Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest();
            }

            try
            {
                await tripService.RemoveTripAsync(id);
                return RedirectToAction("Index", "Trip");
            }
            catch (NullReferenceException ex)
            {
                logger.LogError(ex.Message);
                return RedirectToAction("Error", "Home", new { code = 404 });
            }
        }
    }
}
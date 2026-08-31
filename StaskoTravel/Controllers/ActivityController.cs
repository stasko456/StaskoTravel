using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using StaskoTravel.Core.IService;
using StaskoTravel.ViewModels.Activity;
using StaskoTravel.ViewModels.Pagination;

namespace StaskoTravel.Controllers
{
    public class ActivityController : Controller
    {
        private readonly IActivityService activityService;
        private readonly ILogger<ActivityController> logger;

        public ActivityController(IActivityService _activityService,
                                  ILogger<ActivityController> _logger)
        {
            this.activityService = _activityService;
            this.logger = _logger;
        }

        [HttpGet]
        [Authorize(Policy = "AdminOrUser")]
        public async Task<IActionResult> Index(string title, int pageNumber = 1)
        {
            int pageSize = 5;
            var activities = await activityService.GetFilteredActivitiesAsync(title, pageNumber, pageSize);
            int totalPages = await activityService.GetTotalPagesAsync(pageSize);

            var vm = new ActivityPaginationViewModel
            {
                Activities = activities.ToList(),
                TotalPages = totalPages,
                CurrentPage = pageNumber
            };

            return View(vm);
        }

        [HttpGet]
        [Authorize(Policy = "Admin")]
        public IActionResult Create()
        {
            return View(new ActivityCreateViewModel());
        }

        [HttpPost]
        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> Create(ActivityCreateViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            await activityService.AddActivityAsync(vm);
            return RedirectToAction("Index", "Activity");
        }

        [HttpGet]
        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> Edit(Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest();
            }

            try
            {
                var activity = await activityService.GetActivityByIdAsync(id);
                return View(activity);
            }
            catch (NullReferenceException ex)
            {
                logger.LogError(ex.Message);
                return RedirectToAction("Error", "Home", new { code = 404 });
            }
        }

        [HttpPost]
        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> Edit(ActivityEditViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            try
            {
                await activityService.UpdateActivityAsync(vm);
                return RedirectToAction("Index", "Activity");
            }
            catch (NullReferenceException ex)
            {
                logger.LogError(ex.Message);
                return RedirectToAction("Error", "Home", new { code = 404 });
            }
        }

        [HttpPost]
        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> Delete(Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest();
            }

            try
            {
                await activityService.RemoveActivityAsync(id);
                return RedirectToAction("Index", "Activity");
            }
            catch (NullReferenceException ex)
            {
                logger.LogError(ex.Message);
                return RedirectToAction("Error", "Home", new { code = 404 });
            }
        }
    }
}
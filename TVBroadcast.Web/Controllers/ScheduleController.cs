using Microsoft.AspNetCore.Mvc;
using TVBroadcast.BLL.Services;
using TVBroadcast.Domain.IRepository;
using TVBroadcast.Domain.IServices;
using TVBroadcast.Domain.Models;

namespace TVBroadcast.Web.Controllers
{
    public class ScheduleController : Controller
    {
        private readonly IShowService _showService;

        public ScheduleController(IShowService showService)
        {
            _showService = showService;
        }

        public IActionResult Index()
        {
            var shows = _showService.GetShowsForCurrentWindow();
            return View(shows);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View(); // renders the Add form
        }

        [HttpPost]
        public async Task<IActionResult> Add(ShowsModel model)

        {
            if (!ModelState.IsValid)
            {
                return Json(new { success = false, message = "Please fill all required fields!" });
            }

            if (!_showService.IsTimeSlotAvailable(model.StartTime, model.EndTime))
            {
                return Json(new { success = false, message = "⏰ This time slot is already booked! Please choose a different one." });
            }
            model.ApprovalStatus = "Pending";
            await _showService.AddShowAsync(model);
            return Json(new
            {
                success = true,
                message = "Show added successfully!",
                redirectUrl = Url.Action("Index")
            });
        }

        //edit form
        [HttpPost]
        public async Task<IActionResult> Edit(ShowsModel model)
        {
            if (ModelState.IsValid)
            {
                model.ApprovalStatus = "Pending";
                await _showService.UpdateShowAsync(model);
                return Ok(); // Used in Ajax
            }

            return BadRequest(ModelState);
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var show = _showService.GetById(id);
            if (show == null)
                return NotFound();

            _showService.DeleteShowAsync(id);
            return Ok(); // Success response
        }



    }



}



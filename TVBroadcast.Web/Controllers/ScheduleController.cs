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
            if (ModelState.IsValid)
            {
                Console.WriteLine("Received: " + model.Title + ", " + model.Time);

                await _showService.AddShowAsync(model); // This will save everything!
                return RedirectToAction("Index");
            }

            return View(model);
        }

        //edit form
        [HttpPost]
        public async Task<IActionResult> Edit(ShowsModel model)
        {
            if (ModelState.IsValid)
            {
                await _showService.UpdateShowAsync(model);
                return Ok(); // Used in Ajax
            }

            return BadRequest(ModelState);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await _showService.DeleteShowAsync(id);
            return Ok();
        }


    }

}

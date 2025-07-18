using Microsoft.AspNetCore.Mvc;
using TVBroadcast.DAL.Context;
using TVBroadcast.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace TVBroadcast.Web.Controllers
{
    public class ApproverController : Controller
    {
        private readonly AppDbContext _context;

        public ApproverController(AppDbContext context)
        {
            _context = context;
        }

        // GET: /Approver/Notifications

        public IActionResult Notifications()
        {
            var pendingShows = _context.Shows
                .Where(s => s.ApprovalStatus == "Pending")
                .ToList();

            return View(pendingShows); // 👶 send non-null list to view
        }




        // GET: /Approver/GetShowById/5
        [HttpGet]
        public IActionResult GetShowById(int id)
        {
            var show = _context.Shows.FirstOrDefault(s => s.Id == id);
            if (show == null) return NotFound();

            var result = new
            {
                id = show.Id,
                title = show.Title,
                genre = show.Genre,
                description = show.Description,
                startTime = show.StartTime.ToString("yyyy-MM-ddTHH:mm"),
                endTime = show.EndTime.ToString("yyyy-MM-ddTHH:mm"),
                approvalStatus = show.ApprovalStatus,
                comment = show.Comment
            };

            return Json(result);
        }

        // POST: /Approver/UpdateApprovalStatus
        [HttpPost]
        public IActionResult UpdateApprovalStatus(ShowsModel model)
        {
            var show = _context.Shows.FirstOrDefault(s => s.Id == model.Id);
            if (show == null) return NotFound();

            show.ApprovalStatus = model.ApprovalStatus;
            show.Comment = model.Comment;

            _context.SaveChanges();

            return RedirectToAction("Notifications");
        }

        // POST: /Approver/Reject/5
        [HttpPost]
        public IActionResult Reject(int id)
        {
            var show = _context.Shows.FirstOrDefault(s => s.Id == id);
            if (show != null)
            {
                show.ApprovalStatus = "Rejected";
                _context.SaveChanges();
            }

            return RedirectToAction("Notifications");
        }
    }
}

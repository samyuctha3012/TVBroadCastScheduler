using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;
using System.Text;
using TVBroadcast.DAL.Context;
using TVBroadcast.Domain.Models;


namespace TVBroadcast.Web.Controllers
{
    public class AccountController : Controller
    {

        private readonly AppDbContext _context;

        public AccountController(
            AppDbContext context
        )
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            HttpContext.Session.Clear();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                string hashedPassword = ComputeSha256Hash(model.Password); // 💖 Hash the input password

                var user = _context.Users.FirstOrDefault(u => u.Email == model.Email && u.PasswordHash == hashedPassword); // ✅ Compare hashed

                if (user != null)
                {
                    HttpContext.Session.SetString("UserEmail", user.Email);
                    HttpContext.Session.SetInt32("UserId", user.Id);
                    HttpContext.Session.SetString("FullName", user.FullName);
                    HttpContext.Session.SetInt32("RoleId", user.RoleId);


                    return RedirectToAction("Index", "Schedule");
                }
                else
                {
                    ModelState.AddModelError("", "Invalid email or password.");
                    ViewBag.LoginFailed = true;
                }
            }

            return View(model);
        }


        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                // Hash the password
                string passwordHash = ComputeSha256Hash(model.Password);

                var role = _context.Roles.FirstOrDefault(r => r.Name == model.Role);
                if (role == null)
                {
                    ModelState.AddModelError("", "Selected role is invalid.");
                    return View(model);
                }

                //Create user
                var user = new User
                {
                    FullName = model.FullName,
                    Email = model.Email,
                    PasswordHash = passwordHash,
                    RoleId = role.Id
                };

                //save to DB
                _context.Users.Add(user);
                await _context.SaveChangesAsync();

                // 🎉 Redirect
                return RedirectToAction("Login");
            }

            return View(model);
        }

        //Hash utility
        private string ComputeSha256Hash(string rawData)
        {
            using var sha256 = SHA256.Create();
            byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(rawData));
            return BitConverter.ToString(bytes).Replace("-", "").ToLower();
        }

        //Logout 
        public IActionResult Logout()
        {
            var fullName = HttpContext.Session.GetString("FullName");
            TempData["LogoutMessage"] = $"{fullName} logged out successfully!";
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Schedule");
        }
    }
}

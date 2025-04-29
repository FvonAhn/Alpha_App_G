using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using WebApp.Models;
using Data.Context;

namespace WebApp.Controllers
{

    [Authorize]
    public class AccountController : Controller
    {
        private readonly DataContext _context;

        public AccountController(DataContext context)
        {
            _context = context;
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Auth");
        }

        [HttpPost]
        public async Task<IActionResult> UpdateProfile(UserEditViewModel model) 
        {
            if (!ModelState.IsValid)
            {
                TempData["Error"] = "Invalid data.";
                return RedirectToAction("Index", "Home");
            }

            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == model.Id);

            if (user == null)
            {
                TempData["Error"] = "No User was found.";
                return RedirectToAction("Login", "Auth");
            }

            var existingUser = await _context.Users.FirstOrDefaultAsync(x => x.Email == model.Email && x.Id != user.Id);

            if (existingUser != null)
            {
                TempData["Error"] = "An account exists with this email.";
                return RedirectToAction("Index", "Home");
            }

            user.FullName = model.FullName;
            user.Email = model.Email;

            if (model.NewAvatar != null && model.NewAvatar.Length > 0)
            {
                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads");
                Directory.CreateDirectory(uploadsFolder);

                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(model.NewAvatar.FileName);
                var filePath = Path.Combine(uploadsFolder, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await model.NewAvatar.CopyToAsync(stream);
                }

                user.AvatarUrl = "/uploads/" + fileName;
            }

            await _context.SaveChangesAsync();

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Email),
                new Claim("FullName", user.FullName ?? user.Email),
                new Claim("AvatarUrl", user.AvatarUrl ?? "/images/Avatar1.svg")
            };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));

            TempData["Success"] = "Profile updated.";
            return RedirectToAction("Index", "Home");           
        }
    }
}

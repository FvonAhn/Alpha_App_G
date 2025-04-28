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
            return RedirectToAction("Login", "Auth");
        }

        [HttpGet]
        public async Task<IActionResult> Edit()
        {
            var email = User.Identity?.Name;
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == email);

            if (user == null)
                return RedirectToAction("Login", "Auth");

            var model = new UserEditViewModel
            {
                FullName = user.FullName,
                Email = user.Email,
                CurrentAvatarUrl = user.AvatarUrl,
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UserEditViewModel model) 
        {
            if (!ModelState.IsValid)
                return View(model);

            var email = User.Identity?.Name;
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == email);

            if (user == null)
                return RedirectToAction("Login", "Auth");

            user.FullName = model.FullName;

            var existingUser = await _context.Users.FirstOrDefaultAsync(x => x.Email == model.Email && x.Id != user.Id);

            if (existingUser != null)
            {
                ModelState.AddModelError("Email", "An account exists with this email.");
                return View(model);
            }

            if(model.NewAvatar != null &&  model.NewAvatar.Length > 0)
            {
                var uploadsFolder = Path.Combine("wwwroot/uploads");
                Directory.CreateDirectory(uploadsFolder);

                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(model.NewAvatar.FileName);
                var filePath = Path.Combine(uploadsFolder, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await model.NewAvatar.CopyToAsync(stream);
                }

                user.AvatarUrl = "uploads" + fileName;
            }

            await _context.SaveChangesAsync();

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.FullName ?? user.Email),
                new Claim("AvatarUrl", user.AvatarUrl ?? "/images/Avatar1.svg")
            };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));

            TempData["Success"] = "Profile updated succesfully";
            return RedirectToAction("Edit");

        }

    }
}

using ClimbingApplication.Context;
using ClimbingApplication.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace ClimbingApplication.Controllers
{
    public class AccountController : Controller
    {
        private readonly EFContextcs _context;

        public AccountController(EFContextcs context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _context.Felhasznalok.FirstOrDefaultAsync(f => f.email == model.Email && f.jelszo == model.Jelszo);

            if (user == null)
            {
                ModelState.AddModelError("", "Hibás email vagy jelszó");
                return View(model);
            }

            //azonosító + szerpkör

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.email),
                new Claim("UserId", user.ID.ToString()),
                new Claim(ClaimTypes.Role, user.rang)
            };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }
        /*
        public IActionResult AccesDenied() 
        {
            return View(); 
        }*/
    }
}

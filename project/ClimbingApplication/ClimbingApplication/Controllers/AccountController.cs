using ClimbingApplication.Context;
using ClimbingApplication.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;

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
        //Bejelentekzes megvalositasa
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _context.Felhasznalok.FirstOrDefaultAsync(f => f.email == model.Email);

            if (user == null)
            {
                ModelState.AddModelError("jelszo", "Hibás email vagy jelszó");
                return View(model);
            }

            var haser = new PasswordHasher<Felhasznalok>();

            bool isHased = user.jelszo.StartsWith("AQAA"); //Tipikus hash-elt jelszó fejléc nem része a jelszónak

            if (isHased)
            {
                //Hashelt jelszó ellenőrzése
                var res = haser.VerifyHashedPassword(user, user.jelszo, model.Jelszo);
                if (res != PasswordVerificationResult.Success)
                {
                    ModelState.AddModelError("jelszo", "Hibás email vagy jelszó");
                    return View(model);
                }
            }
            else
            {
                //Régi jelszavak hashelése
                if (user.jelszo != model.Jelszo)
                {
                    ModelState.AddModelError("jelszo", "Hibás email vagy jelszó");
                    return View(model);
                }
                user.jelszo = haser.HashPassword(user, model.Jelszo);
                _context.Update(user);
                await _context.SaveChangesAsync();
            }

            //azonosító + szerpkör sikeres bejelentkezés után

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.felhasznaloNev),
                new Claim("UserId", user.ID.ToString()),
                new Claim(ClaimTypes.Role, user.rang)
            };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        //Regisztracio megvalositasa
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterView model)
        {
            //Datum ellenorzese, hogy nem-e jovobeli
            if (model.SzuletesiIdo > DateOnly.FromDateTime(DateTime.Today))
            {
                ModelState.AddModelError("SzuletesiIdo", "A születési idő nem lehet jövőbeli");

            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            //email ellenőrzés
            if (await _context.Felhasznalok.AnyAsync(f => f.email == model.Email))
            {
                ModelState.AddModelError("Email", "Ezzel az emil-lel már reisztráltak");
                return View(model);
            }
            //felahsználónév ellenőrzése
            if (await _context.Felhasznalok.AnyAsync(f => f.felhasznaloNev == model.FelhasznaloNev))
            {
                ModelState.AddModelError("FelhasznaloNev", "Ez a felhasználónév már foglalt kérjük válasz egy másikat");
                return View(model);
            }

            var user = new Felhasznalok
            {
                vezetekNev = model.VezetekNev,
                keresztNev = model.KeresztNev,
                email = model.Email,
                szuletesiIdo = model.SzuletesiIdo,
                telefonszam = model.Telefonszam,
                rang = "user",
                felhasznaloNev = model.FelhasznaloNev

            };

            //jelszó hash-elése
            var hasher = new PasswordHasher<Felhasznalok>();
            user.jelszo = hasher.HashPassword(user, model.Jelszo);

            _context.Felhasznalok.Add(user);
            await _context.SaveChangesAsync();

            //claimsek létrehozása azaz bejelentkezés
            var claims = new List<Claim>
            {
                new Claim (ClaimTypes.Name, model.FelhasznaloNev),
                new Claim("UserId", user.ID.ToString()),
                new Claim(ClaimTypes.Role, user.rang)
            };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);

            //belépés a cookiba
            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                principal,
                new AuthenticationProperties
                {
                    IsPersistent = true,
                    ExpiresUtc = DateTimeOffset.UtcNow.AddHours(4)
                }
            );

            return RedirectToAction("Index", "Home");

        }

        [HttpGet]
        public IActionResult ChangePassword()
        {
            return View();
        }
        //Jelszovaltoztatas
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(ChangePasswordView model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            //felhasznalo ellenozese
            var userId = User.Claims.FirstOrDefault(c => c.Type == "UserId")?.Value;
            if (userId == null)
            {
                return Unauthorized();
            }


            var user = await _context.Felhasznalok.FindAsync(int.Parse(userId));

            if (user == null)
            {
                return NotFound();

            }

            var hasher = new PasswordHasher<Felhasznalok>();

            //Régi jelszó ellenőrzése
            var res = hasher.VerifyHashedPassword(user, user.jelszo, model.JelenlegiJelszo);
            if (res != PasswordVerificationResult.Success)
            {
                ModelState.AddModelError("JelenlegiJelszo", "Hibás a jelenlegi jelszo");
                return View(model);
            }

            //Új jelszó beállítása
            user.jelszo = hasher.HashPassword(user, model.UjJelszo);
            _context.Felhasznalok.Update(user);
            await _context.SaveChangesAsync();

            TempData["Message"] = "Sikeres jelszómódosítás";

            return RedirectToAction("Index", "Home");

        }

        //Kijelentkezes
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }

    }
}

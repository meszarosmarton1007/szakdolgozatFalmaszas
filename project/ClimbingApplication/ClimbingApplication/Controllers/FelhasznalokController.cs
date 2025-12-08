using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ClimbingApplication.Context;
using ClimbingApplication.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace ClimbingApplication.Controllers
{
    //ez az osztály csak az admin által a felhasználón végzett műveleteket hajtja végre. A Regisztráció, bejelentkezés, kijelentkezés, jelszómódositás az AccountControllerben van

    [Authorize(Roles = "admin")]
    public class FelhasznalokController : Controller
    {
        private readonly EFContextcs _context;

        public FelhasznalokController(EFContextcs context)
        {
            _context = context;
        }

        // GET: Felhasznalok
        public async Task<IActionResult> Index()
        {
            return View(await _context.Felhasznalok.ToListAsync());
        }

        // GET: Felhasznalok/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var felhasznalok = await _context.Felhasznalok
                .FirstOrDefaultAsync(m => m.ID == id);
            if (felhasznalok == null)
            {
                return NotFound();
            }

            return View(felhasznalok);
        }

        // GET: Felhasznalok/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Felhasznalok/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //Admin altal torteno felhasznalohozzaadas
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,vezetekNev,keresztNev,email,jelszo,szuletesiIdo,telefonszam,rang,felhasznaloNev")] Felhasznalok felhasznalok)
        {
            if(_context.Felhasznalok.Any(f => f.felhasznaloNev == felhasznalok.felhasznaloNev))
            {
                ModelState.AddModelError("felhasznaloNev", "Ez a név már foglalt! Kérem adj meg egy másik felhasználónevet!");
            }
            
            if (ModelState.IsValid)
            {
                _context.Add(felhasznalok);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(felhasznalok);
        }

        // GET: Felhasznalok/Edit/5
        //Admin altal torteno felhasznalo szerkesztes
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var felhasznalok = await _context.Felhasznalok.FindAsync(id);
            if (felhasznalok == null)
            {
                return NotFound();
            }
            var model = new FelhasznaloEdit
            {
                ID = felhasznalok.ID,
                vezetekNev = felhasznalok.vezetekNev,
                KeresztNev = felhasznalok.keresztNev,
                email = felhasznalok.email,
                telefonszam = felhasznalok.telefonszam,
                szuletesiIdo = felhasznalok.szuletesiIdo,
                rang = felhasznalok.rang,
                felhasznaloNev = felhasznalok.felhasznaloNev,
                ujJelszo = string.Empty
               

            };

            return View(model);
        }

        // POST: Felhasznalok/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //Admin altal torteno felhasznalo szerkesztes
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, FelhasznaloEdit model)
        {

            if(id != model.ID)
            {
                return NotFound();
            }

            if(!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _context.Felhasznalok.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            user.vezetekNev = model.vezetekNev;
            user.keresztNev = model.KeresztNev;
            user.email = model.email;
            user.telefonszam = model.telefonszam;
            user.szuletesiIdo = model.szuletesiIdo;
            user.rang = model.rang;
            user.felhasznaloNev = model.felhasznaloNev;

            //ha van jelszómódosítás
            if (!string.IsNullOrWhiteSpace(model.ujJelszo))
            {
                var hasher = new PasswordHasher<Felhasznalok>();
                user.jelszo = hasher.HashPassword(user, model.ujJelszo);
            }

            _context.Update(user);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // GET: Felhasznalok/Delete/5
        //Admin altal torteno felhasznalo torles
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var felhasznalok = await _context.Felhasznalok
                .FirstOrDefaultAsync(m => m.ID == id);
            if (felhasznalok == null)
            {
                return NotFound();
            }

            return View(felhasznalok);
        }

        // POST: Felhasznalok/Delete/5
        //Felhasznalo torlesi szabalyok, nincs torles, csak frissites
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            //1-es id felhasználó törlését nem engedjük
            if(id == 1)
            {
                return RedirectToAction(nameof(Index));
            }

            var felhasznalok = await _context.Felhasznalok.FindAsync(id);
            if (felhasznalok != null)
            {
                felhasznalok.vezetekNev = "Törölt";
                felhasznalok.keresztNev = "Törölt";
                felhasznalok.email = "torolt@torolt.com";
                //Üres jelszó, hogy ne lehessen belépni
                felhasznalok.jelszo = "";
                felhasznalok.szuletesiIdo = new DateOnly(1990, 1, 1);
                felhasznalok.telefonszam = "+3612345678910";
                felhasznalok.rang = "user";
                //Egyedi felhasználónév
                felhasznalok.felhasznaloNev = "törölt_" + id;

                //db frissítése, mivel nem történt valóditörlés
                _context.Felhasznalok.Update(felhasznalok);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FelhasznalokExists(int id)
        {
            return _context.Felhasznalok.Any(e => e.ID == id);
        }
    }
}

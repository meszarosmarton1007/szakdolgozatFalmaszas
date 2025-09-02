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

namespace ClimbingApplication.Controllers
{
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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, FelhasznaloEdit model)
        {
           /* if (_context.Felhasznalok.Any(f => f.felhasznaloNev == felhasznalok.felhasznaloNev && f.ID != felhasznalok.ID))
            {
                ModelState.AddModelError("felhasznalonev", "Ez a név már foglalt! Kérem adj meg egy másik felhasználónevet!");
            }
            
            
            if (id != felhasznalok.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(felhasznalok);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FelhasznalokExists(felhasznalok.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(felhasznalok);*/

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
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var felhasznalok = await _context.Felhasznalok.FindAsync(id);
            if (felhasznalok != null)
            {
                _context.Felhasznalok.Remove(felhasznalok);
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

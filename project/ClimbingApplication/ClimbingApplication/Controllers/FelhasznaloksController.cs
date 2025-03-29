using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ClimbingApplication.Context;
using ClimbingApplication.Models;

namespace ClimbingApplication.Controllers
{
    public class FelhasznaloksController : Controller
    {
        private readonly EFContextcs _context;

        public FelhasznaloksController(EFContextcs context)
        {
            _context = context;
        }

        // GET: Felhasznaloks
        public async Task<IActionResult> Index()
        {
            var eFContextcs = _context.Felhasznalok.Include(f => f.HelyHozzaado).Include(f => f.Hozzaszolo);
            return View(await eFContextcs.ToListAsync());
        }

        // GET: Felhasznaloks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var felhasznalok = await _context.Felhasznalok
                .Include(f => f.HelyHozzaado)
                .Include(f => f.Hozzaszolo)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (felhasznalok == null)
            {
                return NotFound();
            }

            return View(felhasznalok);
        }

        // GET: Felhasznaloks/Create
        public IActionResult Create()
        {
            ViewData["HelyID"] = new SelectList(_context.FalmaszoHelyek, "ID", "cim");
            ViewData["HozzaszolasokID"] = new SelectList(_context.Hozzaszolasok, "ID", "hozzaszolas");
            return View();
        }

        // POST: Felhasznaloks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,vezetekNev,keresztNev,email,jelszo,szuletesiIdo,telefonszam,rang,HelyID,HozzaszolasokID,ValaszokID")] Felhasznalok felhasznalok)
        {
            if (ModelState.IsValid)
            {
                _context.Add(felhasznalok);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["HelyID"] = new SelectList(_context.FalmaszoHelyek, "ID", "cim", felhasznalok.HelyID);
            ViewData["HozzaszolasokID"] = new SelectList(_context.Hozzaszolasok, "ID", "hozzaszolas", felhasznalok.HozzaszolasokID);
            return View(felhasznalok);
        }

        // GET: Felhasznaloks/Edit/5
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
            ViewData["HelyID"] = new SelectList(_context.FalmaszoHelyek, "ID", "cim", felhasznalok.HelyID);
            ViewData["HozzaszolasokID"] = new SelectList(_context.Hozzaszolasok, "ID", "hozzaszolas", felhasznalok.HozzaszolasokID);
            return View(felhasznalok);
        }

        // POST: Felhasznaloks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,vezetekNev,keresztNev,email,jelszo,szuletesiIdo,telefonszam,rang,HelyID,HozzaszolasokID,ValaszokID")] Felhasznalok felhasznalok)
        {
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
            ViewData["HelyID"] = new SelectList(_context.FalmaszoHelyek, "ID", "cim", felhasznalok.HelyID);
            ViewData["HozzaszolasokID"] = new SelectList(_context.Hozzaszolasok, "ID", "hozzaszolas", felhasznalok.HozzaszolasokID);
            return View(felhasznalok);
        }

        // GET: Felhasznaloks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var felhasznalok = await _context.Felhasznalok
                .Include(f => f.HelyHozzaado)
                .Include(f => f.Hozzaszolo)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (felhasznalok == null)
            {
                return NotFound();
            }

            return View(felhasznalok);
        }

        // POST: Felhasznaloks/Delete/5
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

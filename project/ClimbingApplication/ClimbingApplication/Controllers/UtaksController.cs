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
    public class UtaksController : Controller
    {
        private readonly EFContextcs _context;

        public UtaksController(EFContextcs context)
        {
            _context = context;
        }

        // GET: Utaks
        public async Task<IActionResult> Index()
        {
            var eFContextcs = _context.Utak.Include(u => u.Hozzaszolas);
            return View(await eFContextcs.ToListAsync());
        }

        // GET: Utaks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var utak = await _context.Utak
                .Include(u => u.Hozzaszolas)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (utak == null)
            {
                return NotFound();
            }

            return View(utak);
        }

        // GET: Utaks/Create
        public IActionResult Create()
        {
            ViewData["HozzaszolasokID"] = new SelectList(_context.Hozzaszolasok, "ID", "hozzaszolas");
            return View();
        }

        // POST: Utaks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,kep,nehezseg,nev,leiras,letrehozva,HozzaszolasokID")] Utak utak)
        {
            if (ModelState.IsValid)
            {
                _context.Add(utak);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["HozzaszolasokID"] = new SelectList(_context.Hozzaszolasok, "ID", "hozzaszolas", utak.HozzaszolasokID);
            return View(utak);
        }

        // GET: Utaks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var utak = await _context.Utak.FindAsync(id);
            if (utak == null)
            {
                return NotFound();
            }
            ViewData["HozzaszolasokID"] = new SelectList(_context.Hozzaszolasok, "ID", "hozzaszolas", utak.HozzaszolasokID);
            return View(utak);
        }

        // POST: Utaks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,kep,nehezseg,nev,leiras,letrehozva,HozzaszolasokID")] Utak utak)
        {
            if (id != utak.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(utak);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UtakExists(utak.ID))
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
            ViewData["HozzaszolasokID"] = new SelectList(_context.Hozzaszolasok, "ID", "hozzaszolas", utak.HozzaszolasokID);
            return View(utak);
        }

        // GET: Utaks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var utak = await _context.Utak
                .Include(u => u.Hozzaszolas)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (utak == null)
            {
                return NotFound();
            }

            return View(utak);
        }

        // POST: Utaks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var utak = await _context.Utak.FindAsync(id);
            if (utak != null)
            {
                _context.Utak.Remove(utak);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UtakExists(int id)
        {
            return _context.Utak.Any(e => e.ID == id);
        }
    }
}

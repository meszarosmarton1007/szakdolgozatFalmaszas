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
    public class FalaksController : Controller
    {
        private readonly EFContextcs _context;

        public FalaksController(EFContextcs context)
        {
            _context = context;
        }

        // GET: Falaks
        public async Task<IActionResult> Index()
        {
            var eFContextcs = _context.Falak.Include(f => f.Falonut);
            return View(await eFContextcs.ToListAsync());
        }

        // GET: Falaks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var falak = await _context.Falak
                .Include(f => f.Falonut)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (falak == null)
            {
                return NotFound();
            }

            return View(falak);
        }

        // GET: Falaks/Create
        public IActionResult Create()
        {
            ViewData["FalID"] = new SelectList(_context.Utak, "ID", "kep");
            return View();
        }

        // POST: Falaks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,nev,kep,letrehozva,FalID")] Falak falak)
        {
            if (ModelState.IsValid)
            {
                _context.Add(falak);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FalID"] = new SelectList(_context.Utak, "ID", "kep", falak.FalID);
            return View(falak);
        }

        // GET: Falaks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var falak = await _context.Falak.FindAsync(id);
            if (falak == null)
            {
                return NotFound();
            }
            ViewData["FalID"] = new SelectList(_context.Utak, "ID", "kep", falak.FalID);
            return View(falak);
        }

        // POST: Falaks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,nev,kep,letrehozva,FalID")] Falak falak)
        {
            if (id != falak.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(falak);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FalakExists(falak.ID))
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
            ViewData["FalID"] = new SelectList(_context.Utak, "ID", "kep", falak.FalID);
            return View(falak);
        }

        // GET: Falaks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var falak = await _context.Falak
                .Include(f => f.Falonut)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (falak == null)
            {
                return NotFound();
            }

            return View(falak);
        }

        // POST: Falaks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var falak = await _context.Falak.FindAsync(id);
            if (falak != null)
            {
                _context.Falak.Remove(falak);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FalakExists(int id)
        {
            return _context.Falak.Any(e => e.ID == id);
        }
    }
}

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
    public class FalmaszoHelyeksController : Controller
    {
        private readonly EFContextcs _context;

        public FalmaszoHelyeksController(EFContextcs context)
        {
            _context = context;
        }

        // GET: FalmaszoHelyeks
        public async Task<IActionResult> Index()
        {
            var eFContextcs = _context.FalmaszoHelyek.Include(f => f.Falnev);
            return View(await eFContextcs.ToListAsync());
        }

        // GET: FalmaszoHelyeks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var falmaszoHelyek = await _context.FalmaszoHelyek
                .Include(f => f.Falnev)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (falmaszoHelyek == null)
            {
                return NotFound();
            }

            return View(falmaszoHelyek);
        }

        // GET: FalmaszoHelyeks/Create
        public IActionResult Create()
        {
            ViewData["FalID"] = new SelectList(_context.Falak, "ID", "kep");
            return View();
        }

        // POST: FalmaszoHelyeks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,orszag,cim,honlap,koordinata,leiras,FalID")] FalmaszoHelyek falmaszoHelyek)
        {
            if (ModelState.IsValid)
            {
                _context.Add(falmaszoHelyek);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FalID"] = new SelectList(_context.Falak, "ID", "kep", falmaszoHelyek.FalID);
            return View(falmaszoHelyek);
        }

        // GET: FalmaszoHelyeks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var falmaszoHelyek = await _context.FalmaszoHelyek.FindAsync(id);
            if (falmaszoHelyek == null)
            {
                return NotFound();
            }
            ViewData["FalID"] = new SelectList(_context.Falak, "ID", "kep", falmaszoHelyek.FalID);
            return View(falmaszoHelyek);
        }

        // POST: FalmaszoHelyeks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,orszag,cim,honlap,koordinata,leiras,FalID")] FalmaszoHelyek falmaszoHelyek)
        {
            if (id != falmaszoHelyek.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(falmaszoHelyek);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FalmaszoHelyekExists(falmaszoHelyek.ID))
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
            ViewData["FalID"] = new SelectList(_context.Falak, "ID", "kep", falmaszoHelyek.FalID);
            return View(falmaszoHelyek);
        }

        // GET: FalmaszoHelyeks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var falmaszoHelyek = await _context.FalmaszoHelyek
                .Include(f => f.Falnev)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (falmaszoHelyek == null)
            {
                return NotFound();
            }

            return View(falmaszoHelyek);
        }

        // POST: FalmaszoHelyeks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var falmaszoHelyek = await _context.FalmaszoHelyek.FindAsync(id);
            if (falmaszoHelyek != null)
            {
                _context.FalmaszoHelyek.Remove(falmaszoHelyek);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FalmaszoHelyekExists(int id)
        {
            return _context.FalmaszoHelyek.Any(e => e.ID == id);
        }
    }
}

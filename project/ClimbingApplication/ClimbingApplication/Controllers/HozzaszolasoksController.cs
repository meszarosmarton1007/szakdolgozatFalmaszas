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
    public class HozzaszolasoksController : Controller
    {
        private readonly EFContextcs _context;

        public HozzaszolasoksController(EFContextcs context)
        {
            _context = context;
        }

        // GET: Hozzaszolasoks
        public async Task<IActionResult> Index()
        {
            return View(await _context.Hozzaszolasok.ToListAsync());
        }

        // GET: Hozzaszolasoks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hozzaszolasok = await _context.Hozzaszolasok
                .FirstOrDefaultAsync(m => m.ID == id);
            if (hozzaszolasok == null)
            {
                return NotFound();
            }

            return View(hozzaszolasok);
        }

        // GET: Hozzaszolasoks/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Hozzaszolasoks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,hozzaszolas,ValaszokID")] Hozzaszolasok hozzaszolasok)
        {
            if (ModelState.IsValid)
            {
                _context.Add(hozzaszolasok);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(hozzaszolasok);
        }

        // GET: Hozzaszolasoks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hozzaszolasok = await _context.Hozzaszolasok.FindAsync(id);
            if (hozzaszolasok == null)
            {
                return NotFound();
            }
            return View(hozzaszolasok);
        }

        // POST: Hozzaszolasoks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,hozzaszolas,ValaszokID")] Hozzaszolasok hozzaszolasok)
        {
            if (id != hozzaszolasok.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(hozzaszolasok);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HozzaszolasokExists(hozzaszolasok.ID))
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
            return View(hozzaszolasok);
        }

        // GET: Hozzaszolasoks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hozzaszolasok = await _context.Hozzaszolasok
                .FirstOrDefaultAsync(m => m.ID == id);
            if (hozzaszolasok == null)
            {
                return NotFound();
            }

            return View(hozzaszolasok);
        }

        // POST: Hozzaszolasoks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var hozzaszolasok = await _context.Hozzaszolasok.FindAsync(id);
            if (hozzaszolasok != null)
            {
                _context.Hozzaszolasok.Remove(hozzaszolasok);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HozzaszolasokExists(int id)
        {
            return _context.Hozzaszolasok.Any(e => e.ID == id);
        }
    }
}

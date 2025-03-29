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
    public class ValaszoksController : Controller
    {
        private readonly EFContextcs _context;

        public ValaszoksController(EFContextcs context)
        {
            _context = context;
        }

        // GET: Valaszoks
        public async Task<IActionResult> Index()
        {
            return View(await _context.Valaszok.ToListAsync());
        }

        // GET: Valaszoks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var valaszok = await _context.Valaszok
                .FirstOrDefaultAsync(m => m.ID == id);
            if (valaszok == null)
            {
                return NotFound();
            }

            return View(valaszok);
        }

        // GET: Valaszoks/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Valaszoks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,valasz")] Valaszok valaszok)
        {
            if (ModelState.IsValid)
            {
                _context.Add(valaszok);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(valaszok);
        }

        // GET: Valaszoks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var valaszok = await _context.Valaszok.FindAsync(id);
            if (valaszok == null)
            {
                return NotFound();
            }
            return View(valaszok);
        }

        // POST: Valaszoks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,valasz")] Valaszok valaszok)
        {
            if (id != valaszok.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(valaszok);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ValaszokExists(valaszok.ID))
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
            return View(valaszok);
        }

        // GET: Valaszoks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var valaszok = await _context.Valaszok
                .FirstOrDefaultAsync(m => m.ID == id);
            if (valaszok == null)
            {
                return NotFound();
            }

            return View(valaszok);
        }

        // POST: Valaszoks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var valaszok = await _context.Valaszok.FindAsync(id);
            if (valaszok != null)
            {
                _context.Valaszok.Remove(valaszok);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ValaszokExists(int id)
        {
            return _context.Valaszok.Any(e => e.ID == id);
        }
    }
}

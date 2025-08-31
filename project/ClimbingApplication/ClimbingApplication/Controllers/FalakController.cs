using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ClimbingApplication.Context;
using ClimbingApplication.Models;
using System.Security.Claims;

namespace ClimbingApplication.Controllers
{
    public class FalakController : Controller
    {
        private readonly EFContextcs _context;

        public FalakController(EFContextcs context)
        {
            _context = context;
        }

        // GET: Falak
        public async Task<IActionResult> Index()
        {
            var eFContextcs = _context.Falak
                .Include(f => f.Falhelye)
                .Include(f => f.Letrehozo);
            return View(await eFContextcs.ToListAsync());
        }

        // GET: Falak/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var falak = await _context.Falak
                .Include(f => f.Falhelye)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (falak == null)
            {
                return NotFound();
            }

            return View(falak);
        }

        // GET: Falak/Create
        public IActionResult Create()
        {
            ViewData["FalmaszohelyID"] = new SelectList(_context.FalmaszoHelyek, "ID", "cim");
            return View();
        }

        // POST: Falak/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,nev,kep,letrehozva,FalmaszohelyID")] Falak falak)
        {
            if (ModelState.IsValid)
            {
                var userIdStr = User.FindFirstValue("userId");

                if (string.IsNullOrEmpty(userIdStr))
                {
                    return Unauthorized();
                }

                falak.FelhasznaloID = int.Parse(userIdStr);

                _context.Add(falak);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FalmaszohelyID"] = new SelectList(_context.FalmaszoHelyek, "ID", "cim", falak.FalmaszohelyID);
            return View(falak);
        }

        // GET: Falak/Edit/5
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
            ViewData["FalmaszohelyID"] = new SelectList(_context.FalmaszoHelyek, "ID", "cim", falak.FalmaszohelyID);
            return View(falak);
        }

        // POST: Falak/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,nev,kep,letrehozva,FalmaszohelyID")] Falak falak)
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
            ViewData["FalmaszohelyID"] = new SelectList(_context.FalmaszoHelyek, "ID", "cim", falak.FalmaszohelyID);
            return View(falak);
        }

        // GET: Falak/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var falak = await _context.Falak
                .Include(f => f.Falhelye)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (falak == null)
            {
                return NotFound();
            }

            return View(falak);
        }

        // POST: Falak/Delete/5
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

        //utak listázása falak alapján
        public async Task<IActionResult> Utak(int id)
        {
            var fal = await _context.Falak
                .Include(f => f.Falhelye)
                .FirstOrDefaultAsync(f => f.ID == id);

            if (fal == null)
            {
                return NotFound();
            }

            var utak = await _context.Utak
                .Include(u => u.Falonut)
                .Include(u => u.UtLetrehozo)
                .Where(u => u.FalID == id)
                .ToListAsync();

            ViewData["FalNev"] = fal.nev;
            ViewData["FalID"] = fal.ID;

            return View("../Utak/Index", utak);
        }
    }
}

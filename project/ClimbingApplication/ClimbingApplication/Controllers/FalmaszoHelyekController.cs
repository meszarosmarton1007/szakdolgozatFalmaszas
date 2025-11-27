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
using Microsoft.AspNetCore.Authorization;

namespace ClimbingApplication.Controllers
{
    public class FalmaszoHelyekController : Controller
    {
        private readonly EFContextcs _context;

        

        public FalmaszoHelyekController(EFContextcs context)
        {
            _context = context;
        }
        
        public async Task<IActionResult> Falak(int falmaszohelyId)
        {
            var falak = await _context.Falak
                .Include(f => f.Falhelye)
               .Include(f => f.Letrehozo)
               .Where(f => f.FalmaszohelyID == falmaszohelyId).ToListAsync();

            if (!falak.Any())
            {
                return NotFound();
            }

            ViewBag.FalmaszohelyId = falmaszohelyId;
            return View("~/Views/Falak/Index.cshtml", falak);
        }
        
        // GET: FalmaszoHelyek
        public async Task<IActionResult> Index()
        {
            var eFContextcs = _context.FalmaszoHelyek
                .Include(f => f.Hozzaado)
                .Include(f => f.Falak);
            return View(await eFContextcs.ToListAsync());
        }

        // GET: FalmaszoHelyek/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var falmaszoHelyek = await _context.FalmaszoHelyek
                .Include(f => f.Hozzaado)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (falmaszoHelyek == null)
            {
                return NotFound();
            }

            return View(falmaszoHelyek);
        }

        // GET: FalmaszoHelyek/Create
        [Authorize]
        public IActionResult Create()
        {
            ViewData["FelhasznalokID"] = new SelectList(_context.Felhasznalok, "ID", "email");
            return View();
        }

        // POST: FalmaszoHelyek/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create([Bind("ID,orszag,cim,honlap,nev,leiras,FelhasznalokID")] FalmaszoHelyek falmaszoHelyek)
        {
            if (ModelState.IsValid)
            {
                var userIdStr = User.FindFirstValue("userId");

                if (string.IsNullOrEmpty(userIdStr))
                {
                    return Unauthorized();
                }

                falmaszoHelyek.FelhasznalokID = int.Parse(userIdStr);

                _context.Add(falmaszoHelyek);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FelhasznalokID"] = new SelectList(_context.Felhasznalok, "ID", "email", falmaszoHelyek.FelhasznalokID);
            return View(falmaszoHelyek);
        }

        // GET: FalmaszoHelyek/Edit/5
        [Authorize]
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
            ViewData["FelhasznalokID"] = new SelectList(_context.Felhasznalok, "ID", "email", falmaszoHelyek.FelhasznalokID);
            return View(falmaszoHelyek);
        }

        // POST: FalmaszoHelyek/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(int id, [Bind("ID,orszag,cim,honlap,nev,leiras,FelhasznalokID")] FalmaszoHelyek falmaszoHelyek)
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
            ViewData["FelhasznalokID"] = new SelectList(_context.Felhasznalok, "ID", "email", falmaszoHelyek.FelhasznalokID);
            return View(falmaszoHelyek);
        }

        // GET: FalmaszoHelyek/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var falmaszoHelyek = await _context.FalmaszoHelyek
                .Include(f => f.Hozzaado)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (falmaszoHelyek == null)
            {
                return NotFound();
            }

            return View(falmaszoHelyek);
        }

        // POST: FalmaszoHelyek/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
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

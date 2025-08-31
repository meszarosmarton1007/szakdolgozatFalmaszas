using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ClimbingApplication.Context;
using ClimbingApplication.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace ClimbingApplication.Controllers
{
    public class UtakController : Controller
    {
        private readonly EFContextcs _context;

        public UtakController(EFContextcs context)
        {
            _context = context;
        }

        // GET: Utak
        public async Task<IActionResult> Index()
        {
            var eFContextcs = _context.Utak
                .Include(u => u.Falonut)                //Falnak a neve
                .Include(u => u.UtLetrehozo)            //A mászó aki létrehozta
                .Include(u => u.Hozzaszolasoks)         //hozzászólás betöltése
                    .ThenInclude(h => h.UtHozzaszolo)   //aki a hozzászólást írta
                .Include(u => u.Hozzaszolasoks)         
                    .ThenInclude(h => h.Valaszok)       //válaszok betöltése
                        .ThenInclude(v => v.Valasziro); //a válasznak az írója
            return View(await eFContextcs.ToListAsync());

        }

        // GET: Utak/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var utak = await _context.Utak
                .Include(u => u.Falonut)
                .Include(U => U.UtLetrehozo)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (utak == null)
            {
                return NotFound();
            }

            return View(utak);
        }

        // GET: Utak/Create
        public IActionResult Create(int? falId)
        {
            if (falId.HasValue)
            {
                ViewData["FalID"] = falId; //new SelectList(_context.Falak, "ID", "nev");//ezt változtattam
            }
            else
            {
                ViewData["FalID"] = new SelectList(_context.Falak, "ID", "nev");
            }
            return View();
        }

        // POST: Utak/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,kep,nehezseg,nev,leiras,letrehozva,FalID")] Utak utak, int? falId)
        {
            if (ModelState.IsValid)
            {
                var userIdStr = User.FindFirstValue("userId");

                if (string.IsNullOrEmpty(userIdStr))
                {
                    return Unauthorized();
                }

                utak.FelhasznaloID = int.Parse(userIdStr);
                if (falId.HasValue)
                {
                    utak.FalID = falId.Value;
                }

                _context.Add(utak);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index), new {falId = falId});
            }
           // ViewData["FalID"] = new SelectList(_context.Falak, "ID", "nev", utak.FalID);
            return View(utak);
        }

        // GET: Utak/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var utak = await _context.Utak
                .Include(u => u.Falonut)
                .Include(u => u.UtLetrehozo)
                .FirstOrDefaultAsync(u => u.ID == id);
            if (utak == null)
            {
                return NotFound();
            }
            //ViewData["FalID"] = new SelectList(_context.Falak, "ID", "nev", utak.FalID);
            return View(utak);
        }

        // POST: Utak/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,kep,nehezseg,nev,leiras,letrehozva,FalID")] Utak utak)
        {
            if (id != utak.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var existingUt = await _context.Utak
                        .AsNoTracking()
                        .FirstOrDefaultAsync(u => u.ID == id);

                    if (existingUt == null)
                    {
                        return NotFound();
                    }

                    utak.FalID = existingUt.FalID;
                    utak.FelhasznaloID = existingUt.FelhasznaloID;
                    

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
                return RedirectToAction(nameof(Index), new {falId = utak.FalID});
            }
            //ViewData["FalID"] = new SelectList(_context.Falak, "ID", "nev", utak.FalID);
            return View(utak);
        }

        // GET: Utak/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var utak = await _context.Utak
                .Include(u => u.Falonut)
                .Include(u => u.UtLetrehozo)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (utak == null)
            {
                return NotFound();
            }

            return View(utak);
        }

        // POST: Utak/Delete/5
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

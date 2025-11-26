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
    public class ValaszokController : Controller
    {
        private readonly EFContextcs _context;

        public ValaszokController(EFContextcs context)
        {
            _context = context;
        }

        // GET: Valaszok
        [Authorize(Roles = "senki")]
        public async Task<IActionResult> Index()
        {
            var eFContextcs = _context.Valaszok.Include(v => v.Valasz).Include(v => v.Valasziro);
            return View(await eFContextcs.ToListAsync());
        }

        // GET: Valaszok/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var valaszok = await _context.Valaszok
                .Include(v => v.Valasz)
                .Include(v => v.Valasziro)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (valaszok == null)
            {
                return NotFound();
            }

            return View(valaszok);
        }

        // GET: Valaszok/Create
        [Authorize(Roles = "senki")]
        public IActionResult Create()
        {
            ViewData["HozzaszolasID"] = new SelectList(_context.Hozzaszolasok, "ID", "hozzaszolas");
            ViewData["FelhasznaloID"] = new SelectList(_context.Felhasznalok, "ID", "email");
            return View();
        }

        // POST: Valaszok/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "senki")]
        public async Task<IActionResult> Create([Bind("ID,valasz,HozzaszolasID,FelhasznaloID")] Valaszok valaszok)
        {
            if (ModelState.IsValid)
            {
                _context.Add(valaszok);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["HozzaszolasID"] = new SelectList(_context.Hozzaszolasok, "ID", "hozzaszolas", valaszok.HozzaszolasID);
            ViewData["FelhasznaloID"] = new SelectList(_context.Felhasznalok, "ID", "email", valaszok.FelhasznaloID);
            return View(valaszok);
        }

        //Saját válasz lérehozó, hogy az utaknál lehessen
        //létrehozni a választ a hozzászólás alatt

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> myCreate(int hozzaszolasId, string valasz)
        {
            var userId = int.Parse(User.FindFirstValue("userId"));

            var newReply = new Valaszok
            {
                HozzaszolasID = hozzaszolasId,
                FelhasznaloID = userId,
                valasz = valasz
            };

            _context.Add(newReply);
            await _context.SaveChangesAsync();

            var falId = await _context.Hozzaszolasok
                .Where(h => h.ID == hozzaszolasId)
                .Select(h => h.UtHozzaszolas.FalID)
                .FirstOrDefaultAsync();

            return RedirectToAction("Index", "Utak", new {falId});
        }

        //Saját válsz szerkesztése az utaknál valamit az admin mindent tud szerkeszteni

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> myEdit(int id, string valasz)
        {
            var repy = await _context.Valaszok.FindAsync(id);
            if (repy == null)
            {
                return NotFound();
            }

            var userId = int.Parse(User.FindFirstValue("UserId"));

            if(repy.FelhasznaloID == userId || User.IsInRole("admin"))
            {
                repy.valasz = valasz;
                _context.Update(repy);
                await _context.SaveChangesAsync();
            }

            var falId = await _context.Hozzaszolasok
                .Where(h => h.ID == repy.HozzaszolasID)
                .Select(h => h.UtHozzaszolas.FalID)
                .FirstOrDefaultAsync();

            return RedirectToAction("Index", "Utak", new {falId});
        }

        //saját válasz törlése valamint az admin mindent tud törölni

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> myDelete(int id)
        {
            var repy = await _context.Valaszok.FindAsync(id);

            if(repy == null)
            {
                return NotFound();
            }

            var userId = int.Parse(User.FindFirstValue("UserId"));

            if(repy.FelhasznaloID == userId || User.IsInRole("admin"))
            {
                _context.Valaszok.Remove(repy);
                await _context.SaveChangesAsync();
            }

            var falId = await _context.Hozzaszolasok
                .Where(h => h.ID == repy.HozzaszolasID)
                .Select(h => h.UtHozzaszolas.FalID)
                .FirstOrDefaultAsync();

            return RedirectToAction("Index", "Utak" , new {falId});
        }

        // GET: Valaszok/Edit/5
        [Authorize(Roles = "senki")]
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
            ViewData["HozzaszolasID"] = new SelectList(_context.Hozzaszolasok, "ID", "hozzaszolas", valaszok.HozzaszolasID);
            ViewData["FelhasznaloID"] = new SelectList(_context.Felhasznalok, "ID", "email", valaszok.FelhasznaloID);
            return View(valaszok);
        }

        // POST: Valaszok/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "senki")]
        public async Task<IActionResult> Edit(int id, [Bind("ID,valasz,HozzaszolasID,FelhasznaloID")] Valaszok valaszok)
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
            ViewData["HozzaszolasID"] = new SelectList(_context.Hozzaszolasok, "ID", "hozzaszolas", valaszok.HozzaszolasID);
            ViewData["FelhasznaloID"] = new SelectList(_context.Felhasznalok, "ID", "email", valaszok.FelhasznaloID);
            return View(valaszok);
        }

        // GET: Valaszok/Delete/5
        [Authorize(Roles = "senki")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var valaszok = await _context.Valaszok
                .Include(v => v.Valasz)
                .Include(v => v.Valasziro)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (valaszok == null)
            {
                return NotFound();
            }

            return View(valaszok);
        }

        // POST: Valaszok/Delete/5
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

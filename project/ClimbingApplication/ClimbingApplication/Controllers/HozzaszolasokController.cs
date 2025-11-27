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
    public class HozzaszolasokController : Controller
    {
        private readonly EFContextcs _context;

        public HozzaszolasokController(EFContextcs context)
        {
            _context = context;
        }

        // GET: Hozzaszolasok
        [Authorize(Roles = "senki")]
        public async Task<IActionResult> Index()
        {
            var eFContextcs = _context.Hozzaszolasok.Include(h => h.UtHozzaszolas).Include(h => h.UtHozzaszolo);
            return View(await eFContextcs.ToListAsync());
        }

        // GET: Hozzaszolasok/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hozzaszolasok = await _context.Hozzaszolasok
                .Include(h => h.UtHozzaszolas)
                .Include(h => h.UtHozzaszolo)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (hozzaszolasok == null)
            {
                return NotFound();
            }

            return View(hozzaszolasok);
        }

        // GET: Hozzaszolasok/Create
        [Authorize(Roles = "senki")]
        public IActionResult Create()
        {
            ViewData["UtakID"] = new SelectList(_context.Utak, "ID", "nev");
            ViewData["FelhasznaloID"] = new SelectList(_context.Felhasznalok, "ID", "email");
            return View();
        }

        // POST: Hozzaszolasok/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "senki")]
        public async Task<IActionResult> Create([Bind("ID,hozzaszolas,UtakID,FelhasznaloID")] Hozzaszolasok hozzaszolasok)
        {
            if (ModelState.IsValid)
            {
                _context.Add(hozzaszolasok);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UtakID"] = new SelectList(_context.Utak, "ID", "nev", hozzaszolasok.UtakID);
            ViewData["FelhasznaloID"] = new SelectList(_context.Felhasznalok, "ID", "email", hozzaszolasok.FelhasznaloID);
            return View(hozzaszolasok);
        }

        //Sajat create létrehozás ahhoz, hogy a hozzászólások
        //megfelelő helyen a megfelő módon legenek megjelenítve és létrehozva
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> myCreate(int utakId, string hozzaszolas)
        {
            var userId = int.Parse(User.FindFirstValue("UserId"));
            var newComment = new Hozzaszolasok
            {
                UtakID = utakId,
                FelhasznaloID = userId,
                hozzaszolas = hozzaszolas
            };

            _context.Add(newComment);
            await _context.SaveChangesAsync();

            var falId = await _context.Utak
                .Where(u => u.ID == utakId)
                .Select(u => u.FalID)
                .FirstOrDefaultAsync();

            return RedirectToAction("Index", "Utak", new {falId});
        }

        //Sajt comment szerkesztése az közvetlenül az utaknál
        //valamint admin mindent tud szerkeszteni

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> myEdit(int id, string hozzaszolas)
        {
            var comment = await _context.Hozzaszolasok.FindAsync(id);
            if(comment == null)
            {
                return NotFound();
            }

            var userId = int.Parse(User.FindFirstValue("UserId"));

            if(comment.FelhasznaloID == userId || User.IsInRole("admin"))
            {
                comment.hozzaszolas = hozzaszolas;
                _context.Update(comment);
                await _context.SaveChangesAsync();
            }

            var falId = await _context.Utak
               .Where(u => u.ID == comment.UtakID)
               .Select(u => u.FalID)
               .FirstOrDefaultAsync();

            return RedirectToAction("Index", "Utak", new {falId});
        }

        //Saját komment törlése valamint az admin mindet tud törölni az utaknál

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> myDelete(int id)
        {
            var comment = await _context.Hozzaszolasok.FindAsync(id);
            if (comment == null)
            {
                return NotFound();
            }

            var userId = int.Parse(User.FindFirstValue("UserId"));

            if(comment.FelhasznaloID == userId || User.IsInRole("admin"))
            {
                _context.Hozzaszolasok.Remove(comment);
                await _context.SaveChangesAsync();
            }
            var falId = await _context.Utak
                .Where(u => u.ID == comment.UtakID)
                .Select(u => u.FalID)
                .FirstOrDefaultAsync();
            return RedirectToAction("Index", "Utak" ,new {falId});
        }


        // GET: Hozzaszolasok/Edit/5
        [Authorize(Roles = "senki")]
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
            ViewData["UtakID"] = new SelectList(_context.Utak, "ID", "nev", hozzaszolasok.UtakID);
            ViewData["FelhasznaloID"] = new SelectList(_context.Felhasznalok, "ID", "email", hozzaszolasok.FelhasznaloID);
            return View(hozzaszolasok);
        }

        // POST: Hozzaszolasok/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "senki")]
        public async Task<IActionResult> Edit(int id, [Bind("ID,hozzaszolas,UtakID,FelhasznaloID")] Hozzaszolasok hozzaszolasok)
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
            ViewData["UtakID"] = new SelectList(_context.Utak, "ID", "nev", hozzaszolasok.UtakID);
            ViewData["FelhasznaloID"] = new SelectList(_context.Felhasznalok, "ID", "email", hozzaszolasok.FelhasznaloID);
            return View(hozzaszolasok);
        }

        // GET: Hozzaszolasok/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hozzaszolasok = await _context.Hozzaszolasok
                .Include(h => h.UtHozzaszolas)
                .Include(h => h.UtHozzaszolo)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (hozzaszolasok == null)
            {
                return NotFound();
            }

            return View(hozzaszolasok);
        }

        // POST: Hozzaszolasok/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
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

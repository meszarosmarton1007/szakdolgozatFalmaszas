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
    [Route("FalmaszoHelyek/Utak")]
    public class UtakController : Controller
    {
        private readonly EFContextcs _context;

        public UtakController(EFContextcs context)
        {
            _context = context;
        }

        // GET: Utak
        [HttpGet("{falId}")]
        public async Task<IActionResult> Index(int falId)
        {
            IQueryable<Utak> eFContextcs = _context.Utak
                .Include(u => u.Falonut)                //Falnak a neve
                .Include(u => u.UtLetrehozo)            //A mászó aki létrehozta
                .Include(u => u.Hozzaszolasoks)         //hozzászólás betöltése
                    .ThenInclude(h => h.UtHozzaszolo)   //aki a hozzászólást írta
                .Include(u => u.Hozzaszolasoks)         
                    .ThenInclude(h => h.Valaszok)       //válaszok betöltése
                        .ThenInclude(v => v.Valasziro); //a válasznak az írója
        
                eFContextcs = eFContextcs.Where(u => u.FalID == falId);
                

                var fal = await _context.Falak.AsNoTracking().FirstOrDefaultAsync(f => f.ID == falId);
                if (fal == null)
                {
                    return NotFound();
                }
                

                ViewBag.FalmaszohelyID = fal.Falhelye;
                ViewBag.FalId = falId;
            

            return View(await eFContextcs.ToListAsync());
            
        }

        // GET: Utak/Details/5
        [HttpGet("Utak/Details/{id}")]
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
            ViewBag.FalId = utak.FalID;
            return View(utak);
        }

        // GET: Utak/Create
        [HttpGet("Utak/Create/{falId?}")]
        public IActionResult Create(int falId)
        {
            /*  if (falId.HasValue)
              {
                  //ViewData["FalID"] = new SelectList(_context.Falak, "ID", "nev", falId.Value);
                  //ViewBag.FalId = falId.Value;
                  //var fal = _context.Falak.FirstOrDefault(f => f.ID == falId.Value);
                  if (falId.HasValue)
                  {
                      //ViewBag.FalNev = fal.nev;
                      ViewBag.FalId = falId.Value;
                  }

              }
              else
              {
                  ViewData["FalID"] = new SelectList(_context.Falak, "ID", "nev");
              }*/
            var ut = new Utak
            {
                FalID = falId,
                letrehozva = DateOnly.FromDateTime(DateTime.Now)
            };
            ViewBag.FalId = falId;
            return View(ut);
        }

        // POST: Utak/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost("Utak/Create/{falId}")]
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
                return RedirectToAction("Index", new {falId});
            }
            // ViewData["FalID"] = new SelectList(_context.Falak, "ID", "nev", utak.FalID);
            ViewBag.FalId = falId;
            return View(utak);
        }

        // GET: Utak/Edit/5
        [HttpGet("Utak/Edit/{id}")]
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
           ViewBag.FalId = utak.FalID;
            return View(utak);
        }

        // POST: Utak/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost("Utak/Edit/{id}")]
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

                    return RedirectToAction("Index", new { falId = utak.FalID });
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
                
            }
            //ViewData["FalID"] = new SelectList(_context.Falak, "ID", "nev", utak.FalID);
            ViewBag.FalID = utak.FalID;
            return View(utak);
        }

        // GET: Utak/Delete/5
        [HttpGet("Utak/Delete/{id}")]
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
            ViewBag.FalId = utak.FalID;
            return View(utak);
        }

        // POST: Utak/Delete/5
        [HttpPost("Utak/Delete/{id}"), ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var utak = await _context.Utak.FindAsync(id);
            if (utak != null)
            {
                _context.Utak.Remove(utak);
            }
            
            var falId = utak?.FalID;
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", new { falId });
        }

        private bool UtakExists(int id)
        {
            return _context.Utak.Any(e => e.ID == id);
        }
    }
}

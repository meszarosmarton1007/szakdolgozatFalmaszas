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
using Google.Apis.Auth.OAuth2;
using Google.Cloud.Storage.V1;
using System.Configuration;
using ClimbingApplication.Service;
using Microsoft.AspNetCore.Authorization;

namespace ClimbingApplication.Controllers
{
    public class FalakController : Controller
    {
        private readonly EFContextcs _context;
        private readonly IImageService _imageService;
        private readonly IConfiguration _configuration;

        public FalakController(EFContextcs context, IImageService imageService, IConfiguration configuration)
        {
            _context = context;
            _imageService = imageService;
            _configuration = configuration;
        }

        // GET: Falak
        public async Task<IActionResult> Index(int? falmaszohelyId)
        {

           var query = _context.Falak.Include(f => f.Falhelye).Include(f => f.Letrehozo).AsQueryable();

            if (falmaszohelyId.HasValue)
            {
                query = query.Where(f => f.Falhelye.ID == falmaszohelyId.Value);   
                ViewBag.FalmaszohelyId = falmaszohelyId.Value;

                var terem = await _context.FalmaszoHelyek
                    .AsNoTracking()
                    .FirstOrDefaultAsync(t => t.ID == falmaszohelyId);

                if (terem != null)
                {
                    ViewBag.TeremNev = terem.nev;
                }
            }
            return View(await query.ToListAsync());
            
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
        [Authorize]
        public IActionResult Create(int falmaszohelyId)
        {

            var fal = new Falak
            {
                FalmaszohelyID = falmaszohelyId,
                letrehozva = DateOnly.FromDateTime(DateTime.Now)

            };
            ViewBag.FalmaszohelyId = falmaszohelyId;
            return View(fal);
        }

        // POST: Falak/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create([Bind("ID,nev,kep,letrehozva,FalmaszohelyID")] Falak falak, int falmaszohelyId)
        {
            if (ModelState.IsValid)
            {
                var userIdStr = User.FindFirstValue("userId");

                if (string.IsNullOrEmpty(userIdStr))
                {
                    return Unauthorized();
                }

                falak.FelhasznaloID = int.Parse(userIdStr);
                falak.FalmaszohelyID = falmaszohelyId;

                _context.Add(falak);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index), new {falmaszohelyId = falak.FalmaszohelyID});
            }
            ViewBag.FalmaszohelyId = falmaszohelyId;
            return View(falak);
        }

        // GET: Falak/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id, int? falmaszohelyId)
        {
            if (id == null)
            {
                return NotFound();
            }

            var falak = await _context.Falak
                .Include(f => f.Falhelye)
                .Include(f => f.Letrehozo)
                .FirstOrDefaultAsync(f => f.ID == id);
            if (falak == null)
            {
                return NotFound();
            }
            ViewBag.Falhelye = falmaszohelyId ?? falak.FalmaszohelyID;
            return View(falak);
        }

        // POST: Falak/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(int id, [Bind("ID,nev,kep,letrehozva,FalmaszohelyID")] Falak falak, int? falmaszohelyId)
        {
            if (id != falak.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var existingFal = await _context.Falak
                        .AsNoTracking()
                        .FirstOrDefaultAsync(f => f.ID == id);

                    if (existingFal == null)
                    {
                        return NotFound();
                    }
                     falak.FalmaszohelyID = existingFal.FalmaszohelyID;
                    falak.FelhasznaloID = existingFal.FelhasznaloID;

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
                return RedirectToAction(nameof(Index), new { falmaszohelyId = falak.FalmaszohelyID });
            }
            ViewBag.FalmaszohelyId = falmaszohelyId ?? falak.FalmaszohelyID;
            return View(falak);
        }

        // GET: Falak/Delete/5
        [Authorize]
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
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var falak = await _context.Falak.FindAsync(id);
            //Kep torlesi logika
           if (falak != null)
            {
                if (!string.IsNullOrEmpty(falak.kep))
                {
                    try
                    {
                      await _imageService.DeleteImageAsync(falak.kep);
                    }
                    catch (Google.GoogleApiException ex)
                    {
                        Console.WriteLine($"A kép nem létezik: {ex.Message}");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Hiba a törlés során: {ex.Message}");
                    }
                }

                _context.Falak.Remove(falak);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index), new {falmaszohelyId = falak.FalmaszohelyID});
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
                .Include(u =>u.Hozzaszolasoks)
                .ThenInclude(k => k.UtHozzaszolo)
                .Include(u => u.Hozzaszolasoks)
                .ThenInclude(k => k.Valaszok)
                .ThenInclude(v => v.Valasziro)
                .Where(u => u.FalID == id)
                .ToListAsync();

            ViewData["FalNev"] = fal.nev;
            ViewData["FalID"] = fal.ID;
            
            return View("../Utak/Index", utak);
        }
    }
}

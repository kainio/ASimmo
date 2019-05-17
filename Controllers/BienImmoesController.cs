using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ASimmo.Data;
using ASimmo.Models;

namespace ASimmo.Controllers
{
    public class BienImmoesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BienImmoesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: BienImmoes
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.BiensImmos.Include(b => b.Adresse).Include(b => b.Classification).Include(b => b.Type);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: BienImmoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bienImmo = await _context.BiensImmos
                .Include(b => b.Adresse)
                .Include(b => b.Classification)
                .Include(b => b.Type)
                .FirstOrDefaultAsync(m => m.BienImmoId == id);
            if (bienImmo == null)
            {
                return NotFound();
            }

            return View(bienImmo);
        }

        // GET: BienImmoes/Create
        public IActionResult Create()
        {
            ViewData["AdresseId"] = new SelectList(_context.Adresses, "AdresseId", "AdresseId");
            ViewData["ClassificationId"] = new SelectList(_context.Classifications, "ClassificationId", "ClassificationId");
            ViewData["TypeId"] = new SelectList(_context.TypesBiensImmos, "TypeBienImmoId", "TypeBienImmoId");
            return View();
        }

        // POST: BienImmoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BienImmoId,Prix,Surface,NombreChambre,TypeId,ClassificationId,AdresseId")] BienImmo bienImmo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bienImmo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AdresseId"] = new SelectList(_context.Adresses, "AdresseId", "AdresseId", bienImmo.AdresseId);
            ViewData["ClassificationId"] = new SelectList(_context.Classifications, "ClassificationId", "ClassificationId", bienImmo.ClassificationId);
            ViewData["TypeId"] = new SelectList(_context.TypesBiensImmos, "TypeBienImmoId", "TypeBienImmoId", bienImmo.TypeId);
            return View(bienImmo);
        }

        // GET: BienImmoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bienImmo = await _context.BiensImmos.FindAsync(id);
            if (bienImmo == null)
            {
                return NotFound();
            }
            ViewData["AdresseId"] = new SelectList(_context.Adresses, "AdresseId", "AdresseId", bienImmo.AdresseId);
            ViewData["ClassificationId"] = new SelectList(_context.Classifications, "ClassificationId", "ClassificationId", bienImmo.ClassificationId);
            ViewData["TypeId"] = new SelectList(_context.TypesBiensImmos, "TypeBienImmoId", "TypeBienImmoId", bienImmo.TypeId);
            return View(bienImmo);
        }

        // POST: BienImmoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BienImmoId,Prix,Surface,NombreChambre,TypeId,ClassificationId,AdresseId")] BienImmo bienImmo)
        {
            if (id != bienImmo.BienImmoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bienImmo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BienImmoExists(bienImmo.BienImmoId))
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
            ViewData["AdresseId"] = new SelectList(_context.Adresses, "AdresseId", "AdresseId", bienImmo.AdresseId);
            ViewData["ClassificationId"] = new SelectList(_context.Classifications, "ClassificationId", "ClassificationId", bienImmo.ClassificationId);
            ViewData["TypeId"] = new SelectList(_context.TypesBiensImmos, "TypeBienImmoId", "TypeBienImmoId", bienImmo.TypeId);
            return View(bienImmo);
        }

        // GET: BienImmoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bienImmo = await _context.BiensImmos
                .Include(b => b.Adresse)
                .Include(b => b.Classification)
                .Include(b => b.Type)
                .FirstOrDefaultAsync(m => m.BienImmoId == id);
            if (bienImmo == null)
            {
                return NotFound();
            }

            return View(bienImmo);
        }

        // POST: BienImmoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bienImmo = await _context.BiensImmos.FindAsync(id);
            _context.BiensImmos.Remove(bienImmo);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BienImmoExists(int id)
        {
            return _context.BiensImmos.Any(e => e.BienImmoId == id);
        }
    }
}

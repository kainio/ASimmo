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
    public class LocauxController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LocauxController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Locaux
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Locaux.Include(l => l.Adresse).Include(l => l.Promoteur);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Locaux/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var local = await _context.Locaux
                .Include(l => l.Adresse)
                .Include(l => l.Promoteur)
                .FirstOrDefaultAsync(m => m.LocalId == id);
            if (local == null)
            {
                return NotFound();
            }

            return View(local);
        }

        // GET: Locaux/Create
        public IActionResult Create()
        {
            ViewData["AdresseId"] = new SelectList(_context.Adresses, "AdresseId", "AdresseId");
            ViewData["PromoteurId"] = new SelectList(_context.Promoteurs, "PromoteurId", "PromoteurId");
            return View();
        }

        // POST: Locaux/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("LocalId,Type,PromoteurId,AdresseId")] Local local)
        {
            if (ModelState.IsValid)
            {
                _context.Add(local);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AdresseId"] = new SelectList(_context.Adresses, "AdresseId", "AdresseId", local.AdresseId);
            ViewData["PromoteurId"] = new SelectList(_context.Promoteurs, "PromoteurId", "PromoteurId", local.PromoteurId);
            return View(local);
        }

        // GET: Locaux/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var local = await _context.Locaux.FindAsync(id);
            if (local == null)
            {
                return NotFound();
            }
            ViewData["AdresseId"] = new SelectList(_context.Adresses, "AdresseId", "AdresseId", local.AdresseId);
            ViewData["PromoteurId"] = new SelectList(_context.Promoteurs, "PromoteurId", "PromoteurId", local.PromoteurId);
            return View(local);
        }

        // POST: Locaux/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("LocalId,Type,PromoteurId,AdresseId")] Local local)
        {
            if (id != local.LocalId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(local);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LocalExists(local.LocalId))
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
            ViewData["AdresseId"] = new SelectList(_context.Adresses, "AdresseId", "AdresseId", local.AdresseId);
            ViewData["PromoteurId"] = new SelectList(_context.Promoteurs, "PromoteurId", "PromoteurId", local.PromoteurId);
            return View(local);
        }

        // GET: Locaux/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var local = await _context.Locaux
                .Include(l => l.Adresse)
                .Include(l => l.Promoteur)
                .FirstOrDefaultAsync(m => m.LocalId == id);
            if (local == null)
            {
                return NotFound();
            }

            return View(local);
        }

        // POST: Locaux/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var local = await _context.Locaux.FindAsync(id);
            _context.Locaux.Remove(local);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LocalExists(int id)
        {
            return _context.Locaux.Any(e => e.LocalId == id);
        }
    }
}

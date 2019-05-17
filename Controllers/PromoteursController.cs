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
    public class PromoteursController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PromoteursController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Promoteurs
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Promoteurs.Include(p => p.Type).Include(p => p.User);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Promoteurs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var promoteur = await _context.Promoteurs
                .Include(p => p.Type)
                .Include(p => p.User)
                .FirstOrDefaultAsync(m => m.PromoteurId == id);
            if (promoteur == null)
            {
                return NotFound();
            }

            return View(promoteur);
        }

        // GET: Promoteurs/Create
        public IActionResult Create()
        {
            ViewData["TypeId"] = new SelectList(_context.TypesPromoteurs, "TypePromoteurId", "TypePromoteurId");
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: Promoteurs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PromoteurId,UserId,TypeId")] Promoteur promoteur)
        {
            if (ModelState.IsValid)
            {
                _context.Add(promoteur);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["TypeId"] = new SelectList(_context.TypesPromoteurs, "TypePromoteurId", "TypePromoteurId", promoteur.TypeId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", promoteur.UserId);
            return View(promoteur);
        }

        // GET: Promoteurs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var promoteur = await _context.Promoteurs.FindAsync(id);
            if (promoteur == null)
            {
                return NotFound();
            }
            ViewData["TypeId"] = new SelectList(_context.TypesPromoteurs, "TypePromoteurId", "TypePromoteurId", promoteur.TypeId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", promoteur.UserId);
            return View(promoteur);
        }

        // POST: Promoteurs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PromoteurId,UserId,TypeId")] Promoteur promoteur)
        {
            if (id != promoteur.PromoteurId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(promoteur);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PromoteurExists(promoteur.PromoteurId))
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
            ViewData["TypeId"] = new SelectList(_context.TypesPromoteurs, "TypePromoteurId", "TypePromoteurId", promoteur.TypeId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", promoteur.UserId);
            return View(promoteur);
        }

        // GET: Promoteurs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var promoteur = await _context.Promoteurs
                .Include(p => p.Type)
                .Include(p => p.User)
                .FirstOrDefaultAsync(m => m.PromoteurId == id);
            if (promoteur == null)
            {
                return NotFound();
            }

            return View(promoteur);
        }

        // POST: Promoteurs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var promoteur = await _context.Promoteurs.FindAsync(id);
            _context.Promoteurs.Remove(promoteur);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PromoteurExists(int id)
        {
            return _context.Promoteurs.Any(e => e.PromoteurId == id);
        }
    }
}

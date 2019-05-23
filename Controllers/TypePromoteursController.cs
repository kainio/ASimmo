using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ASimmo.Data;
using ASimmo.Models;
using Microsoft.AspNetCore.Authorization;

namespace ASimmo.Controllers
{
    [Authorize(Roles = "Admin")]
    public class TypePromoteursController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TypePromoteursController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: TypePromoteurs
        public async Task<IActionResult> Index()
        {
            return View(await _context.TypesPromoteurs.ToListAsync());
        }

        // GET: TypePromoteurs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var typePromoteur = await _context.TypesPromoteurs
                .FirstOrDefaultAsync(m => m.TypePromoteurId == id);
            if (typePromoteur == null)
            {
                return NotFound();
            }

            return View(typePromoteur);
        }

        // GET: TypePromoteurs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TypePromoteurs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TypePromoteurId,Libelle")] TypePromoteur typePromoteur)
        {
            if (ModelState.IsValid)
            {
                _context.Add(typePromoteur);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(typePromoteur);
        }

        // GET: TypePromoteurs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var typePromoteur = await _context.TypesPromoteurs.FindAsync(id);
            if (typePromoteur == null)
            {
                return NotFound();
            }
            return View(typePromoteur);
        }

        // POST: TypePromoteurs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TypePromoteurId,Libelle")] TypePromoteur typePromoteur)
        {
            if (id != typePromoteur.TypePromoteurId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(typePromoteur);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TypePromoteurExists(typePromoteur.TypePromoteurId))
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
            return View(typePromoteur);
        }

        // GET: TypePromoteurs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var typePromoteur = await _context.TypesPromoteurs
                .FirstOrDefaultAsync(m => m.TypePromoteurId == id);
            if (typePromoteur == null)
            {
                return NotFound();
            }

            return View(typePromoteur);
        }

        // POST: TypePromoteurs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var typePromoteur = await _context.TypesPromoteurs.FindAsync(id);
            _context.TypesPromoteurs.Remove(typePromoteur);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TypePromoteurExists(int id)
        {
            return _context.TypesPromoteurs.Any(e => e.TypePromoteurId == id);
        }
    }
}

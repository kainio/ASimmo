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
    public class TypeClassificationsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TypeClassificationsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: TypeClassifications
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.TypesClassifications;
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: TypeClassifications/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var typeClassification = await _context.TypesClassifications
                .FirstOrDefaultAsync(m => m.TypeClassificationId == id);
            if (typeClassification == null)
            {
                return NotFound();
            }

            return View(typeClassification);
        }

        // GET: TypeClassifications/Create
        public IActionResult Create()
        {
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: TypeClassifications/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TypeClassificationId,Libelle,UserId")] TypeClassification typeClassification)
        {
            if (ModelState.IsValid)
            {
                _context.Add(typeClassification);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(typeClassification);
        }

        // GET: TypeClassifications/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var typeClassification = await _context.TypesClassifications.FindAsync(id);
            if (typeClassification == null)
            {
                return NotFound();
            }
            return View(typeClassification);
        }

        // POST: TypeClassifications/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TypeClassificationId,Libelle,UserId")] TypeClassification typeClassification)
        {
            if (id != typeClassification.TypeClassificationId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(typeClassification);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TypeClassificationExists(typeClassification.TypeClassificationId))
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
            return View(typeClassification);
        }

        // GET: TypeClassifications/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var typeClassification = await _context.TypesClassifications
                .FirstOrDefaultAsync(m => m.TypeClassificationId == id);
            if (typeClassification == null)
            {
                return NotFound();
            }

            return View(typeClassification);
        }

        // POST: TypeClassifications/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var typeClassification = await _context.TypesClassifications.FindAsync(id);
            _context.TypesClassifications.Remove(typeClassification);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TypeClassificationExists(int id)
        {
            return _context.TypesClassifications.Any(e => e.TypeClassificationId == id);
        }
    }
}

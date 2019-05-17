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
    public class TypeBienImmoesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TypeBienImmoesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: TypeBienImmoes
        public async Task<IActionResult> Index()
        {
            return View(await _context.TypesBiensImmos.ToListAsync());
        }

        // GET: TypeBienImmoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var typeBienImmo = await _context.TypesBiensImmos
                .FirstOrDefaultAsync(m => m.TypeBienImmoId == id);
            if (typeBienImmo == null)
            {
                return NotFound();
            }

            return View(typeBienImmo);
        }

        // GET: TypeBienImmoes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TypeBienImmoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TypeBienImmoId,Libelle")] TypeBienImmo typeBienImmo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(typeBienImmo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(typeBienImmo);
        }

        // GET: TypeBienImmoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var typeBienImmo = await _context.TypesBiensImmos.FindAsync(id);
            if (typeBienImmo == null)
            {
                return NotFound();
            }
            return View(typeBienImmo);
        }

        // POST: TypeBienImmoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TypeBienImmoId,Libelle")] TypeBienImmo typeBienImmo)
        {
            if (id != typeBienImmo.TypeBienImmoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(typeBienImmo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TypeBienImmoExists(typeBienImmo.TypeBienImmoId))
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
            return View(typeBienImmo);
        }

        // GET: TypeBienImmoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var typeBienImmo = await _context.TypesBiensImmos
                .FirstOrDefaultAsync(m => m.TypeBienImmoId == id);
            if (typeBienImmo == null)
            {
                return NotFound();
            }

            return View(typeBienImmo);
        }

        // POST: TypeBienImmoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var typeBienImmo = await _context.TypesBiensImmos.FindAsync(id);
            _context.TypesBiensImmos.Remove(typeBienImmo);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TypeBienImmoExists(int id)
        {
            return _context.TypesBiensImmos.Any(e => e.TypeBienImmoId == id);
        }
    }
}

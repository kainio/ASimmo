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
using System.Security.Claims;

namespace ASimmo.Controllers
{
    public class ClassificationsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ClassificationsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Classifications
        public async Task<IActionResult> Index()
        {
            if (this.HttpContext.User.IsInRole("Admin"))
            {
                var applicationDbContext = _context.Classifications.Include(c => c.Parent).Include(c => c.Promoteur).Include(c => c.Type);
                return View(await applicationDbContext.ToListAsync());
            }
            else if (this.HttpContext.User.IsInRole("Agent"))
            {
                var _userId = this.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
                var applicationDbContext = _context.Classifications.Include(c => c.Promoteur).Include(c => c.Type).Where(p => p.Promoteur.UserId== _userId);
                return View(await applicationDbContext.ToListAsync());
            }

            return new ForbidResult();
        }

        // GET: Classifications/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var classification = await _context.Classifications
                .Include(c => c.Parent)
                .Include(c => c.Promoteur)
                .Include(c => c.Type)
                .FirstOrDefaultAsync(m => m.ClassificationId == id);
            if (classification == null)
            {
                return NotFound();
            }

            return View(classification);
        }

        // GET: Classifications/Create
        [Authorize(Roles = "Admin, Agent")]
        public IActionResult Create()
        {
            
            ViewData["ParentId"] = new SelectList(_context.Classifications, "ClassificationId", "Libelle");

            if (this.HttpContext.User.IsInRole("Admin"))
            {
                ViewData["PromoteurId"] = new SelectList(_context.Promoteurs, "PromoteurId", "Libelle");
            }
            else
            {
                var _userId = this.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
                ViewData["PromoteurId"] = new SelectList(_context.Promoteurs.Where(p => p.UserId == _userId), "PromoteurId", "Libelle");
            }
                

            ViewData["TypeId"] = new SelectList(_context.TypesClassifications, "TypeClassificationId", "Libelle");
            return View();
        }

        // POST: Classifications/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, Agent")]
        public async Task<IActionResult> Create([Bind("ClassificationId,Libelle,TypeId,ParentId,PromoteurId,Recherchable,PrixMax,PrixMin")] Classification classification)
        {
            
            if (ModelState.IsValid)
            {
                if (this.HttpContext.User.IsInRole("Admin") || IsOwner(classification.PromoteurId))
                {
                    _context.Add(classification);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return new ForbidResult();
                }
            }


            if (this.HttpContext.User.IsInRole("Admin"))
            {
                ViewData["PromoteurId"] = new SelectList(_context.Promoteurs, "PromoteurId", "Libelle");
                ViewData["ParentId"] = new SelectList(_context.Classifications.Where(c => IsOwner(c.PromoteurId)), "ClassificationId", "Libelle", classification.ParentId);

            }
            else
            {
                ViewData["ParentId"] = new SelectList(_context.Classifications, "ClassificationId", "Libelle", classification.ParentId);
                ViewData["PromoteurId"] = new SelectList(_context.Promoteurs.Where(p => IsOwner(p.PromoteurId)), "PromoteurId", "Libelle");
            }

            ViewData["TypeId"] = new SelectList(_context.TypesClassifications, "TypeClassificationId", "Libelle", classification.TypeId);

            return View(classification);
        }

        // GET: Classifications/Edit/5
        [Authorize(Roles = "Admin, Agent")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var classification = await _context.Classifications.FindAsync(id);
            if (classification == null)
            {
                return NotFound();
            }
            ViewData["ParentId"] = new SelectList(_context.Classifications, "ClassificationId", "ClassificationId", classification.ParentId);
            ViewData["PromoteurId"] = new SelectList(_context.Promoteurs, "PromoteurId", "PromoteurId", classification.PromoteurId);
            ViewData["TypeId"] = new SelectList(_context.TypesClassifications, "TypeClassificationId", "TypeClassificationId", classification.TypeId);
            return View(classification);
        }

        // POST: Classifications/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, Agent")]
        public async Task<IActionResult> Edit(int id, [Bind("ClassificationId,Libelle,TypeId,ParentId,PromoteurId,Recherchable,PrixMax,PrixMin")] Classification classification)
        {
            if (id != classification.ClassificationId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(classification);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClassificationExists(classification.ClassificationId))
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
            ViewData["ParentId"] = new SelectList(_context.Classifications, "ClassificationId", "ClassificationId", classification.ParentId);
            ViewData["PromoteurId"] = new SelectList(_context.Promoteurs, "PromoteurId", "PromoteurId", classification.PromoteurId);
            ViewData["TypeId"] = new SelectList(_context.TypesClassifications, "TypeClassificationId", "TypeClassificationId", classification.TypeId);
            return View(classification);
        }

        // GET: Classifications/Delete/5
        [Authorize(Roles = "Admin, Agent")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var classification = await _context.Classifications
                .Include(c => c.Parent)
                .Include(c => c.Promoteur)
                .Include(c => c.Type)
                .FirstOrDefaultAsync(m => m.ClassificationId == id);
            if (classification == null)
            {
                return NotFound();
            }

            return View(classification);
        }

        // POST: Classifications/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, Agent")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var classification = await _context.Classifications.FindAsync(id);
            _context.Classifications.Remove(classification);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClassificationExists(int id)
        {
            return _context.Classifications.Any(e => e.ClassificationId == id);
        }

        private bool IsOwner(int id)
        {
            var _userId = this.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var promoteur = _context.Promoteurs.Include(p => p.User).FirstOrDefault(p=> p.PromoteurId == id);
            if(promoteur.UserId == _userId) 
            {
                return true;
            }

            return false;
        }
    }
}

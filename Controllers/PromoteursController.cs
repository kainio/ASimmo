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
            if (this.HttpContext.User.IsInRole("Admin"))
            {
                var applicationDbContext = _context.Promoteurs.Include(p => p.Type).Include(p => p.User);
                return View(await applicationDbContext.ToListAsync());
            }  
            else if (this.HttpContext.User.IsInRole("Agent"))
            {
                var applicationDbContext = _context.Promoteurs.Include(p => p.Type).Include(p => p.User).Where(p => p.User.UserName == this.HttpContext.User.Identity.Name);
                return View(await applicationDbContext.ToListAsync());
            }

            return new ForbidResult();
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
        [Authorize(Roles = "Admin, Agent")]
        public IActionResult Create()
        {
            ViewData["TypeId"] = new SelectList(_context.TypesPromoteurs, "TypePromoteurId", "Libelle");
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "UserName");
            return View();
        }

        // POST: Promoteurs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, Agent")]
        public async Task<IActionResult> Create([Bind("Libelle,PromoteurId,UserId,TypeId")] Promoteur promoteur)
        {
            if (ModelState.IsValid)
            {
                _context.Add(promoteur);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["TypeId"] = new SelectList(_context.TypesPromoteurs, "TypePromoteurId", "Libelle", promoteur.TypeId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "UserName", promoteur.UserId);
            return View(promoteur);
        }

        // GET: Promoteurs/Edit/5
        [Authorize(Roles = "Admin, Agent")]
        public async Task<IActionResult> Edit(int? id)
        {
            
            if (id == null)
            {
                return NotFound();
            }

            var promoteur = await _context.Promoteurs.Include(p => p.User).FirstOrDefaultAsync(m => m.PromoteurId == id);

            if (promoteur == null)
            {
                return NotFound();
            }

            if (this.HttpContext.User.Identity.IsAuthenticated)
            {
                if (this.HttpContext.User.IsInRole("Admin") || this.HttpContext.User.Identity.Name == promoteur.User.UserName)
                {
                    ViewData["TypeId"] = new SelectList(_context.TypesPromoteurs, "TypePromoteurId", "Libelle", promoteur.TypeId);
                    ViewData["UserId"] = new SelectList(_context.Users, "Id", "UserName", promoteur.UserId);
                    return View(promoteur);
                }
            }
            
            return new ForbidResult();

        }

        // POST: Promoteurs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, Agent")]
        public async Task<IActionResult> Edit(int id, [Bind("Libelle,PromoteurId,UserId,TypeId")] Promoteur promoteur)
        {
            
            if (id != promoteur.PromoteurId)
            {
                return NotFound();
            }

            if (PromoteurExists(promoteur.PromoteurId))
            {
                var userId = this.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;

                if (ModelState.IsValid)
                {
                    try
                    {
                        if (this.HttpContext.User.IsInRole("Admin") || promoteur.UserId == userId )
                        {
                            _context.Update(promoteur);
                            await _context.SaveChangesAsync();
                        }
                        else
                        {
                            return new ForbidResult();
                        }
                    }
                    catch (DbUpdateConcurrencyException)
                    {

                        throw;
                    }
                    return RedirectToAction(nameof(Index));
                }
            }
            else
            {
                return NotFound();
            }
            

            ViewData["TypeId"] = new SelectList(_context.TypesPromoteurs, "TypePromoteurId", "Libelle", promoteur.TypeId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "UserName", promoteur.UserId);
            return View(promoteur);
        }

        // GET: Promoteurs/Delete/5
        [Authorize(Roles = "Admin, Agent")]
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
            if (this.HttpContext.User.IsInRole("Admin") || this.HttpContext.User.Identity.Name == promoteur.User.UserName)
            {
                return View(promoteur);
            }
            else
            {
                return new ForbidResult();
            }
        }

        // POST: Promoteurs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, Agent")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var userId = this.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var promoteur = await _context.Promoteurs.FindAsync(id);

            if (this.HttpContext.User.IsInRole("Admin") || promoteur.UserId == userId)
            {
                _context.Promoteurs.Remove(promoteur);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return new ForbidResult();
            }

        }

        private bool PromoteurExists(int id)
        {
            return _context.Promoteurs.Any(e => e.PromoteurId == id);
        }
    }
}

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
    public class AdressesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdressesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Adresses
        [Authorize(Roles = "Admin, Agent")]
        public async Task<IActionResult> Index()
        {
            if (this.HttpContext.User.IsInRole("Admin"))
            {
                var applicationDbContext = _context.Adresses.Include(c => c.Promoteur);
                return View(await applicationDbContext.ToListAsync());
            }
            else if (this.HttpContext.User.IsInRole("Agent"))
            {
                var _userId = this.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
                var applicationDbContext = _context.Adresses.Include(c => c.Promoteur).Where(p => p.Promoteur.UserId == _userId);
                return View(await applicationDbContext.ToListAsync());
            }
            return new ForbidResult();
        }

        // GET: Adresses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var adresse = await _context.Adresses
                .Include(a => a.Promoteur)
                .FirstOrDefaultAsync(m => m.AdresseId == id);
            if (adresse == null)
            {
                return NotFound();
            }

            return View(adresse);
        }

        // GET: Adresses/Create
        [Authorize(Roles = "Admin, Agent")]
        public IActionResult Create()
        {
            if (this.HttpContext.User.IsInRole("Admin"))
            {
                ViewData["PromoteurId"] = new SelectList(_context.Promoteurs, "PromoteurId", "Libelle");
            }
            else
            {
                var _userId = this.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
                ViewData["PromoteurId"] = new SelectList(_context.Promoteurs.Where(p => p.UserId == _userId), "PromoteurId", "Libelle");
            }
            return View();
        }

        // POST: Adresses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, Agent")]
        public async Task<IActionResult> Create([Bind("AdresseId,Quartier,CodePostal,Ville,AdressePostale,PromoteurId")] Adresse adresse)
        {
            if (ModelState.IsValid)
            {
                if (this.HttpContext.User.IsInRole("Admin") || IsOwner(adresse.PromoteurId))
                {
                    _context.Add(adresse);
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
            }
            else
            {
                var _userId = this.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
                ViewData["PromoteurId"] = new SelectList(_context.Promoteurs.Where(p => p.UserId == _userId), "PromoteurId", "Libelle");
            }
            return View(adresse);
        }

        // GET: Adresses/Edit/5
        [Authorize(Roles = "Admin, Agent")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var adresse = await _context.Adresses.FindAsync(id);
            if (adresse == null)
            {
                return NotFound();
            }
            if (this.HttpContext.User.IsInRole("Admin"))
            {
                ViewData["PromoteurId"] = new SelectList(_context.Promoteurs, "PromoteurId", "Libelle");
            }
            else
            {
                var _userId = this.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
                ViewData["PromoteurId"] = new SelectList(_context.Promoteurs.Where(p => p.UserId == _userId), "PromoteurId", "Libelle");
            }
            return View(adresse);
        }

        // POST: Adresses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, Agent")]
        public async Task<IActionResult> Edit(int id, [Bind("AdresseId,Quartier,CodePostal,Ville,AdressePostale,PromoteurId")] Adresse adresse)
        {
            if (id != adresse.AdresseId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (this.HttpContext.User.IsInRole("Admin") || IsOwner(adresse.PromoteurId))
                    {
                        _context.Update(adresse);
                        await _context.SaveChangesAsync();
                    }
                    else
                    {
                        return new ForbidResult();
                    }

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AdresseExists(adresse.AdresseId))
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


            if (this.HttpContext.User.IsInRole("Admin"))
            {
                ViewData["PromoteurId"] = new SelectList(_context.Promoteurs, "PromoteurId", "Libelle");
            }
            else
            {
                var _userId = this.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
                ViewData["PromoteurId"] = new SelectList(_context.Promoteurs.Where(p => p.UserId == _userId), "PromoteurId", "Libelle");
            }
            return View(adresse);
        }

        // GET: Adresses/Delete/5
        [Authorize(Roles = "Admin, Agent")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var adresse = await _context.Adresses
                .FirstOrDefaultAsync(m => m.AdresseId == id);
            if (adresse == null)
            {
                return NotFound();
            }
            if (this.HttpContext.User.IsInRole("Admin") || IsOwner(adresse.PromoteurId))
            {
                return View(adresse);
            }
            else
            {
                return new ForbidResult();
            }

        }

        // POST: Adresses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, Agent")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var adresse = await _context.Adresses.FindAsync(id);
            if (this.HttpContext.User.IsInRole("Admin") || IsOwner(adresse.PromoteurId))
            {
                _context.Adresses.Remove(adresse);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return new ForbidResult();
            }
        }

        private bool AdresseExists(int id)
        {
            return _context.Adresses.Any(e => e.AdresseId == id);
        }

        private bool IsOwner(int pid)
        {
            var _userId = this.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var promoteur = _context.Promoteurs.Include(p => p.User).FirstOrDefaultAsync(p => p.PromoteurId == pid).GetAwaiter().GetResult();
            if (promoteur.UserId == _userId)
            {
                return true;
            }

            return false;
        }
    }
}

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
    public class LocauxController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LocauxController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Locaux
        [Authorize(Roles = "Admin, Agent")]
        public async Task<IActionResult> Index()
        {
            if (this.HttpContext.User.IsInRole("Admin"))
            {
                var applicationDbContext = _context.Locaux.Include(l => l.Adresse).Include(l => l.Promoteur);
                return View(await applicationDbContext.ToListAsync());
            }
            else if (this.HttpContext.User.IsInRole("Agent"))
            {
                var _userId = this.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
                var applicationDbContext = _context.Locaux.Include(l => l.Adresse).Include(l => l.Promoteur).Where(l => l.Promoteur.UserId == _userId);
                return View(await applicationDbContext.ToListAsync());
            }
            return new ForbidResult();
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
        [Authorize(Roles = "Admin, Agent")]
        public IActionResult Create()
        {

            if (this.HttpContext.User.IsInRole("Admin"))
            {
                ViewData["PromoteurId"] = new SelectList(_context.Promoteurs, "PromoteurId", "Libelle");
                ViewData["AdresseId"] = new SelectList(_context.Adresses, "AdresseId", "AdressePostale");
            }
            else
            {
                var _userId = this.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
                ViewData["AdresseId"] = new SelectList(_context.Adresses.Include(a => a.Promoteur).Where(a => a.Promoteur.UserId == _userId), "AdresseId", "AdressePostale");
                ViewData["PromoteurId"] = new SelectList(_context.Promoteurs.Where(p => p.UserId == _userId), "PromoteurId", "Libelle");
            }
            
            return View();
        }

        // POST: Locaux/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, Agent")]
        public async Task<IActionResult> Create([Bind("LocalId,Type,PromoteurId,AdresseId")] Local local)
        {
            if (ModelState.IsValid)
            {
                if (this.HttpContext.User.IsInRole("Admin") || IsOwner(local.PromoteurId))
                {
                    _context.Add(local);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                } else
                {
                    return new ForbidResult();
                }
            }

            if (this.HttpContext.User.IsInRole("Admin"))
            {
                ViewData["AdresseId"] = new SelectList(_context.Adresses, "AdresseId", "AdresseId", local.AdresseId);
                ViewData["PromoteurId"] = new SelectList(_context.Promoteurs, "PromoteurId", "PromoteurId", local.PromoteurId);
            }
            else
            {
                var _userId = this.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
                ViewData["AdresseId"] = new SelectList(_context.Adresses.Include(a => a.Promoteur).Where(a => a.Promoteur.UserId == _userId), "AdresseId", "AdressePostale", local.AdresseId);
                ViewData["PromoteurId"] = new SelectList(_context.Promoteurs.Where(p => p.UserId == _userId), "PromoteurId", "Libelle", local.PromoteurId);
            }
            return View(local);
        }

        // GET: Locaux/Edit/5
        [Authorize(Roles = "Admin, Agent")]
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
            if (this.HttpContext.User.IsInRole("Admin"))
            {
                ViewData["AdresseId"] = new SelectList(_context.Adresses, "AdresseId", "AdresseId", local.AdresseId);
                ViewData["PromoteurId"] = new SelectList(_context.Promoteurs, "PromoteurId", "PromoteurId", local.PromoteurId);
            }
            else
            {
                var _userId = this.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
                ViewData["AdresseId"] = new SelectList(_context.Adresses.Include(a => a.Promoteur).Where(a => a.Promoteur.UserId == _userId), "AdresseId", "AdressePostale", local.AdresseId);
                ViewData["PromoteurId"] = new SelectList(_context.Promoteurs.Where(p => p.UserId == _userId), "PromoteurId", "Libelle", local.PromoteurId);
            }
            return View(local);
        }

        // POST: Locaux/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, Agent")]
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
                    if (this.HttpContext.User.IsInRole("Admin") || IsOwner(local.PromoteurId))
                    {
                        _context.Update(local);
                        await _context.SaveChangesAsync();
                    }
                    else
                    {
                        return new ForbidResult();
                    }
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
            if (this.HttpContext.User.IsInRole("Admin"))
            {
                ViewData["AdresseId"] = new SelectList(_context.Adresses, "AdresseId", "AdresseId", local.AdresseId);
                ViewData["PromoteurId"] = new SelectList(_context.Promoteurs, "PromoteurId", "PromoteurId", local.PromoteurId);
            }
            else
            {
                var _userId = this.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
                ViewData["AdresseId"] = new SelectList(_context.Adresses.Include(a => a.Promoteur).Where(a => a.Promoteur.UserId == _userId), "AdresseId", "AdressePostale", local.AdresseId);
                ViewData["PromoteurId"] = new SelectList(_context.Promoteurs.Where(p => p.UserId == _userId), "PromoteurId", "Libelle", local.PromoteurId);
            }
            return View(local);
        }

        // GET: Locaux/Delete/5
        [Authorize(Roles = "Admin, Agent")]
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
            if (this.HttpContext.User.IsInRole("Admin") || IsOwner(local.PromoteurId))
            {
                return View(local);
            }
            else
            {
                return new ForbidResult();
            }
        }

        // POST: Locaux/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, Agent")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var local = await _context.Locaux.FindAsync(id);
            if (this.HttpContext.User.IsInRole("Admin") || IsOwner(local.PromoteurId))
            {
                _context.Locaux.Remove(local);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return new ForbidResult();
            }
        }

        private bool LocalExists(int id)
        {
            return _context.Locaux.Any(e => e.LocalId == id);
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

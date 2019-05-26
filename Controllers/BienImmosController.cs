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
    public class BienImmosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BienImmosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: BienImmos
        public async Task<IActionResult> Index()
        {
            if (this.HttpContext.User.IsInRole("Admin"))
            {
                var applicationDbContext = _context.BiensImmos.Include(b => b.Adresse).Include(b => b.Classification).Include(b => b.Type);
                return View(await applicationDbContext.ToListAsync());
            }
            else if (this.HttpContext.User.IsInRole("Agent"))
            {
                var _userId = this.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
                var applicationDbContext = _context.BiensImmos.Include(b => b.Adresse).Include(b => b.Classification).Include(b => b.Type).Include(b=> b.Classification.Promoteur).Where(b => b.Classification.Promoteur.UserId == _userId);
                return View( await applicationDbContext.ToListAsync());
            }
            return new ForbidResult();
            
        }

        // GET: BienImmos/Details/5
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
        [Authorize(Roles = "Admin, Agent")]
        public IActionResult Create()
        {
            if(this.HttpContext.User.IsInRole("Admin"))
            {
                ViewData["AdresseId"] = new SelectList(_context.Adresses, "AdresseId", "AdressePostale");
                ViewData["ClassificationId"] = new SelectList(_context.Classifications, "ClassificationId", "Libelle");

            }
            else
            {
                var _userId = this.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
                ViewData["AdresseId"] = new SelectList(_context.Adresses.Include(a=> a.Promoteur).Where(a => a.Promoteur.UserId == _userId), "AdresseId", "AdressePostale");
                ViewData["ClassificationId"] = new SelectList(_context.Classifications.Include(c => c.Promoteur).Where(c => c.Promoteur.UserId == _userId), "ClassificationId", "Libelle");
            }

            ViewData["TypeId"] = new SelectList(_context.TypesBiensImmos, "TypeBienImmoId", "Libelle");
            return View();
        }

        // POST: BienImmoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, Agent")]
        public async Task<IActionResult> Create([Bind("BienImmoId,Prix,Surface,NombreChambre,TypeId,ClassificationId,AdresseId,Image")] BienImmo bienImmo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bienImmo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            if (this.HttpContext.User.IsInRole("Admin"))
            {
                ViewData["AdresseId"] = new SelectList(_context.Adresses, "AdresseId", "AdressePostale", bienImmo.AdresseId);
                ViewData["ClassificationId"] = new SelectList(_context.Classifications, "ClassificationId", "Libelle", bienImmo.ClassificationId);

            }
            else
            {
                var _userId = this.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
                ViewData["AdresseId"] = new SelectList(_context.Adresses.Include(a => a.Promoteur).Where(a => a.Promoteur.UserId == _userId), "AdresseId", "AdressePostale", bienImmo.AdresseId);
                ViewData["ClassificationId"] = new SelectList(_context.Classifications.Include(c => c.Promoteur).Where(c => c.Promoteur.UserId == _userId), "ClassificationId", "Libelle", bienImmo.ClassificationId);
            }

            ViewData["TypeId"] = new SelectList(_context.TypesBiensImmos, "TypeBienImmoId", "Libelle", bienImmo.TypeId);
            return View(bienImmo);
        }

        // GET: BienImmoes/Edit/5
        [Authorize(Roles = "Admin, Agent")]
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
            if (this.HttpContext.User.IsInRole("Admin"))
            {
                ViewData["AdresseId"] = new SelectList(_context.Adresses, "AdresseId", "AdressePostale", bienImmo.AdresseId);
                ViewData["ClassificationId"] = new SelectList(_context.Classifications, "ClassificationId", "Libelle", bienImmo.ClassificationId);

            }
            else
            {
                var _userId = this.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
                ViewData["AdresseId"] = new SelectList(_context.Adresses.Include(a => a.Promoteur).Where(a => a.Promoteur.UserId == _userId), "AdresseId", "AdressePostale", bienImmo.AdresseId);
                ViewData["ClassificationId"] = new SelectList(_context.Classifications.Include(c => c.Promoteur).Where(c => c.Promoteur.UserId == _userId), "ClassificationId", "Libelle", bienImmo.ClassificationId);
            }

            ViewData["TypeId"] = new SelectList(_context.TypesBiensImmos, "TypeBienImmoId", "Libelle", bienImmo.TypeId);
            return View(bienImmo);
        }

        // POST: BienImmoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, Agent")]
        public async Task<IActionResult> Edit(int id, [Bind("BienImmoId,Prix,Surface,NombreChambre,TypeId,ClassificationId,AdresseId,Image")] BienImmo bienImmo)
        {
            if (id != bienImmo.BienImmoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (this.HttpContext.User.IsInRole("Admin") || IsOwner(bienImmo.ClassificationId))
                    {
                        _context.Update(bienImmo);
                        await _context.SaveChangesAsync();
                    }
                    else
                    {
                        return new ForbidResult();
                    }
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

            if (this.HttpContext.User.IsInRole("Admin"))
            {
                ViewData["AdresseId"] = new SelectList(_context.Adresses, "AdresseId", "AdressePostale", bienImmo.AdresseId);
                ViewData["ClassificationId"] = new SelectList(_context.Classifications, "ClassificationId", "Libelle", bienImmo.ClassificationId);

            }
            else
            {
                var _userId = this.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
                ViewData["AdresseId"] = new SelectList(_context.Adresses.Include(a => a.Promoteur).Where(a => a.Promoteur.UserId == _userId), "AdresseId", "AdressePostale", bienImmo.AdresseId);
                ViewData["ClassificationId"] = new SelectList(_context.Classifications.Where(c => IsOwner(c.PromoteurId)), "ClassificationId", "Libelle", bienImmo.ClassificationId);
            }
            ViewData["TypeId"] = new SelectList(_context.TypesBiensImmos, "TypeBienImmoId", "Libelle", bienImmo.TypeId);
            return View(bienImmo);
        }

        // GET: BienImmoes/Delete/5
        [Authorize(Roles = "Admin, Agent")]
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
            if (this.HttpContext.User.IsInRole("Admin") || IsOwner(bienImmo.ClassificationId))
            {
                return View(bienImmo);
            } else
            {
                return new ForbidResult();
            }
        }

        // POST: BienImmoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, Agent")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bienImmo = await _context.BiensImmos.FindAsync(id);

            if (this.HttpContext.User.IsInRole("Admin") || IsOwner(bienImmo.ClassificationId))
            {
                
                _context.BiensImmos.Remove(bienImmo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return new ForbidResult();
            }
        }

        private bool BienImmoExists(int id)
        {
            return _context.BiensImmos.Any(e => e.BienImmoId == id);
        }

        private bool IsOwner(int cid)
        {
            var _userId = this.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var _c = _context.Classifications.Include(c=> c.Promoteur).Include(c => c.Promoteur.User).Where(c=> c.Promoteur.User.Id == _userId).FirstOrDefault(c => c.ClassificationId == cid);
            if (_c != null)
            {
                return true;
            }
            return false;
        }
    }
}

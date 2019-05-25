using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ASimmo.Data;
using ASimmo.Models;

namespace ASimmo.Pages
{
    public class AccueilModel : PageModel
    {
        private readonly ASimmo.Data.ApplicationDbContext _context;

        public AccueilModel(ASimmo.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Classification> Classification { get;set; }

        public async Task OnGetAsync()
        {
            Classification = await _context.Classifications
                .Include(c => c.Parent)
                .Include(c => c.Promoteur)
                .Include(c => c.Type)
                .Where(c=> c.Recherchable)
                .Take(4)
                .ToListAsync();
        }
    }
}

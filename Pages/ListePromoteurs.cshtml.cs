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
    public class PromoteursModel : PageModel
    {
        private readonly ASimmo.Data.ApplicationDbContext _context;

        public PromoteursModel(ASimmo.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Promoteur> Promoteur { get;set; }

        public async Task OnGetAsync()
        {
            Promoteur = await _context.Promoteurs
                .Include(p => p.Type)
                .Include(p => p.User).ToListAsync();
        }
    }
}

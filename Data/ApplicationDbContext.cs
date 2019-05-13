using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ASimmo.Models;
namespace ASimmo.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Adresse> Adresses { get; set; }
        public DbSet<Promoteur> Promoteurs { get; set; }

        public DbSet<TypePromoteur> TypesPromoteurs { get; set; }
        public DbSet<Local> Locaux { get; set; }

        public DbSet<Classification> Classifications { get; set; }
        public DbSet<TypeClassification> TypesClassifications { get; set; }

        public DbSet<BienImmo> BiensImmos { get; set; }
        public DbSet<TypeBienImmo> TypesBiensImmos { get; set; }

    }
}

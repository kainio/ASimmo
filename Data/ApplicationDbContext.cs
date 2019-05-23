using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ASimmo.Models;
using Microsoft.AspNetCore.Http;

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

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<TypeClassification>().HasData(
                new TypeClassification
                {
                    TypeClassificationId = 1,
                    Libelle = "Projet",
                },
                    new TypeClassification
                {
                    TypeClassificationId = 2,
                    Libelle = "Résidence",
                },
                new TypeClassification
                {
                    TypeClassificationId = 3,
                    Libelle = "Immeuble",
           });

            builder.Entity<TypeBienImmo>().HasData(
                 new TypeBienImmo
                 {
                     TypeBienImmoId = 1,
                     Libelle = "Maison",
                 },
                new TypeBienImmo
                {
                    TypeBienImmoId = 2,
                    Libelle = "Appartement",
                },
                new TypeBienImmo
                {
                    TypeBienImmoId = 3,
                    Libelle = "Terrain",
                },
                new TypeBienImmo
                {
                    TypeBienImmoId = 4,
                    Libelle = "Studio",
                },
                new TypeBienImmo
                {
                    TypeBienImmoId = 5,
                    Libelle = "Villa",
                },
                new TypeBienImmo
                {
                    TypeBienImmoId = 6,
                    Libelle = "Immeuble",
                },
                new TypeBienImmo
                {
                    TypeBienImmoId = 7,
                    Libelle = "Résidence",
                });

            builder.Entity<TypePromoteur>().HasData(
                new TypePromoteur
                {
                    TypePromoteurId = 1,
                    Libelle = "Professionnel",
                },
                new TypePromoteur
                {
                    TypePromoteurId = 2,
                    Libelle = "Entreprise",
                }
            );
        }

    }
}

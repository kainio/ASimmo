using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASimmo.Models;

namespace ASimmo.Data
{
    public class SeedData
    {
        public static void Initialize(ApplicationDbContext context)
        {
            if (!context.TypesClassifications.Any())
            {
                context.TypesClassifications.AddRange(
                    new TypeClassification
                    {
                        Libelle = "Projet",
                    },
                    new TypeClassification
                    {
                        Libelle = "Résidence",
                    },
                    new TypeClassification
                    {
                        Libelle = "Immeuble",
                    }
                );

                context.SaveChanges();
            }
            if (!context.TypesBiensImmos.Any())
            {
                context.TypesBiensImmos.AddRange(
                    new TypeBienImmo
                    {
                        Libelle = "Maison",
                    },
                    new TypeBienImmo
                    {
                        Libelle = "Appartement",
                    },
                    new TypeBienImmo
                    {
                        Libelle = "Terrain",
                    },
                    new TypeBienImmo
                    {
                        Libelle = "Studio",
                    },
                    new TypeBienImmo
                    {
                        Libelle = "Villa",
                    },
                    new TypeBienImmo
                    {
                        Libelle = "Immeuble",
                    },
                    new TypeBienImmo
                    {
                        Libelle = "Résidence",
                    }
                );

                context.SaveChanges();
            }

            if (!context.TypesPromoteurs.Any())
            {
                context.TypesPromoteurs.AddRange(
                    new TypePromoteur
                    {
                        Libelle = "Professionnel",
                    },
                    new TypePromoteur
                    {
                        Libelle = "Entreprise",
                    }
                );

                context.SaveChanges();
            }

        }
    }
}

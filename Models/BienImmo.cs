﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ASimmo.Models
{
    public class BienImmo
    {
        public int BienImmoId { get; set;}
        [Required]
        [Range(minimum:0.01,maximum: (double) Decimal.MaxValue)]
        public Decimal Prix { get; set; }
        [Required]
        public Decimal Surface { get; set; }
        [Required]
        public int NombreChambre { get; set; }
        public int TypeId { get; set; }
        public int ClassificationId { get; set; }
        public int AdresseId { get; set; }

        public TypeBienImmo Type { get; set; }

        public Classification Classification { get; set; }

        public Adresse Adresse { get; set; }
    }
}

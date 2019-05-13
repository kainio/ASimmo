using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ASimmo.Models
{
    public class Local
    {
        public int LocalId { get; set; }

        [Required]
        public string Type { get; set; }

        [Required]
        public int PromoteurId { get; set; }
        [Required]
        public int AdresseId { get; set; }

        public virtual Promoteur Promoteur {get; set;}
        public virtual Adresse Adresse { get; set; }
    }
}

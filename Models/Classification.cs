using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ASimmo.Models
{
    public class Classification
    {
        public int ClassificationId { get; set; }

        [Required]
        public string Libelle {get; set;}

        public int TypeId { get; set; }

        public int? ParentId { get; set; }

        public int PromoteurId { get; set; }

        public bool Recherchable { get; set; }

        public Decimal PrixMax { get; set; }
        public Decimal PrixMin { get; set; }

        public string Image { get; set; }

        public DateTime UpdatedAt { get; set; }
        public DateTime CreatedAt { get; set; }

        public virtual TypeClassification Type { get; set; }

        public virtual Classification Parent { get; set; }

        public virtual Promoteur Promoteur { get; set; }
        
    }
}

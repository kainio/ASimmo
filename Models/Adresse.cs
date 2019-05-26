using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ASimmo.Models
{
    public class Adresse
    {
        public int AdresseId { get; set; }

        public string Quartier { get; set; }
        public string CodePostal { get; set; }

        public string Ville { get; set; }
        public string AdressePostale { get; set; }

       
        public string Lon { get; set; }

        public string Lat { get; set; }

        public int PromoteurId { get; set; }
        public virtual Promoteur Promoteur { get; set; }
    }
}

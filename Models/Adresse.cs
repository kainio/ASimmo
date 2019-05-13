using System;
using System.Collections.Generic;
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
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace ASimmo.Models
{
    public class Promoteur
    {

        public int PromoteurId { get; set; }
        public string Image { get; set; }
        public string UserId { get; set; }
        public int TypeId {get; set;}
        public virtual IdentityUser User { get; set; }
        public virtual TypePromoteur Type { get; set; }


    }
}

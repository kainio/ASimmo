using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace ASimmo.Models
{
    public class TypeClassification
    {
        public int TypeClassificationId { get; set; }

        public string Libelle { get; set; }

        public string UserId { get; set; }

        public virtual IdentityUser User { get; set; }

    }
}

using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Core.Entities
{
    public class AppUser: IdentityUser
    {
        [MaxLength(50)]
        public string FiratName { get; set; } = null!;

        [MaxLength(50)]
        public string LastName { get; set; } = null!;
        public Address Address { get; set; }
    }
}

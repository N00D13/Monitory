using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Monitory.Core.Models
{
    enum Role
    {
        Administrator,
        Reseller,
        User,
    }

    public class Account
    {
        [Key]
        public Guid AccountID { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        [MinLength(6)]
        public string Password { get; set; }

        public string Role { get; set; }

        public ICollection<WebCheck> WebCheck { get; set; }
        
    }
}

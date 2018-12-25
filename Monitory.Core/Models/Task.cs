using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Monitory.Core.Models
{
    public class Task
    {
        public Guid TaskID { get; set; }

        [Required]
        public string Name { get; set; }

        public DateTime CreateDate { get; set; }
    }
}

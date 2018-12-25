using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Monitory.Core.Models
{
    public class WebCheck   
    {
        [Key]
        public Guid WebCheckID { get; set; }

        public string Name { get; set; }
        public string Domain { get; set; }
        public int Delay { get; set; }
        public DateTime CreateDate { get; set; }
        
        public Guid AccountID { get; set; }
        public Account Account { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Entities
{
    public class Kennel
    {
        [Key]
        public int kennelId { get; set;}
        public string description { get; set; }
        public int capacity { get; set; }
        public double dailyCost { get; set; }
        public ICollection<Hospitalization> hospitalizations { get; set; }
    }
}
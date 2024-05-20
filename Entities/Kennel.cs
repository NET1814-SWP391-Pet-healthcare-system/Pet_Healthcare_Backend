using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Entities
{
    public class Kennel
    {
        public int kennelId { get; set;}
        public string description { get; set; }
        public int capacity { get; set; }
        public double dailyCost { get; set; }
        public ICollection<Hospitalization> hospitalizations { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetHealthCareSystem.Models
{
    public class Kennel
    {
        public int KennelId { get; set;}
        public string Description { get; set; }
        public int Capacity { get; set; }
        public double DailyCost { get; set; }
        public ICollection<Hospitalization> hospitalizations { get; set; }
    }
}
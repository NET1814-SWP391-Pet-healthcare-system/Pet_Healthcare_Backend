using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetHealthCareSystem.Models
{
    public class Vaccine
    {
        public int vaccineId { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public bool isAnnualVaccine { get; set; }
        public ICollection<PetVaccination> petVaccinations { get; set; } 
    }
}
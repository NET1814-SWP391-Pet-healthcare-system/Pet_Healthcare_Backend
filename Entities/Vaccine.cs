using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetHealthCareSystem.Models
{
    public class Vaccine
    {
        public int VaccineId { get; set;}
        public string Name { get; set;}
        public string Description {get; set;}
        public bool IsAnnualVaccine {get; set;}
        public ICollection<PetVaccination> PetVaccinations {get; set;} 
    }
}
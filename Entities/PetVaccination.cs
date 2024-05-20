using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetHealthCareSystem.Models
{
    public class PetVaccination
    {
        public Pet Pet{ get; set; }
        public Vaccine Vaccine{ get; set; }
        public DateOnly VaccinationDate { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Entities
{
    public class PetVaccination
    {
        public Pet pet{ get; set; }
        public Vaccine vaccine{ get; set; }
        public DateOnly vaccinationDate { get; set; }
    }
}
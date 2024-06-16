using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Entities
{
    [Table("PetVaccination")]
    public class PetVaccination
    {
        public int PetId { get; set; }
        public Pet? Pet{ get; set; }
        public int VaccineId { get; set; }
        public Vaccine? Vaccine { get; set; }
        public DateOnly? VaccinationDate { get; set; }
    }
}
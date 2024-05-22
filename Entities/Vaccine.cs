using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Entities
{
    [Table("Vaccine")]
    public class Vaccine
    {
        [Key]
        public int VaccineId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsAnnualVaccine { get; set; }
        public ICollection<PetVaccination> PetVaccinations { get; set; } 
    }
}
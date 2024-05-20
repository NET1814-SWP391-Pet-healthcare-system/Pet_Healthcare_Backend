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
        public int vaccineId { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public bool isAnnualVaccine { get; set; }
        public ICollection<PetVaccination> petVaccinations { get; set; } 
    }
}
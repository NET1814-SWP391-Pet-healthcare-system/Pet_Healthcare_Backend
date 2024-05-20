using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Entities
{
    [Table("Pet")]
    public class Pet
    {
        [Key]
        public int petId { get; set; }
        [ForeignKey("User")]
        public User customerId { get; set; }
        public string name { get; set; } 
        public string species { get; set; }
        public string breed { get; set; }
        public bool gender { get; set; }
        public double weight { get; set; }
        public string imageURL { get; set; }
        public ICollection<PetVaccination> petVaccinations { get; set; }
        public ICollection<Hospitalization> hospitalizations { get; set; }
    }
}
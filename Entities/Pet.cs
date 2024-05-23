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
        public int PetId { get; set; }
        public int CustomerId { get; set; }
        [ForeignKey("CustomerId")]
        public User Customer { get; set; }
        public string Name { get; set; } 
        public string Species { get; set; }
        public string Breed { get; set; }
        public bool Gender { get; set; }
        public double Weight { get; set; }
        public string ImageURL { get; set; }
        public ICollection<PetVaccination> PetVaccinations { get; set; }
        public ICollection<Hospitalization> Hospitalizations { get; set; }
        public ICollection<Appointment> Appointments { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Entities
{
    public class Pet
    {
        public int petId { get; set; }
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
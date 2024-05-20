using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetHealthCareSystem.Models
{
    public class Pet
    {
        public int PetId { get; set; }
        public User CustomerId { get; set;}
        public string Name { get; set;} 
        public string Species { get; set;}
        public string Breed { get; set;}
        public bool Gender { get; set;}
        public double Weight { get; set;}
        public string ImageURL { get; set;}
        public ICollection<PetVaccination> PetVaccinations { get; set;}
        public ICollection<Hospitalization> Hospitalizations { get; set;}
    }
}
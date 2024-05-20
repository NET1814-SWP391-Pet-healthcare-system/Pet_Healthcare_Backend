using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PetHealthCareSystem.Data.Enum;

namespace PetHealthCareSystem.Models
{
    public class User
    {
        public int UserId { get; set; }
        public Role Role{ get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }   
        public bool Gender { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Address { get; set; }
        public string Country { get; set; }
        public string ImageURL { get; set; }
        public bool IsActice { get; set; }
        public ICollection<Appointment> Appointments { get; set; }
        public ICollection<Pet> Pets { get; set; }
        public ICollection<Payment> Payments { get; set; }
        public ICollection<Hospitalization> Hospitalizations { get; set; }
        // Vet-specific properties
        public int? Rating { get; set; }
        public int? YearsOfExperience { get; set; }
    }
}
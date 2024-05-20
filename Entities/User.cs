using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Entities.Enum;

namespace Entities
{
    [Table("User")]
    public class User
    {
        [Key]
        public int userId { get; set; }
        [ForeignKey("Role")]
        public Role role{ get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }   
        public bool gender { get; set; }
        public string email { get; set; }
        public string userName { get; set; }
        public string password { get; set; }
        public string address { get; set; }
        public string country { get; set; }
        public string imageURL { get; set; }
        public bool isActice { get; set; }
        public ICollection<Appointment> appointments { get; set; }
        public ICollection<Pet> pets { get; set; }
        public ICollection<Payment> payments { get; set; }
        public ICollection<Hospitalization> hospitalizations { get; set; }
        // Vet-specific properties
        public int? rating { get; set; }
        public int? yearsOfExperience { get; set; }
    }
}
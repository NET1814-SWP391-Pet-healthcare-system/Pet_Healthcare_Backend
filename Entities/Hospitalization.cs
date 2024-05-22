using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Entities
{
    [Table("Hospitalization")]
    public class Hospitalization
    {
        [Key]
        public int HospitalizationId { get; set; }
        [ForeignKey("Pet")]
        public int PetId { get; set; }
        public Pet Pet { get; set; }
        [ForeignKey("Kennel")]
        public int KennelId { get; set; }
        public Kennel Kennel{ get; set; }
        [ForeignKey("User")]
        public int VetId { get; set; }
        public User Vet { get; set; }
        public DateOnly AdmissionDate { get; set; }
        public DateOnly DischargeDate { get; set; }
        public double TotalCost { get; set; }
        public ICollection<PetHealthTrack> PetHealthTracks { get; set; }
    }
}
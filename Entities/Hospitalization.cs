using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Entities
{
    public class Hospitalization
    {
        [Key]
        public int hospitalizationId { get; set; }
        [ForeignKey("Pet")]
        public int petId { get; set; }
        public Pet pet { get; set; }
        [ForeignKey("Kennel")]
        public int kennelId { get; set; }
        public Kennel kennel{ get; set; }
        [ForeignKey("User")]
        public int vetId { get; set; }
        public User vet { get; set; }
        public DateOnly admissionDate { get; set; }
        public DateOnly dischargeDate { get; set; }
        public double totalCost { get; set; }
        public ICollection<PetHealthTrack> petHealthTracks { get; set; }
    }
}
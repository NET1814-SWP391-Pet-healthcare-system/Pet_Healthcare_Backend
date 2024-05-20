using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Entities
{
    public class Hospitalization
    {
        public int hospitalizationId { get; set; }
        public Pet pet{ get; set; }
        public Kennel kennel{ get; set; }
        public User vet { get; set; }
        public DateOnly admissionDate { get; set; }
        public DateOnly dischargeDate { get; set; }
        public double totalCost { get; set; }
        public ICollection<PetHealthTrack> petHealthTracks { get; set; }
    }
}
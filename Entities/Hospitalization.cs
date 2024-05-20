using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetHealthCareSystem.Models
{
    public class Hospitalization
    {
        public int HospitalizationId { get; set; }
        public Pet Pet{ get; set; }
        public Kennel Kennel{ get; set; }
        public User Vet { get; set; }
        public DateOnly AdmissionDate { get; set; }
        public DateOnly DischargeDate { get; set; }
        public double TotalCost { get; set; }
        public ICollection<PetHealthTrack> PetHealthTracks { get; set; }
    }
}
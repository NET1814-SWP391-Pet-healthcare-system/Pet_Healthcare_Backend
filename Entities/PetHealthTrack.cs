using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PetHealthCareSystem.Data.Enum;

namespace PetHealthCareSystem.Models
{
    public class PetHealthTrack
    {
        public int PetHealthTrackId { get; set; }
        public Hospitalization Hospitalization { get; set; }
        public string Description { get; set; }
        public DateOnly DateOnly { get; set; }
        public PetStatus Status { get; set; }
    }
}
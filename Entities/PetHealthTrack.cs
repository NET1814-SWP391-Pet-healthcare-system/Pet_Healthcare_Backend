using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entities.Enum;

namespace Entities
{
    public class PetHealthTrack
    {
        public int petHealthTrackId { get; set; }
        public Hospitalization hospitalization { get; set; }
        public string description { get; set; }
        public DateOnly dateOnly { get; set; }
        public PetStatus status { get; set; }
    }
}
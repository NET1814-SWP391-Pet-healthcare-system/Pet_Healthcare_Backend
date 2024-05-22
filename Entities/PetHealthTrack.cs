using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Entities.Enum;

namespace Entities
{
    [Table("PetHealthTrack")]
    public class PetHealthTrack
    {
        [Key]
        public int PetHealthTrackId { get; set; }
        public int HospitalizationId { get; set; }
        [ForeignKey("HospitalizationId")]
        public Hospitalization Hospitalization { get; set; }
        public string Description { get; set; }
        public DateOnly DateOnly { get; set; }
        public PetStatus Status { get; set; }
    }
}
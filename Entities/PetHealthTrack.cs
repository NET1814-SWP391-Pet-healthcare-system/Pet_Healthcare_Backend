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
        public int petHealthTrackId { get; set; }
        [ForeignKey("Hospitalization")]
        public int hospitalizationId { get; set; }
        public Hospitalization hospitalization { get; set; }
        public string description { get; set; }
        public DateOnly dateOnly { get; set; }
        public PetStatus status { get; set; }
    }
}
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
        public int? PetId { get; set; }
        [ForeignKey("PetId")]
        public Pet? Pet { get; set; }
        public int? KennelId { get; set; }
        [ForeignKey("KennelId")]
        public Kennel? Kennel{ get; set; }
        public int? VetId { get; set; }
        [ForeignKey("VetId")]
        public Vet? Vet { get; set; }
        public DateOnly? AdmissionDate { get; set; }
        public DateOnly? DischargeDate { get; set; }
        public double? TotalCost { get; set; }
        public ICollection<PetHealthTrack>? PetHealthTracks { get; set; }
    }
}
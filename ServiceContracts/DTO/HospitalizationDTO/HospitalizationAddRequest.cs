using Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts.DTO.HospitalizationDTO
{
    public class HospitalizationAddRequest
    {
        [Required]
        public int? PetId { get; set; }
        [Required]
        public int? KennelId { get; set; }
        [Required]
        public int? VetId { get; set; }
        [Required]
        public DateOnly? AdmissionDate { get; set; }
        [Required]
        public DateOnly? DischargeDate { get; set; }
        [Required]
        public double? TotalCost { get; set; }

        public Hospitalization ToHospitalization()
        {
            return new Hospitalization
            {
                PetId = PetId,
                KennelId = KennelId,
                VetId = VetId,
                AdmissionDate = AdmissionDate,
                DischargeDate = DischargeDate,
                TotalCost = TotalCost
            };
        }
    }
}

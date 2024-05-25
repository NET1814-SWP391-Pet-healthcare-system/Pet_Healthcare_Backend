using Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts.DTO.HospitalizationDTO
{
    public class HospitalizationUpdateRequest
    {
        [Required]
        public int HospitalizationId { get; set; }
        public int? PetId { get; set; }
        public int? KennelId { get; set; }
        public int? VetId { get; set; }
        public DateOnly? AdmissionDate { get; set; }
        public DateOnly? DischargeDate { get; set; }
        public double? TotalCost { get; set; }

        public Hospitalization ToHospitalization()
        {
            return new Hospitalization
            {
                HospitalizationId = HospitalizationId,
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

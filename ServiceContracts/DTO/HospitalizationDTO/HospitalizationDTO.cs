using Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts.DTO.HospitalizationDTO
{
    public class HospitalizationDTO
    {
        public int HospitalizationId { get; set; }
        public int? PetId { get; set; }
        public int? KennelId { get; set; }
        public int? VetId { get; set; }
        public DateOnly? AdmissionDate { get; set; }
        public DateOnly? DischargeDate { get; set; }
        public double? TotalCost { get; set; }
    }
}

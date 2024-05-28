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
        [Required(ErrorMessage = "Must choose a date for the hospitalization")]
        [RegularExpression(@"^\d{4}-\d{2}-\d{2}$", ErrorMessage = "The date must be in the format YYYY-MM-DD.")]
        public string? AdmissionDate { get; set; }
        public string? DischargeDate { get; set; }

    }
}

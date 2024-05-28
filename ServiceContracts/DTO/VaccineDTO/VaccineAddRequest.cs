using Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts.DTO.VaccineDTO
{
    public class VaccineAddRequest
    {
        [Required(ErrorMessage = "Vaccine's name is required")]
        public string Name { get; set; }
        public string? Description { get; set; }
        [Required(ErrorMessage = "IsAnnualVaccine is required")]
        public bool IsAnnualVaccine { get; set; }
    }
}

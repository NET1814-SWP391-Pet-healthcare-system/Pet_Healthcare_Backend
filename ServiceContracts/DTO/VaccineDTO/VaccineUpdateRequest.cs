using Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts.DTO.VaccineDTO
{
    public class VaccineUpdateRequest
    {
        [Required(ErrorMessage = "Vaccine's id is required")]
        public int VaccineId { get; set; }
        public string? Name { get; set; } = string.Empty;
        public string? Description { get; set; } = string.Empty;
        public bool? IsAnnualVaccine { get; set; }
    }
}

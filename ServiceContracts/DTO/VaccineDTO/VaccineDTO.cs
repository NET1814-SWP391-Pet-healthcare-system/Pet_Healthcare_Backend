using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts.DTO.VaccineDTO
{
    public class VaccineDTO
    {
        public int VaccineId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public bool? IsAnnualVaccine { get; set; }
    }
}

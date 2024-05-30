using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts.DTO.KennelDTO
{
    public class KennelDto
    {
        public int KennelId { get; set; }
        public string? Description { get; set; }
        public int Capacity { get; set; }
        public double DailyCost { get; set; }
    }
}

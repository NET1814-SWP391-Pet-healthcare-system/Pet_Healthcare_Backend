using Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts.DTO.KennelDTO
{
    public class KennelUpdateRequest
    {
        public string Description { get; set; } = string.Empty;
        public double DailyCost { get; set; } = 0;
    }
}

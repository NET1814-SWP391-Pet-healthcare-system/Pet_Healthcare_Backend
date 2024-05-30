using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;

namespace ServiceContracts.DTO.ServiceDTO
{
    public class ServiceUpdateRequest
    {
        [Required(ErrorMessage = "Must have service name")]
        public string? Name { get; set; }
        [Required(ErrorMessage = "Must have Desc")]
        public string? Description { get; set; }
        [Required(ErrorMessage = "Must have Cost")]
        public double? Cost { get; set; }


    }
}

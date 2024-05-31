using Entities;
using Entities.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts.DTO.AppointmentDTO
{
    public class AppointmentRatingRequest
    {
        [Required]
        [Range(1,5)]
        public int Rating { get; set; }
        public string Comments { get; set; } = string.Empty;
    }
}

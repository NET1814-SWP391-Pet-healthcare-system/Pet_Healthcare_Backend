using Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts.DTO.SlotDTO
{
    public class SlotAddRequest
    {
        [Required(ErrorMessage = "Must enter a start time")]
        [RegularExpression(@"^([01]?[0-9]|2[0-3]):[0-5][0-9]:[0-5][0-9]$", ErrorMessage = "Invalid time format. Use HH:mm:ss.")]
        public required string StartTime { get; set; }
        [Required(ErrorMessage = "Must enter an end time")]
        [RegularExpression(@"^([01]?[0-9]|2[0-3]):[0-5][0-9]:[0-5][0-9]$", ErrorMessage = "Invalid time format. Use HH:mm:ss.")]
        public required string EndTime { get; set; }

        public Slot ToSlot()
        {
            return new Slot()
            {
                StartTime = TimeOnly.Parse(StartTime),
                EndTime = TimeOnly.Parse(EndTime),
            };
        }
    }
}

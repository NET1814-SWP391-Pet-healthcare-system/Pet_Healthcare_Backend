using Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts.DTO.RecordDTO
{
    public class RecordAddRequest
    {
        [Required]
        public int RecordId { get; set; }

        [Required]
        public int PetId { get; set; }

        public int NumberOfVisits { get; set; }

    }
}

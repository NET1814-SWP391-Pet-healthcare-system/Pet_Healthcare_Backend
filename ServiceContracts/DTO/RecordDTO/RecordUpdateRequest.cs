﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts.DTO.RecordDTO
{
    public class RecordUpdateRequest
    {
        [Required]
        public int PetId { get; set; }
        [Required]
        public int NumberOfVisits { get; set; }
    }
}

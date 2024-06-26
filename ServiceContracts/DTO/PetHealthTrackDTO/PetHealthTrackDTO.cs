﻿using Entities.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts.DTO.PetHealthTrackDTO
{
    public class PetHealthTrackDTO
    {
        public int PetHealthTrackId { get; set; }
        public int? HospitalizationId { get; set; }
        public string? PetName { get; set; }
        public string? PetImage { get; set; }
        public string? Description { get; set; }
        public DateOnly? Date { get; set; }
        public PetStatus? Status { get; set; }
    }
}

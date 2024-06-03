using Entities.Enum;
using Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceContracts.Mappers;

namespace ServiceContracts.DTO.PetHealthTrackDTO
{
    public class PetHealthTrackRemoveRequest
    {
        [Required(ErrorMessage = "Must enter a valid ID")]
        public int PetHealthTrackId { get; set; }


        public PetHealthTrack ToPetHealthTrack()
        {
            return new PetHealthTrack
            {
                PetHealthTrackId = PetHealthTrackId
            };
        }
    }
}

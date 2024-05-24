using Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts.DTO.HospitalizationDTO
{
    public class HospitalizationRemoveRequest
    {
        [Required]
        public int HospitalizationId { get; set; }

        public Hospitalization ToHospitalization()
        {
            return new Hospitalization
            {
                HospitalizationId = HospitalizationId,
            };
        }
    }
}

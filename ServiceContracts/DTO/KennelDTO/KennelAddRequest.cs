using Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts.DTO.KennelDTO
{
    public class KennelAddRequest
    {
        public string? Description { get; set; }
        [Required(ErrorMessage = "must enter capacity")]
        [Range(1, 4)]
        public int Capacity { get; set; }
        [Required(ErrorMessage = "must enter daily cost")]
        [Range(double.Epsilon, double.MaxValue)]
        public double DailyCost { get; set; }

        public Kennel ToKennel()
        {
            return new Kennel
            {
                Description = Description,
                Capacity = Capacity,
                DailyCost = DailyCost
            };
        }
    }
}

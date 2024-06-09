using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts.DTO.PetDTO
{
    public class PetDTO
    {
        public int Id { get; set; }
        public string CustomerId { get; set; }
        public string? Name { get; set; }
        public string? Species { get; set; }
        public string? Breed { get; set; }
        public bool? Gender { get; set; }
        public double? Weight { get; set; }
        public string? ImageURL {  get; set; }

    }
}

using Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace ServiceContracts.DTO.PetDTO
{
    public class PetAddRequest
    {
        [Required(ErrorMessage = "Customer username is required")]
        public string CustomerUsername { get; set; }
        public string? Name { get; set; }
        public string? Species { get; set; }
        public string? Breed { get; set; }
        public bool? Gender { get; set; }
        public double? Weight { get; set; }
        public string? ImageURL { get; set; }
        public IFormFile? imageFile { get; set; }
    }
}

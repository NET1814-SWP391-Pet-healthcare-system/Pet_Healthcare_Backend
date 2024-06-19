using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;

namespace ServiceContracts.DTO.UserDTO
{
    public class UserAddRequest
    {
        [Required]
        public string? FirstName { get; set; }
        [Required]
        public string? LastName { get; set; }
        public bool? Gender { get; set; } = true;
        [Required]
        [EmailAddress]
        public string? Email { get; set; }
        [Required]
        public string? Username { get; set; }
        [Required]
        public string? Password { get; set; }
        public string? Address { get; set; }
        public string? Country { get; set; }
        public string? ImageURL { get; set; }
        public bool? IsActice { get; set; } = true;
        [Required]
        public string? Role { get; set; }
        // Vet specific properties
        public int? Rating { get; set; }
        public int? YearsOfExperience { get; set; }
        public string? PhoneNumber { get; set; }
    }
}

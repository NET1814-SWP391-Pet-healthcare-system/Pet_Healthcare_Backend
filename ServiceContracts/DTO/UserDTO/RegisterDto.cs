using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts.DTO.UserDTO
{
    public class RegisterDto
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public bool Gender { get; set; } = true;
        [Required]
        public string? Username { get; set; }
        [Required]
        [EmailAddress]
        public string? Email { get; set;}
        [Required]
        public string? Password { get; set; }
        public string Address { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public string ImageURL { get; set; } = string.Empty;
    }
}

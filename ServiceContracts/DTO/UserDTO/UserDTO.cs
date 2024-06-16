using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts.DTO.UserDTO
{
    public class UserDTO
    {
        public string? UserId { get; set; }
        public string? UserName { get; set; }
        public string? Role { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public bool? Gender { get; set; }
        public string? Address { get; set; }
        public string? Country { get; set; }
        public bool? IsActive { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public int? Rating { get; set; }
        public int? YearsOfExperience { get; set; }
        public string? ImageUrl { get; set; }
    }
}

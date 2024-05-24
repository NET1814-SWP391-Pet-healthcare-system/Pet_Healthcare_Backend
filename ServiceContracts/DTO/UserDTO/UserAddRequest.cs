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
        public int? RoleId { get; set; }
        [Required]
        public string? FirstName { get; set; }
        [Required]
        public string? LastName { get; set; }
        public bool? Gender { get; set; }
        public string? Email { get; set; }
        [Required]
        public string? Username { get; set; }
        [Required]
        public string? Password { get; set; }
        public string? Address { get; set; }
        public string? Country { get; set; }
        public string? ImageURL { get; set; }
        public bool? IsActice { get; set; } = true;

        public User ToUser()
        {
            return new User
            {
                RoleId = RoleId,
                FirstName = FirstName,
                LastName = LastName,
                Gender = Gender,
                Email = Email,
                Username = Username,
                Password = Password,
                Address = Address,
                Country = Country,
                ImageURL = ImageURL,
                IsActice = IsActice
            };
        }
    }
}

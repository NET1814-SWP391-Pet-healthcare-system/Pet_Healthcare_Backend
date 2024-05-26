using Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts.DTO.UserDTO
{
    public class UserUpdateRequest
    {
        public int UserId { get; set; }
        public int? RoleId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public bool? Gender { get; set; }
        public string? Email { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
        public string? Address { get; set; }
        public string? Country { get; set; }
        public string? ImageURL { get; set; }
        public bool? IsActive { get; set; }

        public User ToUser()
        {
            return new User
            {
                UserId = UserId,
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
                IsActive = IsActive
            };
        }
    }
}

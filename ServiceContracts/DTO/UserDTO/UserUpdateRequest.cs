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
        //public string? FirstName { get; set; }
        //public string? LastName { get; set; }
        //public bool? Gender { get; set; }
        public string? Email { get; set; }
        public string? Username { get; set; }
        //public string? Password { get; set; }
        //public string? Address { get; set; }
        //public string? Country { get; set; }
        //public string? ImageURL { get; set; }
        //public bool? IsActive { get; set; }
        public int? Rating { get; set; }
        public int? YearsOfExperience { get; set; }

        public User ToUser()
        {
            return new User
            {
                //UserId = UserId,
                //FirstName = FirstName,
                //LastName = LastName,
                //Gender = Gender,
                //Email = Email,
                //Username = Username,
                //Password = Password,
                //Address = Address,
                //Country = Country,
                //ImageURL = ImageURL,
                //IsActive = IsActive
            };
        }

        public Customer ToCustomer()
        {
            return new Customer
            {
                //UserId = UserId,
                //FirstName = FirstName,
                //LastName = LastName,
                //Gender = Gender,
                //Email = Email,
                //Username = Username,
                //Password = Password,
                //Address = Address,
                //Country = Country,
                //ImageURL = ImageURL,
                //IsActive = IsActive
            };
        }

        public Vet ToVet()
        {
            return new Vet
            {
                //UserId = UserId,
                //FirstName = FirstName,
                //LastName = LastName,
                //Gender = Gender,
                //Email = Email,
                //Username = Username,
                //Password = Password,
                //Address = Address,
                //Country = Country,
                //ImageURL = ImageURL,
                //IsActive = IsActive,
                //Rating = Rating,
                //YearsOfExperience = YearsOfExperience
            };
        }
    }
}

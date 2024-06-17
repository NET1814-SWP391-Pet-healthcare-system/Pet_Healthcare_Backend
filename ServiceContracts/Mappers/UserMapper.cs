using Entities;
using ServiceContracts.DTO.UserDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts.Mappers
{
    public static class UserMapper
    {
        public static UserDTO ToUserDto(this UserUpdateRequest userUpdateRequest)
        {
            return new UserDTO
            {
                UserName = userUpdateRequest.Username,
                FirstName = userUpdateRequest.FirstName,
                LastName = userUpdateRequest.LastName,
                Gender = userUpdateRequest.Gender,
                Address = userUpdateRequest.Address,
                Country = userUpdateRequest.Country,
                IsActive = userUpdateRequest.IsActive,
                Rating = userUpdateRequest.Rating,
                YearsOfExperience = userUpdateRequest.YearsOfExperience
            };
        }

        public static UserDTO ToUserDtoFromUser(this User user)
        {
            return new UserDTO
            {
                UserId = user.Id,
                UserName = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Gender = user.Gender,
                Email = user.Email,
                Address = user.Address,
                Country = user.Country,
                IsActive = user.IsActive,
                ImageUrl = user.ImageURL,
                PhoneNumber = user.PhoneNumber
            };
        }

        public static UserDTO ToUserDtoFromCustomer(this Customer customer)
        {
            return new UserDTO
            {
                UserId = customer.Id,
                UserName = customer.UserName,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                Gender = customer.Gender,
                Email = customer.Email,
                Address = customer.Address,
                Country = customer.Country,
                IsActive = customer.IsActive,
                ImageUrl = customer.ImageURL,
                PhoneNumber= customer.PhoneNumber
            };
        }

        public static UserDTO ToUserDtoFromVet(this Vet vet)
        {
            return new UserDTO
            {
                UserId = vet.Id,
                UserName = vet.UserName,
                FirstName = vet.FirstName,
                LastName = vet.LastName,
                Gender = vet.Gender,
                Email = vet.Email,
                Address = vet.Address,
                Country = vet.Country,
                IsActive = vet.IsActive,
                ImageUrl = vet.ImageURL,
                Rating = vet.Rating,
                YearsOfExperience = vet.YearsOfExperience,
                PhoneNumber = vet.PhoneNumber
            };
        }
    }
}

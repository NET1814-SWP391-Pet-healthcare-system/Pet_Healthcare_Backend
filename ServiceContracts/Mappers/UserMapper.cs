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
                Email = userUpdateRequest.Email,
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
                UserName = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Gender = user.Gender,
                Email = user.Email,
                Address = user.Address,
                Country = user.Country,
                IsActive = user.IsActive
            };
        }
    }
}

using Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.General;
using ServiceContracts.DTO.UserDTO;
using ServiceContracts.Mappers;

namespace PetHealthCareSystem_BackEnd.Extensions
{
    public static class UserManagerExtension
    {
        public async static Task<UserDTO> UpdateUserAsync(this UserManager<User> userManager, string userId, UserUpdateRequest userUpdateRequest)
        {
            var existingUser = await userManager.FindByIdAsync(userId);
            if(existingUser == null)
            {
                return null;
            }
            existingUser.UserName = (userUpdateRequest.Username != "") ? userUpdateRequest.Username : existingUser.UserName;
            existingUser.Email = (userUpdateRequest.Email != "") ? userUpdateRequest.Email : existingUser.Email;
            existingUser.FirstName = (userUpdateRequest.FirstName != "") ? userUpdateRequest.FirstName : existingUser.FirstName;
            existingUser.LastName = (userUpdateRequest.LastName != "") ? userUpdateRequest.LastName : existingUser.LastName;
            existingUser.Gender = (userUpdateRequest.Gender != null) ? userUpdateRequest.Gender : existingUser.Gender;
            existingUser.Address = (userUpdateRequest.Address != "") ? userUpdateRequest.Address : existingUser.Address;
            existingUser.Country = (userUpdateRequest.Country != "") ? userUpdateRequest.Country : existingUser.Country;
            existingUser.ImageURL = (userUpdateRequest.ImageURL != "") ? userUpdateRequest.ImageURL : existingUser.ImageURL;
            existingUser.IsActive = (userUpdateRequest.IsActive != null) ? userUpdateRequest.IsActive : existingUser.IsActive;  
            if(await userManager.IsInRoleAsync(existingUser, "Customer"))
            {
                Customer customer = existingUser as Customer;
                await userManager.UpdateAsync(customer);
            }
            else if(await userManager.IsInRoleAsync(existingUser, "Vet"))
            {
                Vet vet = existingUser as Vet;
                vet.Rating = (userUpdateRequest.Rating != null) ? userUpdateRequest.Rating : vet.Rating;
                vet.YearsOfExperience = (userUpdateRequest.YearsOfExperience != null) ? userUpdateRequest.YearsOfExperience : vet.YearsOfExperience;
                await userManager.UpdateAsync(vet);
            }
            else
            {
                await userManager.UpdateAsync(existingUser);
            }
            var result = userUpdateRequest.ToUserDto();
            result.UserId = existingUser.Id;
            return result;
        }

        public async static Task<IEnumerable<UserDTO>> GetUserDtosInRoleAsync(this UserManager<User> userManager,string role)
        {
            List<UserDTO> result = new List<UserDTO>();
            switch(role.ToUpper())
            {
                case "CUSTOMER":
                    var customerList = await userManager.GetUsersInRoleAsync("Customer");
                    foreach(var user in customerList)
                    {
                        var customer = user as Customer;
                        UserDTO userDTO = new UserDTO()
                        {
                            UserId = customer.Id,
                            UserName = customer.UserName,
                            FirstName = customer.FirstName,
                            LastName = customer.LastName,
                            Gender = customer.Gender,
                            Address = customer.Address,
                            Country = customer.Country,
                            IsActive = customer.IsActive,
                            Email = customer.Email,
                            PhoneNumber = customer.PhoneNumber
                        };
                        result.Add(userDTO);
                    }

                    break;

                case "VET":
                    var vetList = await userManager.GetUsersInRoleAsync("Vet");
                    foreach(var user in vetList)
                    {
                        var vet = user as Vet;
                        UserDTO userDTO = new UserDTO()
                        {
                            UserId = vet.Id,
                            UserName = vet.UserName,
                            FirstName = vet.FirstName,
                            LastName = vet.LastName,
                            Gender = vet.Gender,
                            Address = vet.Address,
                            Country = vet.Country,
                            IsActive = vet.IsActive,
                            Email = vet.Email,
                            PhoneNumber = vet.PhoneNumber,
                            Rating = vet.Rating,
                            YearsOfExperience = vet.YearsOfExperience
                        };
                        result.Add(userDTO);
                    }
                    break;

                case "EMPLOYEE":
                    var employeeList = await userManager.GetUsersInRoleAsync("Employee");
                    foreach(var user in employeeList)
                    {
                        UserDTO userDTO = new UserDTO()
                        {
                            UserId = user.Id,
                            UserName = user.UserName,
                            FirstName = user.FirstName,
                            LastName = user.LastName,
                            Gender = user.Gender,
                            Address = user.Address,
                            Country = user.Country,
                            IsActive = user.IsActive,
                            Email = user.Email,
                            PhoneNumber = user.PhoneNumber
                        };
                        result.Add(userDTO);
                    }
                    break;
            }
            return result;
        }
    }
}

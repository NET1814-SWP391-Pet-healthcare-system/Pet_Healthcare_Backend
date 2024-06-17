using Braintree;
using Entities;
using Entities.Constants;
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
            UserDTO result = null;
            if(existingUser == null)
            {
                return null;
            }

            existingUser.UserName = (string.IsNullOrEmpty(userUpdateRequest.Username)) ? existingUser.UserName : userUpdateRequest.Username;

            existingUser.FirstName = (string.IsNullOrEmpty(userUpdateRequest.FirstName)) ? existingUser.FirstName : userUpdateRequest.FirstName;

            existingUser.LastName = (string.IsNullOrEmpty(userUpdateRequest.LastName)) ? existingUser.LastName : userUpdateRequest.LastName;

            existingUser.Gender = (userUpdateRequest.Gender != null) ? existingUser.Gender : userUpdateRequest.Gender;

            existingUser.Address = (string.IsNullOrEmpty(userUpdateRequest.Address)) ? existingUser.Address : userUpdateRequest.Address;

            existingUser.Country = (string.IsNullOrEmpty(userUpdateRequest.Country)) ? existingUser.Country : userUpdateRequest.Country;

            existingUser.ImageURL = (string.IsNullOrEmpty(userUpdateRequest.ImageURL)) ? existingUser.ImageURL : userUpdateRequest.ImageURL;

            existingUser.IsActive = (userUpdateRequest.IsActive != null) ? existingUser.IsActive : userUpdateRequest.IsActive;

            existingUser.PhoneNumber = (string.IsNullOrEmpty(userUpdateRequest.PhoneNumber)) ? existingUser.PhoneNumber : userUpdateRequest.PhoneNumber;
          
            if(await userManager.IsInRoleAsync(existingUser, UserRole.Customer))
            {
                Entities.Customer customer = existingUser as Entities.Customer;
                await userManager.UpdateAsync(customer);
                result = customer.ToUserDtoFromCustomer();
                result.Role = UserRole.Customer;
            }
            else if(await userManager.IsInRoleAsync(existingUser, UserRole.Vet))
            {
                Vet vet = existingUser as Vet;
                vet.Rating = (userUpdateRequest.Rating != null) ? userUpdateRequest.Rating : vet.Rating;
                vet.YearsOfExperience = (userUpdateRequest.YearsOfExperience != null) ? userUpdateRequest.YearsOfExperience : vet.YearsOfExperience;
                await userManager.UpdateAsync(vet);
                result = vet.ToUserDtoFromVet();
                result.Role = UserRole.Vet;
            }
            else if(await userManager.IsInRoleAsync(existingUser, UserRole.Employee))
            {
                await userManager.UpdateAsync(existingUser);
                result = existingUser.ToUserDtoFromUser();
                result.Role = UserRole.Employee;
            }
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
                        var customer = user as Entities.Customer;
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

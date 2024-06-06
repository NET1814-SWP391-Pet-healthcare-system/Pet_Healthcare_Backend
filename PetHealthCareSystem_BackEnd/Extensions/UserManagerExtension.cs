using Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.General;
using ServiceContracts.DTO.UserDTO;
using ServiceContracts.Mappers;

namespace PetHealthCareSystem_BackEnd.Extensions
{
    public static class UserManagerExtension
    {
        public async static Task<UserDTO> UpdateUserAsync(this UserManager<User> userManager, string username, UserUpdateRequest userUpdateRequest)
        {
            var existingUser = await userManager.FindByNameAsync(username);
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
    }
}

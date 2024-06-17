using Entities;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Hosting;
using ServiceContracts;
using ServiceContracts.DTO.HospitalizationDTO;
using ServiceContracts.DTO.PetDTO;
using ServiceContracts.Mappers;
using Services;
namespace PetHealthCareSystem_BackEnd.Validations
{

    public class HospitalizationValidation
    {

        private static IUserService _userService;
        private static  UserManager<User> _userManager;
        private static IKennelService _kennelService;
        private static  IPetService _petService;


        public static string HospitalizationVerification(HospitalizationAddRequest hospitalization,IKennelService kennelService, IPetService petService,UserManager<User> userManager,IUserService userService)
        {
            var adDate = DateOnly.Parse(hospitalization.AdmissionDate);
            var disDate = DateOnly.Parse(hospitalization.DischargeDate);
            _kennelService = kennelService;
            _petService = petService;
            _userManager = userManager;
            _userService = userService;    
            if (adDate > disDate)
            {
                return "Start date cannot be greater than end date";
            }

            if (hospitalization.TotalCost < 0)
            {
                return "Total cost cannot be negative";
            }

            var pet = _petService.GetPetById(hospitalization.PetId);
            if (pet == null)
            {
                return "This pet does not exist";
            }

            var kennel = _kennelService.GetKennelByIdAsync(hospitalization.KennelId);
            if (kennel == null)
            {
                return "This kennel does not exist";
            }

            var vet = _userManager.FindByIdAsync(hospitalization.VetId);
            if (vet == null || _userService.GetAvailableVetById(hospitalization.VetId) == null)
            {
                return "This vet does not exist";
            }
            return null;
        }
    }

}
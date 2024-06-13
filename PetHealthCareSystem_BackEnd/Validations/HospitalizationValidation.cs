using Entities;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using ServiceContracts;
using ServiceContracts.DTO.HospitalizationDTO;
using ServiceContracts.DTO.PetDTO;
using Services;
namespace PetHealthCareSystem_BackEnd.Validations
{

    public class HospitalizationValidation
    {

        private static IUserService _userService;
        private static  UserManager<User> _userManager;
        private static IKennelService _kennelService;
        private static  IPetService _petService;


        public static string HospitalizationVerification(HospitalizationAddRequest hospitalization,IKennelService kennelService, IPetService petService,UserManager<User> userManager)
        {
            DateTime adDate = DateTime.Parse(hospitalization.AdmissionDate);
            DateTime disDate = DateTime.Parse(hospitalization.DischargeDate);
            _kennelService = kennelService;
            _petService = petService;
            _userManager=userManager;

            if (adDate > disDate)
            {
                return "Start date cannot be greater than end date";
            }

            if (hospitalization.TotalCost < 0)
            {
                return "Total cost cannot be negative";
            }

            var pet =  _petService.GetPetById(hospitalization.PetId);
            if (pet == null)
            {
                return "This pet does not exist";
            }

            var kennel = _kennelService.GetKennelByIdAsync(hospitalization.KennelId);
            if (kennel == null)
            {
                return "This kennel does not exist";
            }

            var vet = _userManager.FindByNameAsync(hospitalization.VetId);
            if (vet == null)
            {
                return "This vet does not exist";
            }

            return null;
        }
    }

}
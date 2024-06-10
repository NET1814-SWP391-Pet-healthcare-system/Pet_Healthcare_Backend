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

        private readonly IUserService _userService;
        private  readonly UserManager<User> _userManager;
        private readonly IKennelService _kennelService;
        private readonly IPetService _petService;

        public HospitalizationValidation(IUserService userService, UserManager<User> userManager, IKennelService kennelService, IPetService petService)
        {
            _userService = userService;
            _userManager = userManager;
            _kennelService = kennelService;
            _petService = petService;
        }   
        public async Task<string> HospitalizationVerificaiton(HospitalizationAddRequest hospitalization)
        {
            DateTime adDate = DateTime.Parse(hospitalization.AdmissionDate);
            DateTime disDate = DateTime.Parse(hospitalization.DischargeDate);
            var pet =  await _petService.GetPetById(hospitalization.PetId);
            var kennel = await _kennelService.GetKennelByIdAsync(hospitalization.KennelId);
            var vet = await _userManager.FindByNameAsync(hospitalization.VetId);
            var actions = new List<Tuple<bool, string>>
{
    new Tuple<bool, string>(adDate > disDate, "Start date cannot be greater than end date"),
    new Tuple<bool, string>(hospitalization.TotalCost < 0, "Total cost cannot be negative"),
    new Tuple<bool, string>(pet==null, "This pet does not exist"),
    new Tuple<bool, string>(vet==null, "This vet does not exist"),
    new Tuple<bool, string>(kennel == null, "This kennel does not exist"),
};

            return actions.FirstOrDefault(x => x.Item1)?.Item2;
        }
    }
}
using Entities;
using ServiceContracts;

namespace PetHealthCareSystem_BackEnd.Validations
{
    public static class RecordValidation
    {
        private static readonly IRecordService _recordService;
        private static readonly IPetService _petService;
        public static bool IsRecordValid(Record record)
        {
            int petId = (int)record.PetId;
            if(_petService.GetPetById(petId)==null)
            {
                return false;
            }
            return true;
        }
    }
}

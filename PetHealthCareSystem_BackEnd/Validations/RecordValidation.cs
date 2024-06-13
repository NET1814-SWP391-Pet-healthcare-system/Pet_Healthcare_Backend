using Entities;
using ServiceContracts;

namespace PetHealthCareSystem_BackEnd.Validations
{
    public static class RecordValidation
    {
        private static  IRecordService? _recordService;
        private static IPetService? _petService;
        public static bool IsRecordValid(Record record,IRecordService recordService, IPetService petService)
        {
            _recordService = recordService;
            _petService = petService;
            if (record.PetId == null) return false;
            int petId = (int)record.PetId;

            var Pet = _petService.GetPetById(petId);
            if(Pet==null)
            {
                return false;
            }

            var records =  _recordService.GetRecordsAsync();
            return true;
        }
    }
}

using Entities;
using ServiceContracts;
using ServiceContracts.DTO.AppointmentDetailDTO;
using Services;

namespace PetHealthCareSystem_BackEnd.Validations
{
    public static class AppointmentDetailValidation
    {
        private static readonly IAppointmentService _appointmentService;
        private static readonly IRecordService _recordService;
        public static bool IsAppointmentDetailValid(AppointmentDetail appointmentDetail)
        {
            int appointmentId = (int)appointmentDetail.AppointmentId;
            int recordId = (int)appointmentDetail.RecordId;
            if(_appointmentService.GetAppointmentByIdAsync(appointmentId)==null || _recordService.GetRecordByIdAsync(recordId)==null)
            {
                return false;
            }
            return true;
        }
        
    }
}

using Entities;
using ServiceContracts.DTO.RecordDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts.Mappers
{
    public static class RecordMapper
    {
        public static RecordDto ToRecordDto(this Record request, IAppointmentDetailService appointmentDetailService)
        {
            int NumOfVist = 0;
            if (request.PetId != null)
            {
                var list =  appointmentDetailService.GetAppointmentDetailsByPetIdAsync((int)request.PetId).Result.ToList();
                NumOfVist = list.Count();
  
            }
            return new RecordDto
            {
                RecordId = request.RecordId,
                PetName = request.Pet.Name,
                NumberOfVisits = NumOfVist
            };
        }
        public static Record ToRecordFromAdd(this RecordAddRequest request, IAppointmentDetailService appointmentDetailService)
        {
            int NumOfVist = 0;
            if (request.PetId != null)
            {
                NumOfVist = appointmentDetailService.GetAppointmentDetailsByPetIdAsync((int)request.PetId).Result.ToList().Count;
            }
            return new Record
            {
                PetId = request.PetId,
                NumberOfVisits = NumOfVist
            };
        }
        public static Record ToRecordFromUpdate(this RecordUpdateRequest request,IAppointmentDetailService appointmentDetailService)
        {
            int NumOfVist = 0;
            if (request.PetId != null)
            {
                NumOfVist = appointmentDetailService.GetAppointmentDetailsByPetIdAsync((int)request.PetId).Result.ToList().Count;
            }

            return new Record
            {
                PetId = request.PetId,
                NumberOfVisits = NumOfVist
            };
        }
    }
}

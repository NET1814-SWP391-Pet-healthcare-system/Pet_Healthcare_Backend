using Entities.Enum;
using Entities;
using ServiceContracts.DTO.HospitalizationDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts.Mappers
{
    public static class HospitalizationMapper
    {
        public static HospitalizationDTO ToHospitalizationDto(this Hospitalization hospitalizationModel)
        {
            return new HospitalizationDTO()
            {
                //HospitalizationId = hospitalizationModel.HospitalizationId,
                //PetId = hospitalizationModel.PetId,
                //KennelId = hospitalizationModel.KennelId,
                //VetId = hospitalizationModel.VetId,
                //AdmissionDate = hospitalizationModel.AdmissionDate,
                //DischargeDate = hospitalizationModel.DischargeDate,
                //TotalCost = hospitalizationModel.TotalCost
            };
        }

        public static Hospitalization ToHospitalizationFromAdd(this HospitalizationAddRequest hospitalizationAddRequest, int petId, int kennelId, int vetId,double totalCost)
        {
            return new Hospitalization()
            {
                //PetId = petId,
                //KennelId = kennelId,
                //VetId = vetId,
                //AdmissionDate = DateOnly.Parse(hospitalizationAddRequest.AdmissionDate),
                //DischargeDate = DateOnly.Parse(hospitalizationAddRequest.DischargeDate),
                //TotalCost = totalCost,
            };
        }
    }
}


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
                HospitalizationId = hospitalizationModel.HospitalizationId,
                PetId = hospitalizationModel.PetId,
                KennelId = hospitalizationModel.KennelId,
                VetId = hospitalizationModel.VetId,
                AdmissionDate = hospitalizationModel.AdmissionDate,
                DischargeDate = hospitalizationModel.DischargeDate,
                TotalCost = hospitalizationModel.TotalCost,
                PaymentStatus = hospitalizationModel.PaymentStatus,
                VetName = hospitalizationModel?.Vet.UserName,
                PetName = hospitalizationModel?.Pet.Name,
                KennelDescription = hospitalizationModel?.Kennel.Description
            };
        }

        public static Hospitalization ToHospitalizationFromAdd(this HospitalizationAddRequest hospitalizationAddRequest)
        {
            return new Hospitalization()
            {
                PetId = hospitalizationAddRequest.PetId,
                KennelId = hospitalizationAddRequest.KennelId,
                VetId = hospitalizationAddRequest.VetId,
                AdmissionDate = DateOnly.Parse(hospitalizationAddRequest.AdmissionDate),
                DischargeDate = DateOnly.Parse(hospitalizationAddRequest.DischargeDate),
                //TotalCost = hospitalizationAddRequest.TotalCost
            };
        }

        public static Hospitalization ToHospitalizationUpdate(this HospitalizationUpdateRequest hospitalizationUpdateRequest)
        {
            return new Hospitalization()
            {
                DischargeDate = DateOnly.Parse(hospitalizationUpdateRequest.DischargeDate),
            };
        }
    }
}


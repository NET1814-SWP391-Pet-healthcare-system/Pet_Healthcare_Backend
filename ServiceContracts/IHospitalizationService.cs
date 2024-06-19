using Entities;
using ServiceContracts.DTO.HospitalizationDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts
{
    public interface IHospitalizationService
    {
        Task<HospitalizationDTO?> AddHospitalization(Hospitalization request);
        Task<Hospitalization?> GetHospitalizationById(int id);
        Task <IEnumerable<Hospitalization>> GetHospitalizations();
        Task<HospitalizationDTO?> UpdateHospitalization(Hospitalization request);
        Task<bool> RemoveHospitalization(int id);
        Task<Hospitalization?> GetHospitalizationByPetId(int id);
        Task<IEnumerable<Hospitalization>> GetAllHospitalizationByVetId(string id);
        Task<IEnumerable<Hospitalization>> GetAllHospitalizationByPetId(int id);
        Task<bool> IsVetFree(string id, DateOnly AddmissionDate, DateOnly DischargeDate);
    }
}
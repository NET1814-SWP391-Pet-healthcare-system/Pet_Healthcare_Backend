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
        Task<Hospitalization> AddHospitalization(Hospitalization request);
        Task<Hospitalization?> GetHospitalizationById(int id);
        Task <IEnumerable<Hospitalization>> GetHospitalizations();
        Task<Hospitalization?> UpdateHospitalization(int id, Hospitalization request);
        Task<Hospitalization?> RemoveHospitalization(int id);
        Task<Hospitalization?> GetHospitalizationByPetId(int id);
        Task<List<Hospitalization?>> GetAllHospitalizationByVetId(string id);
        Task<List<Hospitalization?>> GetAllHospitalizationByPetId(int id);
    }
}
using RepositoryContracts;
using ServiceContracts.DTO;
using ServiceContracts.DTO.HospitalizationDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;
using ServiceContracts;
using Org.BouncyCastle.Bcpg.OpenPgp;

namespace Services
{
    public class HospitalizationService : IHospitalizationService
    {
        private readonly IHospitalizationRepository _hospitalizationRepository;

        public HospitalizationService(IHospitalizationRepository hospitalizationRepository)
        {
            _hospitalizationRepository = hospitalizationRepository;
        }
        public async Task<Hospitalization> AddHospitalization(Hospitalization request)
        {
            return await _hospitalizationRepository.Add(request);
        }

        public async Task<Hospitalization?> UpdateHospitalization(int id,Hospitalization request)
        {
            var existingHospitalization = await _hospitalizationRepository.GetById(id);
            if (existingHospitalization == null || request == null)
            {
                return null;
            }
            existingHospitalization.DischargeDate = request.DischargeDate;

            return await _hospitalizationRepository.Update(id,existingHospitalization);
        }

        public async Task<Hospitalization?> RemoveHospitalization(int id)
        {
            return await _hospitalizationRepository.Remove(id);
        }

        public async Task<Hospitalization?> GetHospitalizationById(int id)
        {
            return await _hospitalizationRepository.GetById(id);
        }

        public async Task<IEnumerable<Hospitalization>> GetHospitalizations()
        {
            return await _hospitalizationRepository.GetAll();
        }
        public async Task<Hospitalization?> GetHospitalizationByPetId(int id)
        {
            return await _hospitalizationRepository.GetByPetId(id);
        }
        public async Task<Hospitalization?> GetHospitalizationByVetId(string id)
        {
            return await _hospitalizationRepository.GetByVetId(id);
        }

        async Task<List<Hospitalization?>> IHospitalizationService.GetAllHospitalizationByVetId(string id)
        {
            return await _hospitalizationRepository.GetAllByVetId(id);
        }

         async Task<List<Hospitalization?>> IHospitalizationService.GetAllHospitalizationByPetId(int id)
        {
            return await _hospitalizationRepository.GetAllByPetId(id);
        }
    }
}

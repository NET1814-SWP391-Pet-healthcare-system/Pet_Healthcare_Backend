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
            if (request == null)
            {
                return null;
            }
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
            if (_hospitalizationRepository.GetById(id) == null)
            {
                return null;
            }
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
    }
}

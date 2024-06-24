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
using ServiceContracts.Mappers;
using Repositories;

namespace Services
{
    public class HospitalizationService : IHospitalizationService
    {
        private readonly IHospitalizationRepository _hospitalizationRepository;

        public HospitalizationService(IHospitalizationRepository hospitalizationRepository)
        {
            _hospitalizationRepository = hospitalizationRepository;
        }
        public async Task<Hospitalization?> AddHospitalization(Hospitalization request)
        {
            if(request==null)
            {
                return null;
            }
            await _hospitalizationRepository.Add(request);
            return request;
        }

        public async Task<Hospitalization?> UpdateHospitalization(Hospitalization request)
        {
            if(request==null)
            {
                return null;
            }
            var hospitalization = await _hospitalizationRepository.GetById(request.HospitalizationId);
            hospitalization.DischargeDate = request.DischargeDate == null ? hospitalization.DischargeDate:request.DischargeDate;
            await _hospitalizationRepository.Update(hospitalization);
            return hospitalization;

        }

        public async Task<bool> RemoveHospitalization(int id)
        {
            var hospitalization = await _hospitalizationRepository.GetById(id);
            if(hospitalization==null)
            {
                return false;
            }
            return await _hospitalizationRepository.Remove(hospitalization);
        }

        public async Task<Hospitalization?> GetHospitalizationById(int id)
        {
            var hospitalization = await _hospitalizationRepository.GetById(id);
            if(hospitalization == null)
            {
                return null;
            }
            return hospitalization;
        }

        public async Task<IEnumerable<Hospitalization>> GetHospitalizations()
        {
            return await _hospitalizationRepository.GetAll();
        }
        public async Task<Hospitalization?> GetHospitalizationByPetId(int id)
        {
           var hospi =  await _hospitalizationRepository.GetByPetId(id);
            if(hospi==null)
            {
                return null;
            }
            return hospi;
        }
        public async Task<Hospitalization?> GetHospitalizationByVetId(string id)
        {
            var hospi = await _hospitalizationRepository.GetByVetId(id);
            if(hospi == null)
            {
                return null;
            }
            return hospi;
        }

        public async Task<IEnumerable<Hospitalization>> GetAllHospitalizationByVetId(string id)
        {
            return await _hospitalizationRepository.GetAllByVetId(id);
        }

        public async Task<IEnumerable<Hospitalization>> GetAllHospitalizationByPetId(int id)
        {
            return await _hospitalizationRepository.GetAllByPetId(id);
        }
        public async Task<bool> IsVetFree(string id, DateOnly AddmissionDate, DateOnly DischargeDate)
        {
            return await _hospitalizationRepository.IsVetDateConflict(id, AddmissionDate, DischargeDate);
        }
    }
}

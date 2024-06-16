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
        public async Task<HospitalizationDTO?> AddHospitalization(Hospitalization request)
        {
            if(request==null)
            {
                return null;
            }
            await _hospitalizationRepository.Add(request);
            return request.ToHospitalizationDto();
        }

        public async Task<HospitalizationDTO?> UpdateHospitalization(Hospitalization request)
        {
            if(request==null)
            {
                return null;
            }
            var hospitalization = await _hospitalizationRepository.GetById(request.HospitalizationId);
            if(hospitalization==null)
            {
                return null;
            }
            hospitalization.VetId = hospitalization.VetId;
            hospitalization.PetId = hospitalization.PetId;
            hospitalization.KennelId =hospitalization.KennelId;
            hospitalization.AdmissionDate = hospitalization.AdmissionDate;
            hospitalization.DischargeDate = request.DischargeDate == null ? hospitalization.DischargeDate:request.DischargeDate;
            hospitalization.TotalCost = hospitalization.TotalCost;

            await _hospitalizationRepository.Update(hospitalization);
            return hospitalization.ToHospitalizationDto();

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

        public async Task<HospitalizationDTO?> GetHospitalizationById(int id)
        {
            var hospitalization = await _hospitalizationRepository.GetById(id);
            if(hospitalization == null)
            {
                return null;
            }
            return hospitalization.ToHospitalizationDto();
        }

        public async Task<IEnumerable<Hospitalization>> GetHospitalizations()
        {
            return await _hospitalizationRepository.GetAll();
        }
        public async Task<HospitalizationDTO?> GetHospitalizationByPetId(int id)
        {
           var hospi =  await _hospitalizationRepository.GetByPetId(id);
            if(hospi==null)
            {
                return null;
            }
            return hospi.ToHospitalizationDto();
        }
        public async Task<HospitalizationDTO?> GetHospitalizationByVetId(string id)
        {
            var hospi = await _hospitalizationRepository.GetByVetId(id);
            if(hospi == null)
            {
                return null;
            }
            return hospi.ToHospitalizationDto();
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

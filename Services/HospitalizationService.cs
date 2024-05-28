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
        public bool AddHospitalization(Hospitalization? request)
        {
            if (request == null)
            {
                return false;
            }
            return _hospitalizationRepository.Add(request);
        }

        public bool UpdateHospitalization(int id,Hospitalization? request)
        {
            var existingHospitalization = _hospitalizationRepository.GetById(id);
            if (existingHospitalization == null || request == null)
            {
                return false;
            }
            existingHospitalization.PetId = request.PetId;
            existingHospitalization.KennelId = request.KennelId;
            existingHospitalization.VetId = request.VetId;
            existingHospitalization.AdmissionDate = request.AdmissionDate;
            existingHospitalization.DischargeDate = request.DischargeDate;
            existingHospitalization.TotalCost = request.TotalCost;

            return _hospitalizationRepository.Update(existingHospitalization);
        }

        public bool RemoveHospitalization(int id)
        {
            if (_hospitalizationRepository.GetById(id) == null)
            {
                return false;
            }
            var hospitalization = id;
            return _hospitalizationRepository.Remove(id);
        }

        public Hospitalization? GetHospitalizationById(int id)
        {
            return _hospitalizationRepository.GetById(id);
        }

        public IEnumerable<Hospitalization> GetHospitalizations()
        {
            return _hospitalizationRepository.GetAll();
        }
    }
}

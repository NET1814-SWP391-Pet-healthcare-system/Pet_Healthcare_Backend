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
        public bool AddHospitalization(HospitalizationAddRequest? request)
        {
            if (request == null)
            {
                return false;
            }
            var hospitalization = request.ToHospitalization();
            return _hospitalizationRepository.Add(hospitalization);
        }

        public bool UpdateHospitalization(HospitalizationUpdateRequest? request)
        {
            if (_hospitalizationRepository.GetById(request.HospitalizationId) == null)
            {
                return false;
            }
            return _hospitalizationRepository.Update(request.ToHospitalization());
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

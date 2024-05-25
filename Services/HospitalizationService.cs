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
            _hospitalizationRepository.Add(hospitalization);
            return true;
        }

        public bool UpdateHospitalization(HospitalizationUpdateRequest? request)
        {
            if (request == null)
            {
                return false;
            }
            var hospitalization = request.ToHospitalization();
            _hospitalizationRepository.Update(hospitalization);
            return true;
        }
        //huh this needs to be changed
        public bool RemoveHospitalization(int id)
        {
            if (id == null)
            {
                return false;
            }
            var hospitalization = id;
            _hospitalizationRepository.Remove(id);
            return true;
        }

        public Hospitalization? GetHospitalizationById(int id)
        {
            return _hospitalizationRepository.GetById(id);
        }

        public IEnumerable<Hospitalization> GetHospitalization()
        {
            return _hospitalizationRepository.GetAll();
        }
    }
}

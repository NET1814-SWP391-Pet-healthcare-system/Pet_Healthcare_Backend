using RepositoryContracts;
using ServiceContracts.DTO;
using ServiceContracts.DTO.HospitalizationDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;

namespace Services
{
    public class HospitalizationService
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
        public bool RemoveHospitalization(HospitalizationRemoveRequest? request)
        {
            if (request == null)
            {
                return false;
            }
            var hospitalization = request.ToHospitalization();
            _hospitalizationRepository.Remove(hospitalization.HospitalizationId);
            return true;
        }
    }
}

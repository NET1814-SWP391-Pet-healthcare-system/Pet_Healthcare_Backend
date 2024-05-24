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
using ServiceContracts.DTO.UserDTO;

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

        public bool AddHospitalization(UserAddRequest request)
        {
            throw new NotImplementedException();
        }

        public User? GetHospitalizationById(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<User> GetHospitalization()
        {
            throw new NotImplementedException();
        }

        public bool UpdateHospitalization(UserUpdateRequest request)
        {
            throw new NotImplementedException();
        }

        public bool RemoveHospitalization(int id)
        {
            throw new NotImplementedException();
        }
    }
}

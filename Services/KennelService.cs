using Entities;
using RepositoryContracts;
using ServiceContracts;
using ServiceContracts.DTO.KennelDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class KennelService : IKennelService
    {
        private readonly IKennelRepository _kennelRepository;
        public KennelService(IKennelRepository kennelRepository)
        {
            _kennelRepository = kennelRepository;
        }
        public bool AddKennel(KennelAddRequest? request)
        {
            if (request == null) return false;
            var kennel = request.ToKennel();
            return _kennelRepository.Add(kennel);
        }

        public Kennel? GetKennelById(int id)
        {
            return _kennelRepository.GetById(id);
        }

        public IEnumerable<Kennel> GetKennels()
        {
            return _kennelRepository.GetAll();
        }

        public bool RemoveKennel(int id)
        {
            return _kennelRepository.Remove(id);
        }

        public bool UpdateKennel(int id, KennelUpdateRequest? request)
        {
            var existingKennel = _kennelRepository.GetById(id);
            if (existingKennel == null) return false;
            if (request == null) return false;
            var kennelModel = request.ToKennel();
            existingKennel.Capacity = kennelModel.Capacity;
            existingKennel.Description = kennelModel.Description;
            existingKennel.DailyCost = kennelModel.DailyCost;
            return _kennelRepository.Update(existingKennel);
        }
    }
}

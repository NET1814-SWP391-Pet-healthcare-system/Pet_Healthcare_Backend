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

        public async Task<Kennel> AddKennelAsync(Kennel kennelModel)
        {
            return await _kennelRepository.AddAsync(kennelModel);
        }

        public async Task<Kennel?> GetKennelByIdAsync(int id)
        {
            return await _kennelRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Kennel>> GetKennelsAsync()
        {
            return await _kennelRepository.GetAllAsync();
        }

        public async Task<Kennel?> RemoveKennelAsync(int id)
        {
            return await _kennelRepository.RemoveAsync(id);
        }

        public async Task<Kennel?> UpdateKennelAsync(int id, Kennel kennelModel)
        {
            var existingKennel = await _kennelRepository.GetByIdAsync(id);
            if (existingKennel == null)
            {
                return null;
            }
            existingKennel.Description = kennelModel.Description;
            existingKennel.Capacity = kennelModel.Capacity;
            existingKennel.DailyCost = kennelModel.DailyCost;
            return await _kennelRepository.UpdateAsync(existingKennel);
        }
    }
}

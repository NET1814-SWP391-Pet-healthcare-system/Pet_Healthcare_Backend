using Entities;
using Repositories;
using RepositoryContracts;
using ServiceContracts;
using ServiceContracts.DTO.KennelDTO;
using ServiceContracts.DTO.UserDTO;
using ServiceContracts.Mappers;
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
        private readonly IHospitalizationService _hospitalizationService;
        public KennelService(IKennelRepository kennelRepository, IHospitalizationService hospitalizationService)
        {
            _kennelRepository = kennelRepository;
            _hospitalizationService = hospitalizationService;
        }

        public async Task<Kennel?> AddKennelAsync(Kennel kennelModel)
        {
            if (kennelModel == null)
            {
                return null;
            }
            await _kennelRepository.AddAsync(kennelModel);

            return kennelModel;
        }

        public async Task<KennelDto?> GetKennelByIdAsync(int id)
        {
            var existingKennel = await _kennelRepository.GetByIdAsync(id);
            if(existingKennel == null)
            {
                return null;
            }
            var result = existingKennel.ToKennelDto();
            var hospitalizations = await _hospitalizationService.GetHospitalizations();
            var hospitalizedKennelIds = hospitalizations.Where(h => h.KennelId.HasValue)
                                                        .Select(h => h.KennelId.Value)
                                                        .ToList();
            if(hospitalizedKennelIds.Contains(result.KennelId))
            {
                result.IsAvailable = false;
                return result;
            }
            result.IsAvailable = true;
            return result;
        }

        public async Task<IEnumerable<KennelDto>> GetKennelsAsync()
        {
            var hospitalizations = await _hospitalizationService.GetHospitalizations();
            var hospitalizedKennelIds = hospitalizations.Where(h => h.KennelId.HasValue)
                                                        .Select(h => h.KennelId.Value)
                                                        .Distinct();

            var kennels = await _kennelRepository.GetAllAsync();
            var kennelDtos = kennels.Select(k => new KennelDto
            {
                KennelId = k.KennelId,
                Description = k.Description,
                Capacity = (int) k.Capacity,
                DailyCost =(double) k.DailyCost,
                IsAvailable = !hospitalizedKennelIds.Contains(k.KennelId)
            });
            return kennelDtos;
        }

        public async Task<bool> RemoveKennelAsync(int id)
        {
            var kennel = await _kennelRepository.GetByIdAsync(id);
            if (kennel == null)
            {
                return false;
            }
            return await _kennelRepository.RemoveAsync(kennel);
        }

        public async Task<Kennel?> UpdateKennelAsync(Kennel kennelModel)
        {
            if (kennelModel == null)
            {
                return null;
            }
            var existingKennel = await _kennelRepository.GetByIdAsync(kennelModel.KennelId);
            //existingKennel.KennelId = existingKennel.KennelId;
            //existingKennel.Capacity = existingKennel.Capacity;
            existingKennel.Description = (string.IsNullOrEmpty(kennelModel.Description)) ? existingKennel.Description : kennelModel.Description;
            existingKennel.DailyCost = (kennelModel.DailyCost == null) ? existingKennel.DailyCost : kennelModel.DailyCost;
            await _kennelRepository.UpdateAsync(existingKennel);

            return existingKennel;
        }
    }
}

using Entities;
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

        public async Task<Kennel> AddKennelAsync(Kennel kennelModel)
        {
            return await _kennelRepository.AddAsync(kennelModel);
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
                Capacity = k.Capacity,
                DailyCost = k.DailyCost,
                IsAvailable = !hospitalizedKennelIds.Contains(k.KennelId)
            });
            return kennelDtos;
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
            kennelModel.Description = (kennelModel.Description == "") ? existingKennel.Description : kennelModel.Description;
            kennelModel.DailyCost = (kennelModel.DailyCost == 0) ? existingKennel.DailyCost : kennelModel.DailyCost;
            return await _kennelRepository.UpdateAsync(id, kennelModel);
        }
    }
}

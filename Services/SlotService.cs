using Entities;
using RepositoryContracts;
using ServiceContracts;
using ServiceContracts.DTO.SlotDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class SlotService : ISlotService
    {
        private readonly ISlotRepository _slotRepository;
        public SlotService(ISlotRepository slotRepository)
        {
            _slotRepository = slotRepository;
        }

        public async Task<Slot> AddSlotAsync(Slot slotModel)
        {
            return await _slotRepository.AddAsync(slotModel);
        }

        public async Task<Slot?> GetSlotByIdAsync(int id)
        {
            return await _slotRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Slot>> GetSlotsAsync()
        {
            return await _slotRepository.GetAllAsync();
        }

        public async Task<Slot?> RemoveSlotAsync(int id)
        {
            return await _slotRepository.RemoveAsync(id);
        }

        public async Task<Slot?> UpdateSlotAsync(int id, Slot slotModel)
        {
            var existingSlot = await _slotRepository.GetByIdAsync(id);
            if (existingSlot == null)
            {
                return null;
            }
            existingSlot.StartTime = slotModel.StartTime;
            existingSlot.EndTime = slotModel.EndTime;   
            return await _slotRepository.UpdateAsync(existingSlot);
        }
    }
}

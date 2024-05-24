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
        public bool AddSlot(SlotAddRequest? request)
        {
            if (request == null) return false;
            var slot = request.ToSlot();
            return _slotRepository.Add(slot);
        }

        public Slot? GetSlotById(int id)
        {
            var slot = _slotRepository.GetById(id);
            if (slot == null) return null;
            return slot;
        }

        public IEnumerable<Slot> GetSlots()
        {
            return _slotRepository.GetAll();
        }

        public bool RemoveSlot(int id)
        {
            return _slotRepository.Remove(id);
        }

        public bool UpdateSlot(int id, SlotUpdateRequest? request)
        {
            var exisingSlot = _slotRepository.GetById(id);
            if (exisingSlot == null) return false;
            if (request == null) return false;
            var slotModel = request.ToSlot();
            exisingSlot.StartTime = slotModel.StartTime;
            exisingSlot.EndTime = slotModel.EndTime;
            return _slotRepository.Update(exisingSlot);
        }
    }
}

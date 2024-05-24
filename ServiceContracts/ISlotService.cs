using Entities;
using ServiceContracts.DTO.SlotDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts
{
    public interface ISlotService
    {
        bool AddSlot(SlotAddRequest? request);
        Slot? GetSlotById(int id);
        IEnumerable<Slot> GetSlots();
        bool UpdateSlot(int id, SlotUpdateRequest? request);
        bool RemoveSlot(int id);
    }
}

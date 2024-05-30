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
        Task<Slot> AddSlotAsync(Slot slotModel);
        Task<Slot?> GetSlotByIdAsync(int id);
        Task<IEnumerable<Slot>> GetSlotsAsync();
        Task<Slot?> UpdateSlotAsync(int id, Slot slotModel);
        Task<Slot?> RemoveSlotAsync(int id);
    }
}

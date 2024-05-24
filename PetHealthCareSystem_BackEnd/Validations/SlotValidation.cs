using Entities;
using Microsoft.AspNetCore.Mvc;

namespace PetHealthCareSystem_BackEnd.Validations
{
    public static class SlotValidation
    {
        public static bool IsOverlapping(Slot requestSlot, IEnumerable<Slot> slots)
        {
            foreach (var existingSlot in slots)
            {
                if ((requestSlot.StartTime >= existingSlot.StartTime 
                    && requestSlot.StartTime < existingSlot.EndTime) || 
                    (requestSlot.EndTime > existingSlot.StartTime 
                    && requestSlot.EndTime <= existingSlot.EndTime))
                {
                    return true;
                }
            }
            return false;
        }
    }
}

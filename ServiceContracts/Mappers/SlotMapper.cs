using Entities;
using ServiceContracts.DTO.SlotDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts.Mappers
{
    public static class SlotMapper
    {
        public static SlotDto ToSlotDto(this Slot slotModel)
        {
            return new SlotDto()
            {
                SlotId = slotModel.SlotId,
                StartTime = slotModel.StartTime,
                EndTime = slotModel.EndTime
            };
        }

        public static Slot ToSlotFromAdd(this SlotAddRequest slotAddRequest)
        {
            return new Slot()
            {
                StartTime = TimeOnly.FromDateTime(slotAddRequest.StartTime),
                EndTime = TimeOnly.FromDateTime(slotAddRequest.EndTime)
            };
        }

        public static Slot ToSlotFromUpdate(this SlotUpdateRequest slotUpdateRequest)
        {
            return new Slot()
            {
                StartTime = TimeOnly.FromDateTime(slotUpdateRequest.StartTime),
                EndTime = TimeOnly.FromDateTime(slotUpdateRequest.EndTime)
            };
        }
    }
}

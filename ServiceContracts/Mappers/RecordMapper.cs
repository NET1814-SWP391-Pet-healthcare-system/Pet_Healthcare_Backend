using Entities;
using ServiceContracts.DTO.RecordDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts.Mappers
{
    public static class RecordMapper
    {
        public static RecordDto ToRecordDto(this Record request)
        {
            return new RecordDto
            {
                RecordId = request.RecordId,
                PetName = request.Pet.Name,
                NumberOfVisits = request.NumberOfVisits
            };
        }
        public static Record ToRecordFromAdd(this RecordAddRequest request)
        {
            return new Record
            {
                PetId = request.PetId,
                NumberOfVisits = request.NumberOfVisits
            };
        }
        public static Record ToRecordFromUpdate(this RecordUpdateRequest request)
        {
            return new Record
            {
                RecordId = request.RecordId,
                PetId = request.PetId,
                NumberOfVisits = request.NumberOfVisits
            };
        }
    }
}

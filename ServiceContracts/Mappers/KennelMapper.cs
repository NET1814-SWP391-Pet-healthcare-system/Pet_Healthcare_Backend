using Entities;
using ServiceContracts.DTO.KennelDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts.Mappers
{
    public static class KennelMapper
    {
        public static KennelDto ToKennelDto(this Kennel kennelModel)
        {
            return new KennelDto()
            {
                KennelId = kennelModel.KennelId,
                Description = kennelModel.Description,
                Capacity = kennelModel.Capacity,
                DailyCost = kennelModel.DailyCost
            };
        }

        public static Kennel ToKennelFromAdd(this KennelAddRequest kennelAddRequest)
        {
            return new Kennel()
            {
                Description = kennelAddRequest.Description,
                Capacity = kennelAddRequest.Capacity,
                DailyCost = kennelAddRequest.DailyCost
            };
        }

        public static Kennel ToKennelFromUpdate(this KennelUpdateRequest kennelUpdateRequest)
        {
            return new Kennel()
            {
                Description = kennelUpdateRequest.Description,
                DailyCost = kennelUpdateRequest.DailyCost
            };
        }
    }
}

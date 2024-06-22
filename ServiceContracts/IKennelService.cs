using Entities;
using ServiceContracts.DTO.KennelDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts
{
    public interface IKennelService
    {
        Task<Kennel?> AddKennelAsync(Kennel kennelModel);
        Task<KennelDto?> GetKennelByIdAsync(int id);
        Task<IEnumerable<KennelDto>> GetKennelsAsync();
        Task<Kennel?> UpdateKennelAsync(Kennel kennelModel);
        Task<bool> RemoveKennelAsync(int id);
    }
}

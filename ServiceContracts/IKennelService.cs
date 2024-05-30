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
        Task<Kennel> AddKennelAsync(Kennel kennelModel);
        Task<Kennel?> GetKennelByIdAsync(int id);
        Task<IEnumerable<Kennel>> GetKennelsAsync();
        Task<Kennel?> UpdateKennelAsync(int id, Kennel kennelModel);
        Task<Kennel?> RemoveKennelAsync(int id);
    }
}

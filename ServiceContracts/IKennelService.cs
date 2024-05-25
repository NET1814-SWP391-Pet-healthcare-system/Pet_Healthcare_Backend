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
        bool AddKennel(KennelAddRequest? request);
        Kennel? GetKennelById(int id);
        IEnumerable<Kennel> GetKennels();
        bool UpdateKennel(int id, KennelUpdateRequest? request);
        bool RemoveKennel(int id);
    }
}

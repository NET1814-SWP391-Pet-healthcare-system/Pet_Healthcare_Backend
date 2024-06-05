using Entities;
using ServiceContracts.DTO.VaccineDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts
{
    public interface IVaccineService
    {
        Task<VaccineDTO?> AddVaccine(VaccineAddRequest vaccineAddRequest);
        Task<VaccineDTO?> GetVaccineById(int id);
        Task<IEnumerable<VaccineDTO>> GetAllVaccines();
        Task<VaccineDTO> UpdateVaccine(int id, VaccineUpdateRequest vaccineUpdateRequest);
        Task<bool> RemoveVaccine(int id);
    }
}

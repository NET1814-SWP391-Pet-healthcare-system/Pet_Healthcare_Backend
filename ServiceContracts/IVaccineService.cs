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
        Task<Vaccine?> AddVaccine(VaccineAddRequest vaccineAddRequest);
        Task<Vaccine?> GetVaccineById(int id);
        Task<IEnumerable<Vaccine>> GetAllVaccines();
        Task<Vaccine> UpdateVaccine(int id, VaccineUpdateRequest vaccineUpdateRequest);
        Task<bool> RemoveVaccine(int id);
    }
}

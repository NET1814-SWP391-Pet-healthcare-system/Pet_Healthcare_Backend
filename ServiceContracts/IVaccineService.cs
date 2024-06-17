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
        Task<Vaccine?> AddVaccine(Vaccine vaccine);
        Task<Vaccine?> GetVaccineById(int id);
        Task<IEnumerable<Vaccine>> GetAllVaccines();
        Task<Vaccine> UpdateVaccine(Vaccine vaccine);
        Task<bool> RemoveVaccineById(int id);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;

namespace RepositoryContracts
{
    public interface IVaccineRepository
    {
        Task<IEnumerable<Vaccine>> GetAllAsync();
        Task<Vaccine?> GetByIdAsync(int id);
        Task<bool> AddAsync(Vaccine vaccine);
        Task<bool> UpdateAsync(Vaccine vaccine);
        Task<bool> RemoveAsync(Vaccine vaccine);
        Task<bool> SaveChangesAsync();
    }
}

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
        Task<Vaccine?> Update(int id, Vaccine vaccine);
        Task<bool> Remove(Vaccine vaccine);
        Task<bool> SaveChangesAsync();
    }
}

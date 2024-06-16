using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryContracts
{
    public interface IPetVaccinationRepository
    {
        Task<IEnumerable<PetVaccination>> GetAllAsync();
        Task<PetVaccination?> GetByIdAsync(int petId, int vaccineId);
        Task<bool> AddAsync(PetVaccination petVaccination);
        Task<bool> UpdateAsync(PetVaccination petVaccination);
        Task<bool> RemoveAsync(PetVaccination petVaccination);
        Task<bool> SaveChangesAsync();
    }
}

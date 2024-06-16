using Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryContracts
{
    public interface IHospitalizationRepository
    {
        Task<bool> Add(Hospitalization hospitalization);
        Task<Hospitalization?> GetById(int id);
        Task<IEnumerable<Hospitalization>> GetAll();
        Task<bool> Update(Hospitalization hospitalization);
        Task<bool> Remove(Hospitalization hospitalization);
        Task<Hospitalization?> GetByPetId(int id);
        Task<Hospitalization?> GetByVetId(string id);
        Task<IEnumerable<Hospitalization>> GetAllByVetId(string id);
        Task<IEnumerable<Hospitalization>> GetAllByPetId(int id);
        Task<bool> IsVetDateConflict(string id, DateOnly AddmissionDate, DateOnly DischargeDate);
        Task<bool> SaveChangesAsync();

    }
}

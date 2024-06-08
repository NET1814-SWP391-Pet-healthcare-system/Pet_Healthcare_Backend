using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryContracts
{
    public interface IHospitalizationRepository
    {
        Task<Hospitalization> Add(Hospitalization hospitalization);
        Task<Hospitalization?> GetById(int id);
        Task<IEnumerable<Hospitalization>> GetAll();
        Task<Hospitalization?> Update(int id, Hospitalization hospitalization);
        Task<Hospitalization?> Remove(int id);
        Task<Hospitalization?> GetByPetId(int id);
        Task<Hospitalization?> GetByVetId(string id);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;

namespace RepositoryContracts
{
    public interface IServiceRepository
    {
        Task<IEnumerable<Service>> GetAll();
        Task<Service?> GetById(int id);
        Task<bool> Add(Service service);
        Task<bool> Update(Service service);
        Task<bool> Remove(int id);
        Task<bool> SaveChangesAsync();
        Task<Service?> GetByName(string name);
    }
}

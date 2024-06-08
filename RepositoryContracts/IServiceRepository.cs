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
        Task<Service> Add(Service service);
        Task<Service?> Update(int id,Service service);
        Task<Service?> Remove(int id);
        Task<Service?> GetByName(string name);
    }
}

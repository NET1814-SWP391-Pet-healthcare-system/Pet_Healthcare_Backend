using PetHealthCareSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryContracts
{
    public interface IServiceRepository
    {
        IEnumerable<Service> GetAll();
        Service GetById(int id);
        bool Add(Service service);
        bool Update(Service service);
        bool Remove(int id);
        bool SaveChanges();
    }
}

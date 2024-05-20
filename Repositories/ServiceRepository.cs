using RepositoryContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;

namespace Repositories
{
    public class ServiceRepository : IServiceRepository
    {
        public bool Add(Service service)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Service> GetAll()
        {
            throw new NotImplementedException();
        }

        public Service GetById(int id)
        {
            throw new NotImplementedException();
        }

        public bool Remove(int id)
        {
            throw new NotImplementedException();
        }

        public bool SaveChanges()
        {
            throw new NotImplementedException();
        }

        public bool Update(Service service)
        {
            throw new NotImplementedException();
        }
    }
}

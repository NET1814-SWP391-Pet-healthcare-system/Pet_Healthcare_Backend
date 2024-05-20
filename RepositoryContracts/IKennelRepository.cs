using System;
using Entities;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryContracts
{
    public interface IKennelRepository
    {
        IEnumerable<Kennel> GetAll();
        Kennel GetById(int id);
        bool Add(Kennel kennel);
        bool Update(Kennel kennel);
        bool Remove(int id);
        bool SaveChanges();
    }
}

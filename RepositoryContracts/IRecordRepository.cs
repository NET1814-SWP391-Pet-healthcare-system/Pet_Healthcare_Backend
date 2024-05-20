using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryContracts
{
    public interface IRecordRepository
    {
        IEnumerable<Record> GetAll();
        Record GetById(int id);
        bool Add(Record record);
        bool Update(Record record);
        bool Remove(int id);
        bool SaveChanges();
    }
}

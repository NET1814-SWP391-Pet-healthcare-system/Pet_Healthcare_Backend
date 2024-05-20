using Entities;
using RepositoryContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class RecordRepository : IRecordRepository
    {
        public bool Add(Record record)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Record> GetAll()
        {
            throw new NotImplementedException();
        }

        public Record GetById(int id)
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

        public bool Update(Record record)
        {
            throw new NotImplementedException();
        }
    }
}

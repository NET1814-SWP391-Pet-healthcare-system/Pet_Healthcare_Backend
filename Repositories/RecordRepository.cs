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
        private readonly ApplicationDbContext _context;

        public RecordRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool Add(Record record)
        {
            if(record == null) return false;

            _context.Records.Add(record);
            return true;
        }

        public IEnumerable<Record> GetAll()
        {
            return _context.Records.ToList();
        }

        public Record? GetById(int id)
        {
            
            return _context.Records.FirstOrDefault(x => x.RecordId == id);
        }

        public bool Remove(int id)
        {
            if(GetById(id) == null) return false;

            _context.Records.Remove(GetById(id));
            return true;
        }

        public bool SaveChanges()
        {
            if(_context.SaveChanges() > 0) return true;
            return false;
        }

        public bool Update(Record record)
        {
            if(record == null) return false;
            _context.Update(record);
            return true;
        }
    }
}

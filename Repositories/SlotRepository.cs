using RepositoryContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;
using Entities.Enum;

namespace Repositories
{
    public class SlotRepository : ISlotRepository
    {
        private readonly ApplicationDbContext _context;
        public SlotRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public bool Add(Slot slot)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Slot> GetAll()
        {
            throw new NotImplementedException();
        }

        public Slot? GetById(int id)
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

        public bool Update(Slot slot)
        {
            throw new NotImplementedException();
        }
    }
}

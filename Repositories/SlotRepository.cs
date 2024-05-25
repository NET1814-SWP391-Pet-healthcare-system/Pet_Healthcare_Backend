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
        public bool Add(Slot? slot)
        {
            if (slot == null) return false;
            _context.Slots.Add(slot);
            return SaveChanges();
        }

        public IEnumerable<Slot> GetAll()
        {
            return _context.Slots.ToList(); 
        }

        public Slot? GetById(int id)
        {
            return _context.Slots.Find(id);
        }

        public bool Remove(int id)
        {
            var slot = _context.Slots.Find(id);
            if (slot == null) return false;
            _context.Slots.Remove(slot);
            return SaveChanges();
        }

        public bool SaveChanges()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool Update(Slot? slot)
        {
            if (slot == null) return false;
            _context.Slots.Update(slot);
            return SaveChanges();
        }
    }
}

using RepositoryContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;
using Entities.Enum;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace Repositories
{
    public class SlotRepository : ISlotRepository
    {
        private readonly ApplicationDbContext _context;
        public SlotRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Slot> AddAsync(Slot slotModel)
        {
            await _context.Slots.AddAsync(slotModel);
            await _context.SaveChangesAsync();
            return slotModel;
        }

        public async Task<IEnumerable<Slot>> GetAllAsync()
        {
            return await _context.Slots.ToListAsync();
        }

        public async Task<Slot?> GetByIdAsync(int id)
        {
            return await _context.Slots.FindAsync(id);
        }

        public async Task<Slot?> RemoveAsync(int id)
        {
            var slotModel = await _context.Slots.FindAsync(id);
            if (slotModel == null) 
            {
                return null;
            }
            _context.Slots.Remove(slotModel);
            await _context.SaveChangesAsync();
            return slotModel;
        }

        public async Task<Slot?> UpdateAsync(Slot slotModel)
        {
            await _context.SaveChangesAsync();
            return slotModel;
        }
    }
}

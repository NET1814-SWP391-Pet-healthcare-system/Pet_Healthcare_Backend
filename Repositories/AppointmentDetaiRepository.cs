using Entities;
using Microsoft.EntityFrameworkCore;
using RepositoryContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class AppointmentDetaiRepository : IAppointmentDetailRepository
    {
        private readonly ApplicationDbContext _context;

        public AppointmentDetaiRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<bool> AddAsync(AppointmentDetail appointmentDetail)
        {
            await _context.AppointmentDetails.AddAsync(appointmentDetail);
            return await SaveChangesAsync();
        }

        public async Task<IEnumerable<AppointmentDetail>> GetAllAsync()
        {
            return await _context.AppointmentDetails
                .Include(a => a.Appointment)
                .Include(a => a.Record)
                .ToListAsync();

        }

        public async Task<AppointmentDetail?> GetByIdAsync(int id)
        {
            return await _context.AppointmentDetails
                .Include (a => a.Appointment)
                .Include(a => a.Record)
                .FirstOrDefaultAsync(a => a.AppointmentId == id);
                
        }

        public async Task<bool>? RemoveAsync(AppointmentDetail appointmentDetail )
        {
            _context.AppointmentDetails.Remove(appointmentDetail);
            return await SaveChangesAsync();
        }

        public async Task<bool> UpdateAsync(AppointmentDetail appointmentDetail)
        {

            _context.Entry(appointmentDetail).State = EntityState.Modified;
            return await SaveChangesAsync();
        }
        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}

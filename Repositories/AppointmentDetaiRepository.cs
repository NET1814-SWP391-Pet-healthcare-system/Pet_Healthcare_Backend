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
        public async Task<AppointmentDetail> AddAsync(AppointmentDetail appointmentDetail)
        {
            await _context.AppointmentDetails.AddAsync(appointmentDetail);
            await _context.SaveChangesAsync();
            return appointmentDetail;
        }

        public async Task<IEnumerable<AppointmentDetail>> GetAllAsync()
        {
            return await _context.AppointmentDetails
                .Include(a => a.Appointment)
                .Include(r => r.Record)
                .ToListAsync();

        }

        public async Task<AppointmentDetail>? GetByIdAsync(int id)
        {
            return _context.AppointmentDetails.FirstOrDefault(x => x.AppointmentDetailId == id);
        }

        public async Task<AppointmentDetail>? RemoveAsync(int id)
        {
            var appointmentDetail = await GetByIdAsync(id);
            if (appointmentDetail ==null)
            {
                return null;
            }
            _context.Remove(appointmentDetail);
            await _context.SaveChangesAsync();
            return appointmentDetail;
        }

        public async Task<AppointmentDetail> UpdateAsync(AppointmentDetail appointmentDetail)
        {
            
            await _context.SaveChangesAsync();
            return appointmentDetail;
        }
    }
}

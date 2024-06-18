using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryContracts
{
    public interface IAppointmentDetailRepository
    {
        Task<IEnumerable<AppointmentDetail>> GetAllAsync();
        Task<AppointmentDetail>? GetByIdAsync(int id);
        Task<bool> AddAsync(AppointmentDetail appointmentDetail);
        Task<bool> UpdateAsync(AppointmentDetail appointmentDetail);
        Task<bool> RemoveAsync(AppointmentDetail appointmentDetail);
        Task<bool> SaveChangesAsync();
    }
}


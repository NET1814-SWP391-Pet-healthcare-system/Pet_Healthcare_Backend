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
        Task<AppointmentDetail>? AddAsync(AppointmentDetail appointmentDetail);
        Task<AppointmentDetail>? UpdateAsync(AppointmentDetail appointmentDetail);
        Task<AppointmentDetail>? RemoveAsync(int id);
    }
}


using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryContracts
{
    public interface IAppointmentRepository
    {
        Task<IEnumerable<Appointment>> GetAllAsync();
        Task<IEnumerable<Appointment>> GetByDateAndSlotAsync(DateOnly date, int slotId);
        Task<IEnumerable<Appointment>> GetByUserIdAsync(string userId);
        Task<Appointment?> GetByIdAsync(int id);
        Task<Appointment> AddAsync(Appointment appointmentModel);
        Task<Appointment?> UpdateAsync(Appointment appointmentModel);
        Task<Appointment?> RemoveAsync(int id);
    }
}

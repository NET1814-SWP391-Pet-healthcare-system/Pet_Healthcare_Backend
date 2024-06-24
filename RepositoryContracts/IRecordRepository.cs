using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryContracts
{
    public interface IRecordRepository
    {
        Task<IEnumerable<Record>?> GetAllAsync();
        Task<IEnumerable<AppointmentDetail>> GetAllAppointmentDetailAsync();
        Task<Record?> GetByIdAsync(int id);
        Task<bool> AddAsync(Record record);
        Task<bool> UpdateAsync(Record record);
        Task<bool> RemoveAsync(Record record);
        Task<bool> SaveChangesAsync();
    }
}

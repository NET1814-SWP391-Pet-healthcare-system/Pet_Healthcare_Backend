using Entities;
using ServiceContracts.DTO;
using ServiceContracts.DTO.AppointmentDetailDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts
{
    public interface IAppointmentDetailService
    {
        Task<AppointmentDetail?> AddAppointmentDetailAsync(AppointmentDetail request);
        Task<AppointmentDetail?> UpdateAppointmentDetailAsync(AppointmentDetail request);
        Task<IEnumerable<AppointmentDetail>> GetAppointmentDetailsAsync();
        Task<bool> RemoveAppointmentDetailAsync(int id);
        Task<AppointmentDetail?> GetAppointmentDetailByIdAsync(int id);

    }
}

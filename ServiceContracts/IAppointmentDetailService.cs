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
        Task<AppointmentDetail>? AddAppointmentDetail(AppointmentDetail request);
        Task<AppointmentDetail>? UpdateAppointmentDetail(int i ,AppointmentDetail request);
        Task<IEnumerable<AppointmentDetail>> GetAppointmentDetails();
        Task<AppointmentDetail>? RemoveAppointmentDetail(int id);
        Task<AppointmentDetail>? GetAppointmentDetailById(int id);

    }
}

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
        bool AddAppointmentDetail(AppointmentDetailAddRequest request);
        bool UpdateAppointmentDetail(int i ,AppointmentDetailAddRequest request);
        IEnumerable<AppointmentDetail> GetAppointmentDetails();
        bool RemoveAppointmentDetail(int id);
        AppointmentDetail? GetAppointmentDetail(int id);

    }
}

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
        IEnumerable<AppointmentDetail> GetAll();
        AppointmentDetail GetById(int id);
        bool Add(AppointmentDetail appointmentDetail);
        bool Update(AppointmentDetail appointmentDetail);
        bool Remove(int id);
        bool SaveChanges();
    }
}


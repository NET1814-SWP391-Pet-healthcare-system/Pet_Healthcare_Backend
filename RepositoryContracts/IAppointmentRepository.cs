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
        IEnumerable<Appointment> GetAll();
        Appointment GetById(int id);
        bool Add(Appointment appointment);
        bool Update(Appointment appointment);
        bool Remove(int id);
        bool SaveChanges();
    }
}

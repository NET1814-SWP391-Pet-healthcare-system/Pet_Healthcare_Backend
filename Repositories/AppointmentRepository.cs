using RepositoryContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;

namespace Repositories
{
    public class AppointmentRepository : IAppointmentRepository
    {
        public bool Add(Appointment appointment)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Appointment> GetAll()
        {
            throw new NotImplementedException();
        }

        public Appointment GetById(int id)
        {
            throw new NotImplementedException();
        }

        public bool Remove(int id)
        {
            throw new NotImplementedException();
        }

        public bool SaveChanges()
        {
            throw new NotImplementedException();
        }

        public bool Update(Appointment appointment)
        {
            throw new NotImplementedException();
        }
    }
}

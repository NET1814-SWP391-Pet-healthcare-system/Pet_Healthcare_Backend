using Entities;
using RepositoryContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class AppointmentDetaiRepository : IAppointmentDetailRepository
    {
        private readonly ApplicationDbContext _context;

        public AppointmentDetaiRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public bool Add(AppointmentDetail appointmentDetail)
        {
            if(appointmentDetail == null) return false;

            _context.AppointmentDetails.Add(appointmentDetail);
            return true;
        }

        public IEnumerable<AppointmentDetail> GetAll()
        {
            return _context.AppointmentDetails.ToList();
        }

        public AppointmentDetail? GetById(int id)
        {
            return _context.AppointmentDetails.FirstOrDefault(x => x.AppointmentDetailId == id);
        }

        public bool Remove(int id)
        {
            if(GetById(id) == null) return false;

            _context.AppointmentDetails.Remove(GetById(id));
            return true;
        }

        public bool SaveChanges()
        {
           if(_context.SaveChanges() > 0) return true;

           return false;
        }

        public bool Update(AppointmentDetail appointmentDetail)
        {
            if(appointmentDetail == null) return false;

            _context.AppointmentDetails.Update(appointmentDetail);
            return true;
        }
    }
}

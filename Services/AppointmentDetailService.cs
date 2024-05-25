using Entities;
using RepositoryContracts;
using ServiceContracts;
using ServiceContracts.DTO.AppointmentDetailDTO;

namespace Services
{
    public class AppointmentDetailService : IAppointmentDetailService
    {
        private readonly IAppointmentDetailRepository _appointmentDetailRepository;

        public AppointmentDetailService(IAppointmentDetailRepository appointmentDetailRepository)
        {
            _appointmentDetailRepository = appointmentDetailRepository;
        }

        public bool AddAppointmentDetail(AppointmentDetailAddRequest? request)
        {
            if (request == null)
            {
                return false;
            }
            var user = request.ToAppointmentDetail();
            _appointmentDetailRepository.Add(user);
            return true;
        }

 

        public AppointmentDetail? GetAppointmentDetail(int id)
        {
            return _appointmentDetailRepository.GetById(id);
        }

        public IEnumerable<AppointmentDetail> GetAppointmentDetails()
        {
            return _appointmentDetailRepository.GetAll();
        }

        public bool RemoveAppointmentDetail(int id)
        {
            if(_appointmentDetailRepository.GetById(id) == null)
            {
                return false;
            }
            _appointmentDetailRepository.Remove(id);
            return true;
        }

        public bool UpdateAppointmentDetail(int i ,AppointmentDetailAddRequest request)
        {
            var ExistingAppointmentDetail = _appointmentDetailRepository.GetById(i);
            if(request == null || request.ToAppointmentDetail == null || ExistingAppointmentDetail==null)
            {
                return false;
            }
            var AppointmentDetail = request.ToAppointmentDetail();
            ExistingAppointmentDetail.Appointment = AppointmentDetail.Appointment;
            ExistingAppointmentDetail.Treatment = AppointmentDetail.Treatment;
            ExistingAppointmentDetail.Diagnosis = AppointmentDetail.Diagnosis;
            _appointmentDetailRepository.Update(ExistingAppointmentDetail);
            return true;
        }

 
    }
}

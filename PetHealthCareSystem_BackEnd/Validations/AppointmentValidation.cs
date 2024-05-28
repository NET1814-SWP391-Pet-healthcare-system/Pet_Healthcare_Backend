using Entities;

namespace PetHealthCareSystem_BackEnd.Validations
{
    public static class AppointmentValidation
    {
        public static bool IsDuplicateBooking(Appointment requestAppointment, IEnumerable<Appointment> existingAppointmentsInDate)
        {
            foreach (var existingAppointment in existingAppointmentsInDate)
            {
                if (requestAppointment.VetId == existingAppointment.VetId
                    && requestAppointment.SlotId == existingAppointment.SlotId)
                {
                    return true;
                }
            }
            return false;
        }
    }
}

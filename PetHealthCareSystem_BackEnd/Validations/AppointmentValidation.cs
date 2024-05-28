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

        public static bool IsUserPet(Customer customer, int petId)
        {
            foreach (var pet in customer.Pets)
            {
                if (petId == pet.PetId)
                {
                    return true;
                }
            }
            return false;
        }
    }
}

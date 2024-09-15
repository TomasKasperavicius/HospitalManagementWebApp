using HospitalManagementWebApp.Models;

namespace HospitalManagementWebApp.Services.Interfaces
{
    public interface IUserService
    {
        List<DoctorListViewModel> GetDoctors();
        ReserveAppointmentModel ReserveAppointment(ReserveAppointmentModel reserveAppointmentModel);
        List<AppointmentViewModel> GetPatientAppointments(int patientID);
        List<Appointment> GetDoctorAppointments(int? doctorID, DateTime date);
        int? CancelAppointment(int appointmentID);
    }
}

using HospitalManagementWebApp.Models;

namespace HospitalManagementWebApp.Services.Interfaces
{
    public interface IPatientService
    {
        ReserveAppointmentModel? ReserveAppointment(ReserveAppointmentModel reserveAppointmentModel);
        List<AppointmentViewModel> GetPatientAppointments(int patientID);
        int? CancelAppointment(int appointmentID);
        List<MedicalHistory> GetPatientMedicalHistory(int patientID);
        Patient? GetPatient(int patientID);
    }
}

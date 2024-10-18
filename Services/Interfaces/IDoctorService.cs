using HospitalManagementWebApp.Models;

namespace HospitalManagementWebApp.Services.Interfaces
{
    public interface IDoctorService
    {
        List<DoctorListViewModel> GetDoctors();
        List<Appointment> GetDoctorAppointments(int? doctorID, DateTime date);
        DoctorListViewModel? GetDoctor(int id);
    }
}

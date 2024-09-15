using HospitalManagementWebApp.Models;

namespace HospitalManagementWebApp.Repositories.Interfaces
{
    public interface IAppointmentRepository : IGenericRepository<Appointment>
    {
        IEnumerable<Appointment> AddRange(IEnumerable<Appointment> appointments);
        IEnumerable<Appointment> GetPatientAppointments(int patientId);
    }
}

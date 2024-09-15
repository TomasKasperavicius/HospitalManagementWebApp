using HospitalManagementWebApp.Models;
using HospitalManagementWebApp.Repositories.Interfaces;

namespace HospitalManagementWebApp.Repositories
{
    public class AppointmentRepository : GenericRepository<Appointment>, IAppointmentRepository
    {
        public AppointmentRepository(HospitalManagerDbContext context) : base(context) { }
        public IEnumerable<Appointment> AddRange(IEnumerable<Appointment> appointments)
        {
            _context.Set<Appointment>().AddRange(appointments);
            _context.SaveChanges();
            return appointments;
        }
        public IEnumerable<Appointment> GetPatientAppointments(int patientId)
        {
            return _context.Set<Appointment>().Where(a => a.UserID == patientId).ToList();
        }
    }

}

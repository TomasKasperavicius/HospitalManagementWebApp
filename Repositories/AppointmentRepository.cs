using HospitalManagementWebApp.Models;
using HospitalManagementWebApp.Repositories.Interfaces;

namespace HospitalManagementWebApp.Repositories
{
    public class AppointmentRepository : GenericRepository<Appointment>, IAppointmentRepository
    {
        public AppointmentRepository(HospitalManagerDbContext context) : base(context) { }
    }
}

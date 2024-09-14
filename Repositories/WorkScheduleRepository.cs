using HospitalManagementWebApp.Models;
using HospitalManagementWebApp.Repositories.Interfaces;

namespace HospitalManagementWebApp.Repositories
{
    public class WorkScheduleRepository : GenericRepository<WorkSchedule>, IWorkScheduleRepository
    {
        public WorkScheduleRepository(HospitalManagerDbContext context) : base(context)
        {
            
        }
    }
}

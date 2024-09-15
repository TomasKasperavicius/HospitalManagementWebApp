using HospitalManagementWebApp.Models;
using HospitalManagementWebApp.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagementWebApp.Repositories
{
    public class WorkScheduleRepository : GenericRepository<WorkSchedule>, IWorkScheduleRepository
    {
        public WorkScheduleRepository(HospitalManagerDbContext context) : base(context)
        {
        }
        public IEnumerable<WorkSchedule> AddRange(IEnumerable<WorkSchedule> workSchedules)
        {
            _context.Set<WorkSchedule>().AddRange(workSchedules);
            _context.SaveChanges();
            return workSchedules;
        }
    }
}

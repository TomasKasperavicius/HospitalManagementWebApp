using HospitalManagementWebApp.Models;

namespace HospitalManagementWebApp.Repositories.Interfaces
{
    public interface IWorkScheduleRepository : IGenericRepository<WorkSchedule>
    {
        IEnumerable<WorkSchedule> AddRange(IEnumerable<WorkSchedule> workSchedules);
    }
}

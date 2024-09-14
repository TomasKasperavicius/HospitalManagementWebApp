using HospitalManagementWebApp.Models;
using HospitalManagementWebApp.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagementWebApp.Repositories
{
    public class DoctorRepository : GenericRepository<Doctor>, IDoctorRepository
    {
        public DoctorRepository(HospitalManagerDbContext context) : base(context)
        {
        }
    }
}

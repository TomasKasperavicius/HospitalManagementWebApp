using HospitalManagementWebApp.Models;
using HospitalManagementWebApp.Repositories.Interfaces;

namespace HospitalManagementWebApp.Repositories
{
    public class PatientRepository : GenericRepository<Patient>, IPatientRepository
    {
        public PatientRepository(HospitalManagerDbContext context) : base(context) { }
    }
}

using HospitalManagementWebApp.Models;
using HospitalManagementWebApp.Repositories.Interfaces;

namespace HospitalManagementWebApp.Repositories
{
    public class MedicalHistoryRepository : GenericRepository<MedicalHistory>, IMedicalHistoryRepository
    {
        public MedicalHistoryRepository(HospitalManagerDbContext context) : base(context) { }
        public IEnumerable<MedicalHistory> GetPatientMedicalHistory(int patientId)
        {
            return _context.Set<MedicalHistory>().Where(a => a.PatientID == patientId);
        }
    }
}

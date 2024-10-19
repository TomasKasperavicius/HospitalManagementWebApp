using HospitalManagementWebApp.Models;

namespace HospitalManagementWebApp.Repositories.Interfaces
{
    public interface IMedicalHistoryRepository : IGenericRepository<MedicalHistory>
    {
        IEnumerable<MedicalHistory> GetPatientMedicalHistory(int patientId);
    }
}

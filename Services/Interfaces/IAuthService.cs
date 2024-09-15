using HospitalManagementWebApp.Models;

namespace HospitalManagementWebApp.Services.Interfaces
{
    public interface IAuthService
    {
        IUser? Login(LoginCrediantials loginCrediantials);
        Patient? RegisterPatient(Patient patient);
        Doctor? RegisterDoctor(RegisterDoctorViewModel registerDoctorViewModel);
    }
}

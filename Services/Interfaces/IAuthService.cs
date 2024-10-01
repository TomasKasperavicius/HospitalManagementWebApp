using HospitalManagementWebApp.Models;
using HospitalManagementWebApp.Models.Dto;

namespace HospitalManagementWebApp.Services.Interfaces
{
    public interface IAuthService
    {
        IUser? Login(LoginCrediantials loginCrediantials);
        Patient? RegisterPatient(Patient patient);
        Doctor? RegisterDoctor(RegisterDoctorViewModel registerDoctorViewModel);
        string GenerateJwt(JwtDTO jwtDTO);
    }
}

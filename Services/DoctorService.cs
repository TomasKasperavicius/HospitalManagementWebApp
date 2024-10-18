using HospitalManagementWebApp.Models;
using HospitalManagementWebApp.Repositories;
using HospitalManagementWebApp.Repositories.Interfaces;
using HospitalManagementWebApp.Services.Interfaces;

namespace HospitalManagementWebApp.Services
{
    public class DoctorService(IAppointmentRepository appointmentRepository, IDoctorRepository doctorRepository, IAddressRepository addressRepository) : IDoctorService
    {
        public List<Appointment> GetDoctorAppointments(int? doctorID, DateTime date)
        {
            var appointments = appointmentRepository.Find(a => a.DoctorID == doctorID).Where(a => a.Date.Year == date.Year && a.Date.Month == date.Month && a.Date.Day == date.Day).ToList();
            return appointments;
        }

        public List<DoctorListViewModel> GetDoctors()
        {
            var doctors = doctorRepository.GetAll();
            var doctorViewModels = new List<DoctorListViewModel>();

            foreach (var doctor in doctors)
            {
                var address = addressRepository.GetById(doctor.AddressID);
                if (address != null)
                {
                    doctorViewModels.Add(new DoctorListViewModel
                    {
                        ID = doctor.ID,
                        Name = doctor.FirstName + " " + doctor.LastName,
                        Email = doctor.Email,
                        Phone = doctor.Phone,
                        Specialty = (Specialty)doctor.Specialty,
                        Image = doctor.Image,
                        Address = $"{address.Street}, {address.City}, {address.State}, {address.Country}"
                    });
                }
            }
            return doctorViewModels;
        }
        public DoctorListViewModel? GetDoctor(int id)
        {
            var doctor = doctorRepository.GetById(id);
            if (doctor != null)
            {
                var address = addressRepository.GetById(doctor.AddressID);
                if (address != null)
                {
                    var doctorViewModel = new DoctorListViewModel
                    {
                        ID = doctor.ID,
                        Name = doctor.FirstName + " " + doctor.LastName,
                        Email = doctor.Email,
                        Phone = doctor.Phone,
                        Specialty = (Specialty)doctor.Specialty,
                        Image = doctor.Image,
                        Address = $"{address.Street}, {address.City}, {address.State}, {address.Country}"
                    };
                    return doctorViewModel;
                }
            }
            return null;
        }
    }
}

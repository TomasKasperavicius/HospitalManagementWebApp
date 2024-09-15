using HospitalManagementWebApp.Models;
using HospitalManagementWebApp.Repositories.Interfaces;
using HospitalManagementWebApp.Services.Interfaces;

namespace HospitalManagementWebApp.Services
{
    public class UserService(IAppointmentRepository appointmentRepository, IPatientRepository patientRepository, IDoctorRepository doctorRepository, IAddressRepository addressRepository) : IUserService
    {
        public int? CancelAppointment(int appointmentID)
        {
            var appointment = appointmentRepository.GetById(appointmentID);
            if (appointment != null)
            {

                var patientID = appointment.UserID;
                appointment.UserID = null;
                appointment.Status = Status.Free;
                appointmentRepository.Update(appointment);
                return patientID;
            }
            return null;
        }

        public List<AppointmentViewModel> GetPatientAppointments(int patientID)
        {
            var patient = patientRepository.GetById(patientID);
            if (patient == null) return new List<AppointmentViewModel>();

            var appointments = appointmentRepository.GetPatientAppointments(patientID);
            var appointmentViewModels = new List<AppointmentViewModel>();

            foreach (var appointment in appointments)
            {
                var doctor = doctorRepository.GetById(appointment.DoctorID);
                var address = addressRepository.GetById(appointment.AddressID);

                if (doctor != null && address != null)
                {
                    appointmentViewModels.Add(new AppointmentViewModel
                    {
                        ID = appointment.ID,
                        Name = doctor.FirstName + " " + doctor.LastName,
                        Email = doctor.Email,
                        Phone = doctor.Phone,
                        Image = doctor.Image,
                        Specialty = (Specialty)doctor.Specialty,
                        Date = appointment.Date,
                        Address = $"{address.Street}, {address.City}, {address.State}, {address.Country}"
                    });
                }
            }

            return appointmentViewModels;
        }
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

        public ReserveAppointmentModel? ReserveAppointment(ReserveAppointmentModel reserveAppointmentModel)
        {
            var doctor = doctorRepository.GetById(reserveAppointmentModel.DoctorID);
            if (doctor == null)
            {
                return null;
            }

            var appointment = appointmentRepository.Find(a => a.DoctorID == reserveAppointmentModel.DoctorID && a.Date == reserveAppointmentModel.Date).FirstOrDefault();
            if (appointment != null)
            {
                appointment.UserID = reserveAppointmentModel.UserID;
                appointment.Status = Status.Occupied;
                appointmentRepository.Update(appointment);
                return reserveAppointmentModel;
            }
            return null;
        }
    }
}

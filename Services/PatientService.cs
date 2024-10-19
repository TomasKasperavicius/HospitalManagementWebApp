using HospitalManagementWebApp.Models;
using HospitalManagementWebApp.Repositories.Interfaces;
using HospitalManagementWebApp.Services.Interfaces;

namespace HospitalManagementWebApp.Services
{
    public class PatientService(IAppointmentRepository appointmentRepository, IPatientRepository patientRepository, IDoctorRepository doctorRepository, IAddressRepository addressRepository, IMedicalHistoryRepository medicalHistoryRepository) : IPatientService
    {
        public Patient? GetPatient(int patientID)
        {
            return patientRepository.GetById(patientID);
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
        public List<MedicalHistory> GetPatientMedicalHistory(int patientID)
        {
            return medicalHistoryRepository.GetPatientMedicalHistory(patientID).ToList();
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
    }
}

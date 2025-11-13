using System.Collections.Generic;
using System.Linq;
using Business;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    public class PatientDAO
    {
        public List<Patient> GetAll()
        {
            using var context = new HospitalManagementContext();
            return context.Patients
                            .Where(p => p.IsActive == true || p.IsActive == null)
                          .ToList();
        }

        public Patient? GetById(int id)
        {
            using var context = new HospitalManagementContext();
            return context.Patients.FirstOrDefault(p => p.PatientId == id);
        }

        public void Add(Patient patient)
        {
            using var context = new HospitalManagementContext();
            patient.IsActive ??= true;
            context.Patients.Add(patient);
            context.SaveChanges();
        }

        public void Update(Patient patient)
        {
            using var context = new HospitalManagementContext();
            var existing = context.Patients.FirstOrDefault(p => p.PatientId == patient.PatientId);
            if (existing == null) return;

            existing.FullName = patient.FullName;
            existing.Gender = patient.Gender;
            existing.Address = patient.Address;
            existing.Dob = patient.Dob;
            existing.IsActive = patient.IsActive;
            existing.Username = patient.Username;
            existing.Password = patient.Password;

            context.SaveChanges();
        }

        public void SetActive(int patientId, bool active)
        {
            using var context = new HospitalManagementContext();
            var p = context.Patients.FirstOrDefault(p => p.PatientId == patientId);
            if (p == null) return;

            p.IsActive = active;
            context.SaveChanges();
        }

        public Patient? Login(string username, string password)
        {
            using var context = new HospitalManagementContext();
            return context.Patients
                          .FirstOrDefault(p => p.Username == username &&
                                               p.Password == password &&
                                               p.IsActive == true);
        }

        public List<Appointment> GetAppointments(int patientId)
        {
            using var context = new HospitalManagementContext();
            return context.Appointments
                          .Include(a => a.Doctor)
                          .Where(a => a.PatientId == patientId)
                          .OrderByDescending(a => a.AppointmentDate)
                          .ToList();
        }

        public List<Admission> GetAdmissions(int patientId)
        {
            using var context = new HospitalManagementContext();
            return context.Admissions
                          .Include(a => a.Room)
                          .Where(a => a.PatientId == patientId)
                          .OrderByDescending(a => a.AdmissionDate)
                          .ToList();
        }
    }
}

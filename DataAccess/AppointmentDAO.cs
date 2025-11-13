using System;
using System.Collections.Generic;
using System.Linq;
using Business;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    public class AppointmentDAO
    {
        public List<Appointment> GetAll()
        {
            using var context = new HospitalManagementContext();
            return context.Appointments
                .Include(a => a.Patient)
                .Include(a => a.Doctor)
                .OrderByDescending(a => a.AppointmentDate)
                .ToList();
        }

        public Appointment? GetById(int id)
        {
            using var context = new HospitalManagementContext();
            return context.Appointments
                .Include(a => a.Patient)
                .Include(a => a.Doctor)
                .FirstOrDefault(a => a.AppointmentId == id);
        }

        public void Add(Appointment appointment)
        {
            using var context = new HospitalManagementContext();
            appointment.CreatedAt ??= DateTime.Now;
            appointment.Status ??= "Scheduled";
            context.Appointments.Add(appointment);
            context.SaveChanges();
        }

        public void UpdateStatus(int id, string status)
        {
            using var context = new HospitalManagementContext();
            var appt = context.Appointments.FirstOrDefault(a => a.AppointmentId == id);
            if (appt == null) return;

            appt.Status = status;
            context.SaveChanges();
        }

        public List<Appointment> GetByDoctor(int doctorId)
        {
            using var context = new HospitalManagementContext();
            return context.Appointments
                .Include(a => a.Patient)
                .Where(a => a.DoctorId == doctorId)
                .ToList();
        }

        public List<Appointment> GetByPatient(int patientId)
        {
            using var context = new HospitalManagementContext();
            return context.Appointments
                .Include(a => a.Doctor)
                .Where(a => a.PatientId == patientId)
                .ToList();
        }

        public void UpdateNotes(int id, string? notes)
        {
            using var context = new HospitalManagementContext();
            var appt = context.Appointments.FirstOrDefault(a => a.AppointmentId == id);
            if (appt == null) return;
            appt.Notes = notes;
            context.SaveChanges();
        }


    }
}

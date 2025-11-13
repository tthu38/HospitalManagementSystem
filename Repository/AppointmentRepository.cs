using System.Collections.Generic;
using Business;
using DataAccess;

namespace Repository
{
    public class AppointmentRepository : IAppointmentRepository
    {
        private readonly AppointmentDAO dao = new();

        public List<Appointment> GetAll() => dao.GetAll();
        public Appointment? GetById(int id) => dao.GetById(id);
        public void Add(Appointment a) => dao.Add(a);
        public void UpdateStatus(int id, string status) => dao.UpdateStatus(id, status);
        public List<Appointment> GetByDoctor(int doctorId) => dao.GetByDoctor(doctorId);
        public List<Appointment> GetByPatient(int patientId) => dao.GetByPatient(patientId);
        public void UpdateNotes(int id, string? notes) => dao.UpdateNotes(id, notes);

    }
}

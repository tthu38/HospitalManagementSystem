using System.Collections.Generic;
using Business;

namespace Repository
{
    public interface IAppointmentRepository
    {
        List<Appointment> GetAll();
        Appointment? GetById(int id);
        void Add(Appointment appointment);
        void UpdateStatus(int id, string status);
        List<Appointment> GetByDoctor(int doctorId);
        List<Appointment> GetByPatient(int patientId);
        void UpdateNotes(int id, string? notes);
    }
}

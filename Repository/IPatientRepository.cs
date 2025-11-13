using System.Collections.Generic;
using Business;

namespace Repository
{
    public interface IPatientRepository
    {
        List<Patient> GetAll();
        Patient? GetById(int id);
        void Add(Patient patient);
        void Update(Patient patient);
        void SetActive(int patientId, bool active);
        Patient? Login(string username, string password);
        List<Appointment> GetAppointments(int patientId);
        List<Admission> GetAdmissions(int patientId);
    }
}

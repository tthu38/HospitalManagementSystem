using System.Collections.Generic;
using Business;
using DataAccess;

namespace Repository
{
    public class PatientRepository : IPatientRepository
    {
        private readonly PatientDAO dao = new();

        public List<Patient> GetAll() => dao.GetAll();

        public Patient? GetById(int id) => dao.GetById(id);

        public void Add(Patient patient) => dao.Add(patient);

        public void Update(Patient patient) => dao.Update(patient);

        public void SetActive(int patientId, bool active) => dao.SetActive(patientId, active);

        public Patient? Login(string username, string password) => dao.Login(username, password);

        public List<Appointment> GetAppointments(int patientId) => dao.GetAppointments(patientId);

        public List<Admission> GetAdmissions(int patientId) => dao.GetAdmissions(patientId);
    }
}

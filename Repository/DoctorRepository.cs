using System.Collections.Generic;
using Business;
using DataAccess;

namespace Repository
{
    public class DoctorRepository : IDoctorRepository
    {
        private readonly DoctorDAO dao = new();

        public List<Doctor> GetAll() => dao.GetAll();
        public Doctor? GetById(int id) => dao.GetById(id);
        public void Add(Doctor doctor) => dao.Add(doctor);
        public void Update(Doctor doctor) => dao.Update(doctor);
        public void SetActive(int id, bool active) => dao.SetActive(id, active);
        public Doctor? Login(string username, string password) => dao.Login(username, password);
    }
}

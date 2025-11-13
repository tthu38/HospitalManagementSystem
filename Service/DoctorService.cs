using System.Collections.Generic;
using Business;
using Repository;

namespace Service
{
    public class DoctorService
    {
        private readonly IDoctorRepository repo;

        public DoctorService()
        {
            repo = new DoctorRepository();
        }

        public List<Doctor> GetAll() => repo.GetAll();

        public Doctor? GetById(int id) => repo.GetById(id);

        public void Create(Doctor doctor)
        {
            repo.Add(doctor);
        }

        public void Update(Doctor doctor)
        {
            repo.Update(doctor);
        }

        public void SetActive(int id, bool active)
        {
            repo.SetActive(id, active);
        }

        public Doctor? Login(string username, string password)
        {
            return repo.Login(username, password);
        }
    }
}

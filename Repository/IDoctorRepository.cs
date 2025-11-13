using System.Collections.Generic;
using Business;

namespace Repository
{
    public interface IDoctorRepository
    {
        List<Doctor> GetAll();
        Doctor? GetById(int id);
        void Add(Doctor doctor);
        void Update(Doctor doctor);
        void SetActive(int id, bool active);
        Doctor? Login(string username, string password);
    }
}

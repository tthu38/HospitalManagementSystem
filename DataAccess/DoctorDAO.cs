using System.Collections.Generic;
using System.Linq;
using Business;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    public class DoctorDAO
    {
        public List<Doctor> GetAll()
        {
            using var context = new HospitalManagementContext();
            return context.Doctors.Include(d => d.Department).ToList();
        }

        public Doctor? GetById(int id)
        {
            using var context = new HospitalManagementContext();
            return context.Doctors.Include(d => d.Department).FirstOrDefault(d => d.DoctorId == id);
        }

        public void Add(Doctor doctor)
        {
            using var context = new HospitalManagementContext();
            context.Doctors.Add(doctor);
            context.SaveChanges();
        }

        public void Update(Doctor doctor)
        {
            using var context = new HospitalManagementContext();
            var existing = context.Doctors.FirstOrDefault(d => d.DoctorId == doctor.DoctorId);
            if (existing == null) return;

            existing.FullName = doctor.FullName;
            existing.DepartmentId = doctor.DepartmentId;
            existing.Specialization = doctor.Specialization;
            existing.Username = doctor.Username;
            existing.Password = doctor.Password;
            existing.IsActive = doctor.IsActive;
            context.SaveChanges();
        }

        public void SetActive(int id, bool active)
        {
            using var context = new HospitalManagementContext();
            var doctor = context.Doctors.FirstOrDefault(d => d.DoctorId == id);
            if (doctor == null) return;
            doctor.IsActive = active;
            context.SaveChanges();
        }

        public Doctor? Login(string username, string password)
        {
            using var context = new HospitalManagementContext();
            return context.Doctors.Include(d => d.Department)
                .FirstOrDefault(d => d.Username == username && d.Password == password && d.IsActive == true);
        }
    }
}

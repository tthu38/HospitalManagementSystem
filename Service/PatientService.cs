using System.Collections.Generic;
using Business;
using Repository;

namespace Service
{
    public class PatientService
    {
        private readonly IPatientRepository repo;

        public PatientService()
        {
            repo = new PatientRepository();
        }

        // Lấy tất cả bệnh nhân (admin xem danh sách)
        public List<Patient> GetAll() => repo.GetAll();

        // Lấy một bệnh nhân theo Id
        public Patient? GetById(int id) => repo.GetById(id);

        // ✅ Register – dùng khi đăng ký từ RegisterWindow
        public void Register(Patient patient)
        {
            repo.Add(patient);
        }

        // Cập nhật hồ sơ bệnh nhân (admin hoặc bệnh nhân tự sửa)
        public void Update(Patient patient)
        {
            repo.Update(patient);
        }

        // Active / Inactive bệnh nhân (xoá mềm)
        public void SetActive(int patientId, bool active)
        {
            repo.SetActive(patientId, active);
        }

        // Login bệnh nhân
        public Patient? Login(string username, string password)
        {
            return repo.Login(username, password);
        }

        // Lịch hẹn của bệnh nhân
        public List<Appointment> GetAppointments(int patientId)
        {
            return repo.GetAppointments(patientId);
        }

        // Lịch sử nhập viện của bệnh nhân
        public List<Admission> GetAdmissions(int patientId)
        {
            return repo.GetAdmissions(patientId);
        }
    }
}

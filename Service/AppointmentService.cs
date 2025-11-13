using Business;
using Repository;

namespace Service
{
    public class AppointmentService
    {
        private readonly IAppointmentRepository _repo = new AppointmentRepository();

        public void Add(Appointment appointment) => _repo.Add(appointment);
        public List<Appointment> GetByDoctor(int doctorId) => _repo.GetByDoctor(doctorId);
        public List<Appointment> GetByPatient(int patientId) => _repo.GetByPatient(patientId);
        public void Confirm(int id) => _repo.UpdateStatus(id, "Confirmed");
        public void Cancel(int id) => _repo.UpdateStatus(id, "Cancelled");

        // ✅ mới: cập nhật notes
        public void UpdateNotes(int id, string? notes) => _repo.UpdateNotes(id, notes);


        public void UpdateStatus(int id, string status)
        {
            var repo = new AppointmentRepository();
            repo.UpdateStatus(id, status);
        }
    }
}

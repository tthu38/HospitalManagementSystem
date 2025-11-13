using Business;
using Service;
using System.Linq;
using System.Windows;

namespace HospitalApp
{
    public partial class DoctorDashboard : Window
    {
        private readonly Doctor _loggedDoctor;
        private readonly AppointmentService _appointmentService = new();
        private readonly PatientService _patientService = new();
        private readonly AdmissionService _admissionService = new();

        public DoctorDashboard(Doctor doctor)
        {
            InitializeComponent();
            _loggedDoctor = doctor;
            LoadAppointments();
        }

        private void LoadAppointments()
        {
            dgAppointments.ItemsSource = _appointmentService
                .GetByDoctor(_loggedDoctor.DoctorId)
                .OrderByDescending(a => a.AppointmentDate)
                .ToList();
        }

        private void BtnAppointment_Click(object sender, RoutedEventArgs e) => LoadAppointments();

        private void BtnConfirm_Click(object sender, RoutedEventArgs e)
        {
            if (dgAppointments.SelectedItem is not Appointment a)
            {
                MessageBox.Show("Please select an appointment to confirm.");
                return;
            }

            if (a.Status == "Cancelled")
            {
                MessageBox.Show("Cannot confirm a cancelled appointment.");
                return;
            }

            if (a.Status == "Confirmed")
            {
                MessageBox.Show("This appointment is already confirmed.");
                return;
            }

            _appointmentService.UpdateStatus(a.AppointmentId, "Confirmed");
            LoadAppointments();
            MessageBox.Show("Appointment confirmed successfully.");
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            if (dgAppointments.SelectedItem is not Appointment a)
            {
                MessageBox.Show("Please select an appointment to cancel.");
                return;
            }

            if (a.Status == "Cancelled")
            {
                MessageBox.Show("This appointment is already cancelled.");
                return;
            }

            if (a.Status == "Confirmed")
            {
                MessageBox.Show("Cannot cancel a confirmed appointment.");
                return;
            }

            if (a.AppointmentDate <= DateTime.Now.AddDays(1))
            {
                MessageBox.Show("Appointments can only be cancelled at least 1 day in advance.");
                return;
            }

            _appointmentService.UpdateStatus(a.AppointmentId, "Cancelled");
            LoadAppointments();
            MessageBox.Show("Appointment cancelled successfully.");
        }


        private void BtnNotes_Click(object sender, RoutedEventArgs e)
        {
            if (dgAppointments.SelectedItem is not Appointment a)
            {
                MessageBox.Show("Select an appointment first.");
                return;
            }

            var win = new AppointmentNotesWindow(a);
            if (win.ShowDialog() == true)
                LoadAppointments();
        }

        private void BtnPatient_Click(object sender, RoutedEventArgs e)
        {
            new DoctorPatientWindow().ShowDialog();
        }


        private void BtnAdmission_Click(object sender, RoutedEventArgs e)
        {
            new DoctorAdmissionWindow().ShowDialog();
        }

        private void BtnLogout_Click(object sender, RoutedEventArgs e)
        {
            // Khi bác sĩ bấm Logout, quay lại màn hình đăng nhập
            new HomeWindow().Show();
            this.Close();
        }

    }
}

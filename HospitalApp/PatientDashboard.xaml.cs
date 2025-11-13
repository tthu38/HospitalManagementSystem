using System;
using System.Linq;
using System.Windows;
using Business;
using Service;

namespace HospitalApp
{
    public partial class PatientDashboard : Window
    {
        private readonly Patient _loggedPatient;
        private readonly AppointmentService _appointmentService = new();
        private readonly BillingService _billingService = new();
        private readonly FeedbackService _feedbackService = new();
        private readonly PatientService _patientService = new();

        public PatientDashboard(Patient patient)
        {
            InitializeComponent();
            _loggedPatient = patient;
            txtWelcome.Text = $"Welcome, {_loggedPatient.FullName}";
            LoadAppointments();
        }

        private void LoadAppointments()
        {
            dgAppointments.ItemsSource = _appointmentService.GetByPatient(_loggedPatient.PatientId)
                .OrderByDescending(a => a.AppointmentDate)
                .ToList();
        }

        private void BtnAppointment_Click(object sender, RoutedEventArgs e) => LoadAppointments();

        private void BtnBilling_Click(object sender, RoutedEventArgs e)
        {
            dgAppointments.ItemsSource = _billingService.GetByPatient(_loggedPatient.PatientId);
        }

        private void BtnFeedback_Click(object sender, RoutedEventArgs e)
        {
            new FeedbackWindow(_loggedPatient.PatientId).ShowDialog();
        }

        private void BtnProfile_Click(object sender, RoutedEventArgs e)
        {
            new PatientProfile(_loggedPatient).ShowDialog();
        }

        private void BtnLogout_Click(object sender, RoutedEventArgs e)
        {
            new HomeWindow().Show();
            Close();
        }

        private void BtnBook_Click(object sender, RoutedEventArgs e)
        {
            var win = new AppointmentCreateWindow(_loggedPatient.PatientId);
            if (win.ShowDialog() == true)
            {
                LoadAppointments();
            }
        }

        private void BtnCancelAppointment_Click(object sender, RoutedEventArgs e)
        {
            if (dgAppointments.SelectedItem is not Appointment appt)
            {
                MessageBox.Show("Please select an appointment to cancel.");
                return;
            }

            if (appt.Status == "Cancelled")
            {
                MessageBox.Show("This appointment is already cancelled.");
                return;
            }

            if (appt.AppointmentDate <= DateTime.Now)
            {
                MessageBox.Show("You can only cancel future appointments.");
                return;
            }

            if (MessageBox.Show("Do you really want to cancel this appointment?",
                                "Confirm",
                                MessageBoxButton.YesNo,
                                MessageBoxImage.Question) == MessageBoxResult.No)
            {
                return;
            }

            _appointmentService.Cancel(appt.AppointmentId);
            LoadAppointments();
            MessageBox.Show("Appointment cancelled.");
        }

        private void BtnAdmissions_Click(object sender, RoutedEventArgs e)
        {
            var list = _patientService.GetAdmissions(_loggedPatient.PatientId);
            dgAppointments.ItemsSource = list;
        }

        private void BtnDoctors_Click(object sender, RoutedEventArgs e)
        {
            var win = new DoctorBrowseWindow
            {
                Owner = this
            };
            win.ShowDialog();
        }

        private void BtnChat_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // ✅ Dùng ShowDialog() thay vì Show() để tránh lỗi "window already closed"
                var chat = new ChatBotWindow
                {
                    Owner = this
                };
                chat.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Không thể mở chatbot: {ex.Message}", "Error",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}

using Business;
using Service;
using System;
using System.Windows;
using System.Windows.Controls;

namespace HospitalApp
{
    public partial class AppointmentCreateWindow : Window
    {
        private readonly AppointmentService _service = new();
        private readonly DoctorService _doctorService = new();
        private readonly int _patientId;

        public AppointmentCreateWindow(int patientId)
        {
            InitializeComponent();
            _patientId = patientId;
            cbDoctor.ItemsSource = _doctorService.GetAll();
            cbDoctor.DisplayMemberPath = "FullName";
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            if (cbDoctor.SelectedItem is not Doctor doctor || dpDate.SelectedDate == null)
            {
                MessageBox.Show("Please fill all fields.");
                return;
            }

            if (!TimeSpan.TryParse(txtTime.Text, out var time))
            {
                MessageBox.Show("Invalid time format. Use HH:mm");
                return;
            }

            var dt = dpDate.SelectedDate.Value.Add(time);

            var appointment = new Appointment
            {
                DoctorId = doctor.DoctorId,
                PatientId = _patientId,
                AppointmentDate = dt,
                Status = "Scheduled",
                CreatedAt = DateTime.Now
            };

            _service.Add(appointment);
            MessageBox.Show("Appointment created successfully!");
            DialogResult = true;
        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (sender is TextBox t && t.Text == "HH:mm")
                t.Text = "";
        }

        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (sender is TextBox t && string.IsNullOrWhiteSpace(t.Text))
                t.Text = "HH:mm";
        }

    }
}

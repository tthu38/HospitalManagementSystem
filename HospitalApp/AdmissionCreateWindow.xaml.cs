using System;
using System.Windows;
using Business;
using Service;

namespace HospitalApp
{
    public partial class AdmissionCreateWindow : Window
    {
        private readonly PatientService _patientService = new();
        private readonly RoomService _roomService = new();
        private readonly AdmissionService _admissionService = new();

        public AdmissionCreateWindow()
        {
            InitializeComponent();

            // Load danh sách bệnh nhân và phòng trống
            cbPatient.ItemsSource = _patientService.GetAll();
            cbRoom.ItemsSource = _roomService.GetAvailable();
        }

        private void BtnAdmit_Click(object sender, RoutedEventArgs e)
        {
            // 1️⃣ Kiểm tra chọn bệnh nhân và phòng
            if (cbPatient.SelectedItem is not Patient p || cbRoom.SelectedItem is not Room r)
            {
                MessageBox.Show("Please select both patient and room!");
                return;
            }

            // 2️⃣ Lấy ngày nhập viện (nếu không chọn thì lấy ngày hiện tại)
            DateTime admissionDate = dpAdmissionDate.SelectedDate ?? DateTime.Now;

            // 3️⃣ Tạo đối tượng Admission mới
            var admission = new Admission
            {
                PatientId = p.PatientId,
                RoomId = r.RoomId,
                AdmissionDate = admissionDate, // ✅ DateTime
                Status = "Active"
            };

            try
            {
                _admissionService.Create(admission);
                MessageBox.Show("Patient admitted successfully!");
                DialogResult = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Cannot create admission:\n" +
                    (ex.InnerException?.Message ?? ex.Message),
                    "Error",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error
                );
            }
        }
    }
}

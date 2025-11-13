using System;
using System.Linq;
using System.Windows;
using Business;
using Service;

namespace HospitalApp
{
    public partial class DoctorAdmissionWindow : Window
    {
        private readonly AdmissionService _service = new();
        private readonly RoomService _roomService = new();
        private readonly PatientService _patientService = new();

        public DoctorAdmissionWindow()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            // ✅ Lấy toàn bộ admission có kèm bệnh nhân và phòng
            dgAdmissions.ItemsSource = _service.GetAll()
                .OrderByDescending(a => a.AdmissionDate)
                .ToList();
        }

        private void BtnNew_Click(object sender, RoutedEventArgs e)
        {
            // ✅ Khi tạo admission mới, chỉ hiển thị bệnh nhân chưa nhập viện
            var win = new AdmissionCreateWindow();
            if (win.ShowDialog() == true)
            {
                LoadData();
            }
        }

        private void BtnDischarge_Click(object sender, RoutedEventArgs e)
        {
            if (dgAdmissions.SelectedItem is not Admission a)
            {
                MessageBox.Show("Select an admission first.");
                return;
            }

            if (a.DischargeDate != null)
            {
                MessageBox.Show("This patient is already discharged.");
                return;
            }

            // ✅ Cập nhật trạng thái Discharged + trả phòng
            _service.Discharge(a.AdmissionId);
            MessageBox.Show("Patient discharged successfully!", "Success",
                MessageBoxButton.OK, MessageBoxImage.Information);

            LoadData();
        }
    }
}

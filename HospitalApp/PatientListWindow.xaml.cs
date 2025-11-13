using System.Windows;
using System.Windows.Controls;
using Business;
using Service;

namespace HospitalApp
{
    public partial class PatientListWindow : UserControl
    {
        private readonly PatientService _service = new();

        public PatientListWindow()
        {
            InitializeComponent();
            LoadPatients();
        }

        private void LoadPatients()
        {
            dgPatients.ItemsSource = _service.GetAll();
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            var win = new PatientDetailWindow(); // form thêm mới
            if (win.ShowDialog() == true)
                LoadPatients();
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (dgPatients.SelectedItem is not Patient selected)
            {
                MessageBox.Show("Please select a patient to edit.");
                return;
            }

            var win = new PatientDetailWindow(selected);
            if (win.ShowDialog() == true)
                LoadPatients();
        }

        private void BtnToggle_Click(object sender, RoutedEventArgs e)
        {
            if (dgPatients.SelectedItem is not Patient p)
            {
                MessageBox.Show("Please select a patient.");
                return;
            }

            bool newStatus = !(p.IsActive ?? true);
            _service.SetActive(p.PatientId, newStatus);
            LoadPatients();

            MessageBox.Show($"Patient {(newStatus ? "activated" : "deactivated")} successfully!");
        }
    }
}

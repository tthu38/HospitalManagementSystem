using System.Windows;
using System.Windows.Controls;
using Business;
using Service;

namespace HospitalApp
{
    public partial class DoctorListWindow : UserControl
    {
        private readonly DoctorService _doctorService = new();

        public DoctorListWindow()
        {
            InitializeComponent();
            LoadDoctors();
        }

        private void LoadDoctors()
        {
            dgDoctors.ItemsSource = _doctorService.GetAll();
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            var win = new DoctorDetailWindow();
            if (win.ShowDialog() == true)
                LoadDoctors();
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (dgDoctors.SelectedItem is not Doctor selected)
            {
                MessageBox.Show("Please select a doctor to edit.");
                return;
            }

            var win = new DoctorDetailWindow(selected);
            if (win.ShowDialog() == true)
                LoadDoctors();
        }

        private void BtnToggle_Click(object sender, RoutedEventArgs e)
        {
            if (dgDoctors.SelectedItem is not Doctor selected)
            {
                MessageBox.Show("Select a doctor first.");
                return;
            }

            bool newStatus = !(selected.IsActive ?? true);
            _doctorService.SetActive(selected.DoctorId, newStatus);
            LoadDoctors();

            MessageBox.Show($"Doctor {(newStatus ? "activated" : "deactivated")} successfully!");
        }
    }
}

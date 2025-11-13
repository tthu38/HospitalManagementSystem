using System;
using System.Windows;
using Business;
using Service;

namespace HospitalApp
{
    public partial class RegisterWindow : Window
    {
        private readonly PatientService _patientService = new();

        public RegisterWindow()
        {
            InitializeComponent();
        }

        private void BtnRegister_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUsername.Text) ||
                string.IsNullOrWhiteSpace(txtPassword.Password) ||
                string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("Please enter at least full name, username and password.",
                                "Validation", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            string? gender = (cbGender.SelectedItem as System.Windows.Controls.ComboBoxItem)?.Content?.ToString();

            DateOnly? dob = null;
            if (dpDob.SelectedDate.HasValue)
            {
                var d = dpDob.SelectedDate.Value;
                dob = new DateOnly(d.Year, d.Month, d.Day);
            }

            var patient = new Patient
            {
                FullName = txtName.Text.Trim(),
                Gender = gender,
                Address = txtAddress.Text.Trim(),
                Username = txtUsername.Text.Trim(),
                Password = txtPassword.Password.Trim(),
                Dob = dob,
                IsActive = true
            };

            try
            {
                _patientService.Register(patient); // dùng Create (đã thêm ở service)
                MessageBox.Show("Registration successful! You can now log in.",
                                "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                DialogResult = true;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Cannot register patient: " + ex.Message,
                                "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}

using System;
using System.Windows;
using Business;
using Service;

namespace HospitalApp
{
    public partial class PatientProfile : Window
    {
        private readonly PatientService _service = new();
        private readonly Patient _patient;

        public PatientProfile(Patient patient)
        {
            InitializeComponent();
            _patient = patient;

            // Load data lên form
            txtName.Text = _patient.FullName;
            txtUsername.Text = _patient.Username;
            txtPassword.Password = _patient.Password;
            txtAddress.Text = _patient.Address;
            txtEmail.Text = _patient.Email;
            txtPhone.Text = _patient.Phone;


            if (_patient.Dob.HasValue)
                dpDob.SelectedDate = _patient.Dob.Value.ToDateTime(TimeOnly.MinValue);

            foreach (var item in cbGender.Items)
            {
                if (item is System.Windows.Controls.ComboBoxItem cbi &&
                    cbi.Content?.ToString() == _patient.Gender)
                {
                    cbGender.SelectedItem = cbi;
                    break;
                }
            }
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtName.Text) ||
                string.IsNullOrWhiteSpace(txtUsername.Text) ||
                string.IsNullOrWhiteSpace(txtPassword.Password))
            {
                MessageBox.Show("Full name, username and password are required.");
                return;
            }

            string? gender = (cbGender.SelectedItem as System.Windows.Controls.ComboBoxItem)
                             ?.Content?.ToString();

            DateOnly? dob = null;
            if (dpDob.SelectedDate.HasValue)
            {
                var d = dpDob.SelectedDate.Value;
                dob = new DateOnly(d.Year, d.Month, d.Day);
            }

            _patient.FullName = txtName.Text.Trim();
            _patient.Username = txtUsername.Text.Trim();
            _patient.Password = txtPassword.Password.Trim();
            _patient.Address = txtAddress.Text.Trim();
            _patient.Gender = gender;
            _patient.Dob = dob;
            _patient.Email = txtEmail.Text.Trim();
            _patient.Phone = txtPhone.Text.Trim();


            _service.Update(_patient);
            MessageBox.Show("Profile updated!");
        }

        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}

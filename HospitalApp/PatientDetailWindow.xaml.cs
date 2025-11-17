using Business;
using Service;
using System;
using System.Windows;
using System.Windows.Controls;

namespace HospitalApp
{
    public partial class PatientDetailWindow : Window
    {
        private readonly PatientService _service = new();
        private readonly Patient? _editingPatient;

        public PatientDetailWindow() : this(null) { }

        public PatientDetailWindow(Patient? patient)
        {
            InitializeComponent();
            _editingPatient = patient;

            if (_editingPatient != null)
            {
                Title = "Edit Patient";
                txtName.Text = _editingPatient.FullName;
                txtUsername.Text = _editingPatient.Username;
                txtPassword.Password = _editingPatient.Password;
                txtAddress.Text = _editingPatient.Address;
                dpDob.SelectedDate = _editingPatient.Dob?.ToDateTime(TimeOnly.MinValue);
                txtEmail.Text = _editingPatient.Email;
                txtPhone.Text = _editingPatient.Phone;


                // chọn đúng gender
                foreach (ComboBoxItem item in cbGender.Items)
                {
                    if (item.Content.ToString() == _editingPatient.Gender)
                    {
                        cbGender.SelectedItem = item;
                        break;
                    }
                }
            }
            else
            {
                Title = "Add Patient";
                cbGender.SelectedIndex = 0;
            }
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtName.Text) ||
                string.IsNullOrWhiteSpace(txtUsername.Text) ||
                string.IsNullOrWhiteSpace(txtPassword.Password))
            {
                MessageBox.Show("Please fill in full name, username and password.");
                return;
            }

            string? gender = (cbGender.SelectedItem as System.Windows.Controls.ComboBoxItem)?.Content?.ToString();
            DateOnly? dob = null;
            if (dpDob.SelectedDate.HasValue)
            {
                var d = dpDob.SelectedDate.Value;
                dob = new DateOnly(d.Year, d.Month, d.Day);
            }

            if (_editingPatient == null)
            {
                // Thêm mới
                var patient = new Patient
                {
                    FullName = txtName.Text.Trim(),
                    Gender = gender,
                    Address = txtAddress.Text.Trim(),
                    Username = txtUsername.Text.Trim(),
                    Password = txtPassword.Password.Trim(),
                    Email = txtEmail.Text.Trim(),
                    Phone = txtPhone.Text.Trim(),

                    Dob = dob,
                    IsActive = true
                };
                _service.Register(patient);
                MessageBox.Show("Patient added successfully!");
            }
            else
            {
                // Cập nhật
                _editingPatient.FullName = txtName.Text.Trim();
                _editingPatient.Gender = gender;
                _editingPatient.Address = txtAddress.Text.Trim();
                _editingPatient.Username = txtUsername.Text.Trim();
                _editingPatient.Password = txtPassword.Password.Trim();
                _editingPatient.Dob = dob;
                _editingPatient.Email = txtEmail.Text.Trim();
                _editingPatient.Phone = txtPhone.Text.Trim();

                _service.Update(_editingPatient);
                MessageBox.Show("Patient updated successfully!");
            }

            DialogResult = true;
            Close();
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}

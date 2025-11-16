using System;
using System.Windows;
using Business;
using Service;
using System.Threading.Tasks;

namespace HospitalApp
{
    public partial class RegisterWindow : Window
    {
        private readonly PatientService _patientService = new();
        private readonly EmailService _emailService = new();
        private string? _generatedOtp;
        private DateTime _otpCreatedAt;

        public RegisterWindow()
        {
            InitializeComponent();
        }

        private async void BtnSendOtp_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                MessageBox.Show("Please enter your email first!");
                return;
            }

            // Sinh mã OTP 6 số
            var random = new Random();
            _generatedOtp = random.Next(100000, 999999).ToString();
            _otpCreatedAt = DateTime.Now;

            try
            {
                await _emailService.SendEmailAsync(
                    txtEmail.Text.Trim(),
                    "Your Hospital Registration OTP Code",
                    $"Your OTP code is: {_generatedOtp}\nThis code will expire in 3 minutes.");

                MessageBox.Show("OTP has been sent to your email!", "Success",
                    MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to send OTP:\n{ex.Message}", "Error",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnRegister_Click(object sender, RoutedEventArgs e)
        {
            // Xác minh OTP trước khi lưu
            if (string.IsNullOrWhiteSpace(_generatedOtp))
            {
                MessageBox.Show("Please request and verify OTP before registration.");
                return;
            }

            if ((DateTime.Now - _otpCreatedAt).TotalMinutes > 3)
            {
                MessageBox.Show("OTP expired. Please request a new one.");
                return;
            }

            if (txtOtp.Text.Trim() != _generatedOtp)
            {
                MessageBox.Show("Invalid OTP. Please check your email again.");
                return;
            }

            // Kiểm tra thông tin cơ bản
            if (string.IsNullOrWhiteSpace(txtUsername.Text) ||
                string.IsNullOrWhiteSpace(txtPassword.Password) ||
                string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("Please enter full name, username and password.",
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
                IsActive = true,
                Email = txtEmail.Text.Trim(),     // ✅ Thêm dòng này
                Phone = txtPhone.Text.Trim(),
            };

            try
            {
                _patientService.Register(patient);
                MessageBox.Show("Registration successful! You can now log in.",
                    "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                DialogResult = true;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Cannot register patient:\n" +
                    (ex.InnerException?.InnerException?.Message ?? ex.InnerException?.Message ?? ex.Message),
                    "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}

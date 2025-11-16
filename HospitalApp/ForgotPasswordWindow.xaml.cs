using System;
using System.Windows;
using Service;
using Business;

namespace HospitalApp
{
    public partial class ForgotPasswordWindow : Window
    {
        private readonly EmailService _emailService = new();
        private readonly PatientService _patientService = new();
        private string? _sentOtp;
        private string? _emailTarget;

        public ForgotPasswordWindow()
        {
            InitializeComponent();
        }

        private async void BtnSendOtp_Click(object sender, RoutedEventArgs e)
        {
            _emailTarget = txtEmail.Text.Trim();

            if (string.IsNullOrWhiteSpace(_emailTarget))
            {
                MessageBox.Show("Please enter your email address.");
                return;
            }

            // kiểm tra có tài khoản với email này không
            var patient = _patientService.GetAll().Find(p => p.Username == _emailTarget || p.Email == _emailTarget);
            if (patient == null)
            {
                MessageBox.Show("Email not found in system.");
                return;
            }

            try
            {
                var random = new Random();
                _sentOtp = random.Next(100000, 999999).ToString();

                string subject = "Hospital System - Password Reset OTP";
                string body = $"Your OTP code is: {_sentOtp}\nThis code will expire in 5 minutes.";

                await _emailService.SendEmailAsync(_emailTarget, subject, body);
                MessageBox.Show("OTP has been sent to your email.", "Success");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to send OTP: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnResetPassword_Click(object sender, RoutedEventArgs e)
        {
            if (txtOtp.Text.Trim() != _sentOtp)
            {
                MessageBox.Show("Invalid OTP. Please check again.");
                return;
            }

            var newPass = txtNewPassword.Password.Trim();
            if (string.IsNullOrWhiteSpace(newPass))
            {
                MessageBox.Show("Please enter a new password.");
                return;
            }

            var patient = _patientService.GetAll().Find(p => p.Username == _emailTarget || p.Email == _emailTarget);
            if (patient == null)
            {
                MessageBox.Show("User not found.");
                return;
            }

            patient.Password = newPass;
            _patientService.Update(patient);
            MessageBox.Show("Password reset successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            Close();
        }
    }
}

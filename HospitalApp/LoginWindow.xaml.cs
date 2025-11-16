using Business;
using Service;
using System.Windows;
using System.Windows.Controls;

namespace HospitalApp
{
    public partial class LoginWindow : Window
    {
        private readonly AdminService _adminService = new();
        private readonly DoctorService _doctorService = new();
        private readonly PatientService _patientService = new();

        public LoginWindow()
        {
            InitializeComponent();
        }

        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            var username = txtUsername.Text;
            var password = txtPassword.Password;
            var role = (cbRole.SelectedItem as ComboBoxItem)?.Content.ToString();

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Please enter username and password!");
                return;
            }

            switch (role)
            {
                case "Admin":
                    var admin = _adminService.Login(username, password);
                    if (admin != null)
                    {
                        new AdminDashboard(admin).Show();
                        this.Close();
                    }
                    else MessageBox.Show("Invalid Admin credentials.");
                    break;

                case "Doctor":
                    var doctor = _doctorService.Login(username, password);
                    if (doctor != null)
                    {
                        new DoctorDashboard(doctor).Show();
                        this.Close();
                    }
                    else MessageBox.Show("Invalid Doctor credentials.");
                    break;

                case "Patient":
                    var patient = _patientService.Login(username, password);
                    if (patient != null)
                    {
                        new PatientDashboard(patient).Show();
                        this.Close();
                    }
                    else MessageBox.Show("Invalid Patient credentials.");
                    break;
            }
        }

        private void BtnForgotPassword_Click(object sender, RoutedEventArgs e)
        {
            var forgotWin = new ForgotPasswordWindow();
            forgotWin.ShowDialog();
        }

       


    }
}

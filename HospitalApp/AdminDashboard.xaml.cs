using System.Windows;
using Business;

namespace HospitalApp
{
    public partial class AdminDashboard : Window
    {
        private readonly Admin _loggedAdmin;

        public AdminDashboard(Admin admin)
        {
            InitializeComponent();
            _loggedAdmin = admin;
        }

        private void BtnDoctor_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new DoctorListWindow();
        }

        private void BtnPatient_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new PatientListWindow();
        }

        private void BtnRoom_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new RoomListWindow();
        }

        private void BtnBilling_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new BillingManagementWindow();
        }

        private void BtnLogout_Click(object sender, RoutedEventArgs e)
        {
            new HomeWindow().Show();
            this.Close();
        }
    }
}

using System.Windows;
using Business;
using Service;

namespace HospitalApp
{
    public partial class DoctorBrowseWindow : Window
    {
        private readonly DoctorService _doctorService = new();

        public DoctorBrowseWindow()
        {
            InitializeComponent();
            dgDoctors.ItemsSource = _doctorService.GetAll();
        }

        private void BtnViewDetail_Click(object sender, RoutedEventArgs e)
        {
            if (dgDoctors.SelectedItem is not Doctor doc)
            {
                MessageBox.Show("Please select a doctor.");
                return;
            }

            var win = new DoctorProfile(doc);
            win.Owner = this;
            win.ShowDialog();
        }
    }
}

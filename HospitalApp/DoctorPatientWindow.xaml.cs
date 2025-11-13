using System.Windows;
using Service;

namespace HospitalApp
{
    public partial class DoctorPatientWindow : Window
    {
        private readonly PatientService _service = new();
        public DoctorPatientWindow()
        {
            InitializeComponent();
            dgPatients.ItemsSource = _service.GetAll();
        }
    }
}

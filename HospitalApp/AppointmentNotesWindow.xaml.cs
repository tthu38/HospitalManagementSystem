using System.Windows;
using Business;
using Service;

namespace HospitalApp
{
    public partial class AppointmentNotesWindow : Window
    {
        private readonly AppointmentService _service = new();
        private readonly Appointment _appointment;

        public AppointmentNotesWindow(Appointment appointment)
        {
            InitializeComponent();
            _appointment = appointment;
            txtNotes.Text = appointment.Notes;
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            _appointment.Notes = txtNotes.Text;
            _service.UpdateNotes(_appointment.AppointmentId, _appointment.Notes);
            MessageBox.Show("Notes updated successfully.");
            DialogResult = true;
        }
    }
}

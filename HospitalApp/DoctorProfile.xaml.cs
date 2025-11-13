using System.Linq;
using System.Windows;
using Business;
using Service;

namespace HospitalApp
{
    public partial class DoctorProfile : Window
    {
        private readonly Doctor _doctor;
        private readonly FeedbackService _feedbackService = new();

        public DoctorProfile(Doctor doctor)
        {
            InitializeComponent();
            _doctor = doctor;
            txtName.Text = doctor.FullName;
            txtDepartment.Text = $"Department: {doctor.Department?.Name ?? "N/A"}";
            txtSpecialization.Text = $"Specialization: {doctor.Specialization}";
        }

        private void BtnFeedback_Click(object sender, RoutedEventArgs e)
        {
            var feedbacks = _feedbackService.GetByDoctor(_doctor.DoctorId);
            if (!feedbacks.Any())
            {
                MessageBox.Show("No feedback yet.");
                return;
            }

            string msg = string.Join("\n\n", feedbacks.Select(f =>
                $"⭐ {f.Rating}/5 by {f.Patient?.FullName}\n{f.Comment}"));
            MessageBox.Show(msg, "Feedbacks");
        }
    }
}

using System;
using System.Windows;
using Business;
using Service;

namespace HospitalApp
{
    public partial class FeedbackWindow : Window
    {
        private readonly FeedbackService _feedbackService = new();
        private readonly DoctorService _doctorService = new();
        private readonly int _patientId;

        public FeedbackWindow(int patientId)
        {
            InitializeComponent();
            _patientId = patientId;

            cbDoctor.ItemsSource = _doctorService.GetAll();
            cbDoctor.DisplayMemberPath = "FullName";
        }

        private void BtnSubmit_Click(object sender, RoutedEventArgs e)
        {
            if (cbDoctor.SelectedItem is not Doctor doc)
            {
                MessageBox.Show("Please select a doctor.");
                return;
            }

            int? rating = null;
            if (int.TryParse(txtRating.Text, out int r))
                rating = r;

            var f = new Feedback
            {
                PatientId = _patientId,
                DoctorId = doc.DoctorId,
                Rating = rating,
                Comment = txtComment.Text,
                CreatedAt = DateTime.Now
            };

            _feedbackService.Create(f);
            MessageBox.Show("Feedback submitted!");
            Close();
        }
    }
}

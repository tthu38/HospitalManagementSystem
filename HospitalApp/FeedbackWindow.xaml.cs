using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Business;
using Service;

namespace HospitalApp
{
    public partial class FeedbackWindow : Window
    {
        private readonly FeedbackService _feedbackService = new();
        private readonly DoctorService _doctorService = new();
        private readonly int _patientId;

        private int selectedRating = 0;

        public FeedbackWindow(int patientId)
        {
            InitializeComponent();
            _patientId = patientId;

            cbDoctor.ItemsSource = _doctorService.GetAll();
        }

        private void cbDoctor_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbDoctor.SelectedItem is Doctor doc)
            {
                doctorCard.Visibility = Visibility.Visible;
                ratingPanel.Visibility = Visibility.Visible;
                btnSubmit.IsEnabled = true;

                txtDoctorName.Text = doc.FullName;
                txtDoctorDept.Text = doc.Department?.Name ?? "";

            }
        }

        private void Star_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn && int.TryParse(btn.Tag.ToString(), out int v))
            {
                selectedRating = v;
                UpdateStars();
            }
        }

        private void UpdateStars()
        {
            foreach (Button btn in LogicalTreeHelper.GetChildren(this)
                     .OfType<Button>()
                     .Where(b => b.Tag != null))
            {
                int v = int.Parse(btn.Tag.ToString());
                btn.Content = (v <= selectedRating) ? "★" : "☆";
                btn.Foreground = (v <= selectedRating) ? Brushes.Gold : Brushes.Gray;
            }
        }

        private void BtnSubmit_Click(object sender, RoutedEventArgs e)
        {
            if (cbDoctor.SelectedItem is not Doctor doc)
            {
                MessageBox.Show("Please select a doctor.");
                return;
            }

            var f = new Feedback
            {
                PatientId = _patientId,
                DoctorId = doc.DoctorId,
                Rating = selectedRating,
                Comment = txtComment.Text,
                CreatedAt = DateTime.Now
            };

            _feedbackService.Create(f);
            MessageBox.Show("Feedback submitted!");
            Close();
        }
    }
}

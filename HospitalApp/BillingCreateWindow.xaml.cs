using System;
using System.Linq;
using System.Windows;
using Business;
using Service;

namespace HospitalApp
{
    public partial class BillingCreateWindow : Window
    {
        private readonly AdmissionService _admissionService = new();
        private readonly BillingService _billingService = new();

        public BillingCreateWindow()
        {
            InitializeComponent();

            // Hiển thị Admission có dạng: "Nguyen Van A - Room 101 - Admitted on 11/11/2025"
            var list = _admissionService.GetAll()
                .Select(a => new
                {
                    a.AdmissionId,
                    DisplayInfo = $"{a.Patient.FullName} | Room {a.Room.RoomNumber} | Admitted {a.AdmissionDate:d}"
                })
                .ToList();

            cbAdmission.ItemsSource = list;
            cbAdmission.DisplayMemberPath = "DisplayInfo";
            cbAdmission.SelectedValuePath = "AdmissionId";
        }

        private void BtnCreate_Click(object sender, RoutedEventArgs e)
        {
            if (cbAdmission.SelectedValue is not int admissionId)
            {
                MessageBox.Show("Select an admission first.");
                return;
            }

            if (!decimal.TryParse(txtAmount.Text, out var amount) || amount <= 0)
            {
                MessageBox.Show("Please enter a valid amount.");
                return;
            }

            var bill = new Billing
            {
                AdmissionId = admissionId,
                TotalAmount = amount,
                Paid = false
            };

            _billingService.Add(bill);
            MessageBox.Show("Billing record created successfully!");
            DialogResult = true;
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}

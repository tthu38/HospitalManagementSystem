using System;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using Business;
using Service;


namespace HospitalApp
{
    public partial class BillingManagementWindow : UserControl
    {
        private readonly BillingService _service = new();

        public BillingManagementWindow()
        {
            InitializeComponent();
            LoadBills();
        }

        private void LoadBills()
        {
            dgBills.ItemsSource = _service.GetAll();
        }

        private void BtnRefresh_Click(object sender, RoutedEventArgs e)
        {
            LoadBills();
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            var win = new BillingCreateWindow();
            if (win.ShowDialog() == true)
                LoadBills();
        }

        private void BtnPaid_Click(object sender, RoutedEventArgs e)
        {
            if (dgBills.SelectedItem is not Billing bill)
            {
                MessageBox.Show("Please select a bill to mark as paid.");
                return;
            }

            if (bill.Paid == true)
            {
                MessageBox.Show("This bill is already paid.");
                return;
            }

            var method = Microsoft.VisualBasic.Interaction.InputBox(
                "Enter payment method (e.g. Cash, Credit Card, Bank Transfer):",
                "Payment Method", "Cash");

            if (string.IsNullOrWhiteSpace(method))
            {
                MessageBox.Show("Payment cancelled.");
                return;
            }

            _service.MarkAsPaid(bill.BillId, method);
            MessageBox.Show($"Bill marked as paid using {method}.");
            LoadBills();
        }

        private void BtnGenerate_Click(object sender, RoutedEventArgs e)
        {
            if (dgBills.SelectedItem is not Billing b)
            {
                MessageBox.Show("Please select a bill to generate.");
                return;
            }

            if (b.Admission == null)
            {
                MessageBox.Show("This bill has no related admission.");
                return;
            }

            try
            {
                string file = Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                    $"Bill_{b.BillId}_{DateTime.Now:yyyyMMdd_HHmmss}.pdf"
                );

                var doc = new PdfSharp.Pdf.PdfDocument();
                doc.Info.Title = "Hospital Billing Invoice";

                var page = doc.AddPage();
                var gfx = PdfSharp.Drawing.XGraphics.FromPdfPage(page);

                var titleFont = new PdfSharp.Drawing.XFont("Arial", 20, PdfSharp.Drawing.XFontStyle.Bold);
                var normalFont = new PdfSharp.Drawing.XFont("Arial", 12, PdfSharp.Drawing.XFontStyle.Regular);
                var grayFont = new PdfSharp.Drawing.XFont("Arial", 10, PdfSharp.Drawing.XFontStyle.Italic);

                int y = 60;
                gfx.DrawString("Hospital Billing Invoice", titleFont, PdfSharp.Drawing.XBrushes.DarkBlue, new PdfSharp.Drawing.XPoint(40, y));
                y += 40;

                gfx.DrawString($"Bill ID: {b.BillId}", normalFont, PdfSharp.Drawing.XBrushes.Black, new PdfSharp.Drawing.XPoint(40, y)); y += 25;
                gfx.DrawString($"Patient: {b.Admission.Patient.FullName}", normalFont, PdfSharp.Drawing.XBrushes.Black, new PdfSharp.Drawing.XPoint(40, y)); y += 25;
                gfx.DrawString($"Room: {b.Admission.Room.RoomNumber} ({b.Admission.Room.RoomType})", normalFont, PdfSharp.Drawing.XBrushes.Black, new PdfSharp.Drawing.XPoint(40, y)); y += 25;
                gfx.DrawString($"Admission Date: {b.Admission.AdmissionDate:d}", normalFont, PdfSharp.Drawing.XBrushes.Black, new PdfSharp.Drawing.XPoint(40, y)); y += 25;
                gfx.DrawString($"Discharge Date: {b.Admission.DischargeDate?.ToString("d") ?? "N/A"}", normalFont, PdfSharp.Drawing.XBrushes.Black, new PdfSharp.Drawing.XPoint(40, y)); y += 25;
                gfx.DrawString($"Total Amount: {b.TotalAmount:N0} ₫", normalFont, PdfSharp.Drawing.XBrushes.Black, new PdfSharp.Drawing.XPoint(40, y)); y += 25;
                gfx.DrawString($"Paid: {(b.Paid == true ? "Yes" : "No")}", normalFont, PdfSharp.Drawing.XBrushes.Black, new PdfSharp.Drawing.XPoint(40, y)); y += 25;
                gfx.DrawString($"Payment Method: {b.PaymentMethod ?? "N/A"}", normalFont, PdfSharp.Drawing.XBrushes.Black, new PdfSharp.Drawing.XPoint(40, y)); y += 25;

                y += 20;
                gfx.DrawLine(PdfSharp.Drawing.XPens.Gray, 40, y, page.Width - 40, y); y += 20;
                gfx.DrawString($"Generated on: {DateTime.Now:g}", grayFont, PdfSharp.Drawing.XBrushes.Gray, new PdfSharp.Drawing.XPoint(40, y));
                y += 40;
                gfx.DrawString("Authorized Signature: _______________________", grayFont, PdfSharp.Drawing.XBrushes.Black, new PdfSharp.Drawing.XPoint(40, y));

                doc.Save(file);
                Process.Start(new ProcessStartInfo(file) { UseShellExecute = true });

                MessageBox.Show($"Bill exported successfully to:\n{file}",
                    "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error exporting PDF:\n{ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

    }
}

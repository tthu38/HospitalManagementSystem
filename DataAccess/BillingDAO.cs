using Business;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataAccess
{
    public class BillingDAO
    {
        public List<Billing> GetAll()
        {
            using var context = new HospitalManagementContext();
            return context.Billings
                .Include(b => b.Admission)!.ThenInclude(a => a.Patient)
                .Include(b => b.Admission)!.ThenInclude(a => a.Room)
                .ToList();
        }

        public List<Billing> GetByPatient(int patientId)
        {
            using var context = new HospitalManagementContext();
            return context.Billings
                .Include(b => b.Admission)!.ThenInclude(a => a.Patient)
                .Where(b => b.Admission!.PatientId == patientId)
                .ToList();
        }

        public void Add(Billing bill)
        {
            using var context = new HospitalManagementContext();
            context.Billings.Add(bill);
            context.SaveChanges();
        }

        public void MarkAsPaid(int billId, string paymentMethod)
        {
            using var context = new HospitalManagementContext();
            var bill = context.Billings.FirstOrDefault(b => b.BillId == billId);
            if (bill == null) return;

            bill.Paid = true;
            bill.PaymentMethod = paymentMethod;
            bill.PaymentDate = DateTime.Now; // ✅ đổi DateOnly -> DateTime
            context.SaveChanges();
        }

        public Billing? GenerateForAdmission(int admissionId)
        {
            using var context = new HospitalManagementContext();

            // 1️⃣ Lấy admission kèm room & patient
            var admission = context.Admissions
                .Include(a => a.Room)
                .Include(a => a.Patient)
                .FirstOrDefault(a => a.AdmissionId == admissionId);

            // 2️⃣ Kiểm tra hợp lệ
            if (admission == null || admission.DischargeDate == null)
                return null;

            // 3️⃣ Tính số ngày nằm viện
            int days = (admission.DischargeDate.Value - admission.AdmissionDate).Days;
            if (days <= 0) days = 1;

            // 4️⃣ Tính giá phòng
            decimal rate = admission.Room?.RoomType switch
            {
                "VIP" => 1_000_000m,
                "Single" => 500_000m,
                "Double" => 300_000m,
                _ => 400_000m
            };

            decimal total = days * rate;

            // 5️⃣ Nếu đã có bill cho admission này -> cập nhật
            var existing = context.Billings
                .FirstOrDefault(b => b.AdmissionId == admission.AdmissionId);

            if (existing != null)
            {
                existing.TotalAmount = total;
                existing.Paid = false;
                existing.PaymentMethod = null;
                existing.PaymentDate = null;

                context.SaveChanges();
                return existing;
            }

            // 6️⃣ Nếu chưa có -> tạo mới
            var bill = new Billing
            {
                AdmissionId = admission.AdmissionId,
                TotalAmount = total,
                Paid = false
            };

            context.Billings.Add(bill);
            context.SaveChanges();

            return bill;
        }
    }
}

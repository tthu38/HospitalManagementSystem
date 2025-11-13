using System.Collections.Generic;
using Business;
using Repository;

namespace Service
{
    public class BillingService
    {
        private readonly IBillingRepository repo;

        public BillingService()
        {
            repo = new BillingRepository();
        }

        /// <summary>
        /// Lấy toàn bộ hóa đơn trong hệ thống (cho admin).
        /// </summary>
        public List<Billing> GetAll() => repo.GetAll();

        /// <summary>
        /// Lấy danh sách hóa đơn của 1 bệnh nhân.
        /// </summary>
        public List<Billing> GetByPatient(int patientId) => repo.GetByPatient(patientId);

        /// <summary>
        /// ✅ Thêm hóa đơn mới (khi admin hoặc hệ thống tạo bill thủ công)
        /// </summary>
        public void Add(Billing billing)
        {
            repo.Add(billing);
        }

        /// <summary>
        /// Đánh dấu hóa đơn đã thanh toán.
        /// </summary>
        public void MarkAsPaid(int billId, string method)
        {
            repo.MarkAsPaid(billId, method);
        }

        /// <summary>
        /// ✅ Sinh hóa đơn tự động khi bệnh nhân xuất viện.
        /// </summary>
        public Billing? GenerateBill(int admissionId)
        {
            return repo.GenerateForAdmission(admissionId);
        }
    }
}

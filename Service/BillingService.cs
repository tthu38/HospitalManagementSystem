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

        public List<Billing> GetAll() => repo.GetAll();

        public List<Billing> GetByPatient(int patientId) => repo.GetByPatient(patientId);

        public void Add(Billing billing)
        {
            repo.Add(billing);
        }
        public void MarkAsPaid(int billId, string method)
        {
            repo.MarkAsPaid(billId, method);
        }

        public Billing? GenerateBill(int admissionId)
        {
            return repo.GenerateForAdmission(admissionId);
        }
    }
}

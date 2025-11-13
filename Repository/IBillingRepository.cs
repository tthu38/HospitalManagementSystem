using System.Collections.Generic;
using Business;

namespace Repository
{
    public interface IBillingRepository
    {
        List<Billing> GetAll();
        List<Billing> GetByPatient(int patientId);
        void Add(Billing billing);
        void MarkAsPaid(int billId, string paymentMethod);
        Billing? GenerateForAdmission(int admissionId);
    }
}

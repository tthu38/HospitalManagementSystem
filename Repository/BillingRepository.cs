using System.Collections.Generic;
using Business;
using DataAccess;

namespace Repository
{
    public class BillingRepository : IBillingRepository
    {
        private readonly BillingDAO dao = new();

        public List<Billing> GetAll() => dao.GetAll();
        public List<Billing> GetByPatient(int patientId) => dao.GetByPatient(patientId);
        public void Add(Billing billing) => dao.Add(billing);
        public void MarkAsPaid(int id, string method) => dao.MarkAsPaid(id, method);
        public Billing? GenerateForAdmission(int admissionId) => dao.GenerateForAdmission(admissionId);
    }
}

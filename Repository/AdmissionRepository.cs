using System.Collections.Generic;
using Business;
using DataAccess;

namespace Repository
{
    public class AdmissionRepository : IAdmissionRepository
    {
        private readonly AdmissionDAO dao = AdmissionDAO.Instance;
        public List<Admission> GetAll() => dao.GetAll();
        public Admission? GetById(int id) => dao.GetById(id);
        public void Add(Admission admission) => dao.Add(admission);
        public void Discharge(int id) => dao.Discharge(id);
    }
}

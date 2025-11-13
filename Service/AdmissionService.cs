using System.Collections.Generic;
using Business;
using Repository;

namespace Service
{
    public class AdmissionService
    {
        private readonly IAdmissionRepository repo;

        public AdmissionService()
        {
            repo = new AdmissionRepository();
        }

        public List<Admission> GetAll() => repo.GetAll();

        public Admission? GetById(int id) => repo.GetById(id);

        public void Create(Admission admission)
        {
            repo.Add(admission);
        }

        public void Discharge(int admissionId)
        {
            repo.Discharge(admissionId);
        }
    }
}

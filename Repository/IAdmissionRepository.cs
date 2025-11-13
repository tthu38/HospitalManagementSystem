using System.Collections.Generic;
using Business;

namespace Repository
{
    public interface IAdmissionRepository
    {
        List<Admission> GetAll();
        Admission? GetById(int id);
        void Add(Admission admission);
        void Discharge(int admissionId);
    }
}

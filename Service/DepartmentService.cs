using System.Collections.Generic;
using Business;
using Repository;

namespace Service
{
    public class DepartmentService
    {
        private readonly IDepartmentRepository repo;

        public DepartmentService()
        {
            repo = new DepartmentRepository();
        }

        public List<Department> GetAll()
        {
            return repo.GetAll();
        }
    }
}

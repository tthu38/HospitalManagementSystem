using System.Collections.Generic;
using Business;
using DataAccess;

namespace Repository
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly DepartmentDAO dao = new();
        public List<Department> GetAll() => dao.GetAll();
    }
}

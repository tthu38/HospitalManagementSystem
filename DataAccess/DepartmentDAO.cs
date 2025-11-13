using System.Collections.Generic;
using System.Linq;
using Business;

namespace DataAccess
{
    public class DepartmentDAO
    {
        public List<Department> GetAll()
        {
            using var context = new HospitalManagementContext();
            return context.Departments.OrderBy(d => d.Name).ToList();
        }
    }
}

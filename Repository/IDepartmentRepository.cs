using System.Collections.Generic;
using Business;

namespace Repository
{
    public interface IDepartmentRepository
    {
        List<Department> GetAll();
    }
}

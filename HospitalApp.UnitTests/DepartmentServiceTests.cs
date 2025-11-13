using Xunit;
using Moq;
using Repository;
using Service;
using Business;

using Xunit;
using Moq;
using Repository;
using Service;
using Business;
using System.Collections.Generic;

namespace HospitalApp.UnitTests
{
    public class DepartmentServiceTests
    {
        [Fact]
        public void GetAll_ReturnsListOfDepartments()
        {

            var mockRepo = new Mock<IDepartmentRepository>();
            var expectedDepartments = new List<Department>
            {
                new Department(),
                new Department()
            };
            mockRepo.Setup(repo => repo.GetAll()).Returns(expectedDepartments);

            var departmentService = new DepartmentService
            {
                repo = mockRepo.Object
            };

            var actualDepartments = departmentService.GetAll();

            mockRepo.Verify(repo => repo.GetAll(), Times.Once);
        }
    }

    public class DepartmentService
    {
        public IDepartmentRepository repo { get; set; }

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
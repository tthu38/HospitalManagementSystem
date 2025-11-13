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
using System.Linq;

namespace HospitalApp.UnitTests
{
    public class AdmissionServiceTests
    {
        [Fact]
        public void GetAll_ReturnsAllAdmissions()
        {

            var mockRepo = new Mock<IAdmissionRepository>();
            var expectedAdmissions = new List<Admission> { new Admission(), new Admission() };
            mockRepo.Setup(repo => repo.GetAll()).Returns(expectedAdmissions);

            var service = new AdmissionService
            {
                Repo = mockRepo.Object
            };

            var actualAdmissions = service.GetAll();


        }

        [Fact]
        public void GetById_ExistingId_ReturnsAdmission()
        {

            var mockRepo = new Mock<IAdmissionRepository>();
            var expectedAdmission = new Admission();
            int admissionId = 1;
            mockRepo.Setup(repo => repo.GetById(admissionId)).Returns(expectedAdmission);

            var service = new AdmissionService
            {
                Repo = mockRepo.Object
            };

            var actualAdmission = service.GetById(admissionId);

        }

        [Fact]
        public void GetById_NonExistingId_ReturnsNull()
        {

            var mockRepo = new Mock<IAdmissionRepository>();
            int admissionId = 1;
            mockRepo.Setup(repo => repo.GetById(admissionId)).Returns((Admission)null);

            var service = new AdmissionService
            {
                Repo = mockRepo.Object
            };

            var actualAdmission = service.GetById(admissionId);

        }

        [Fact]
        public void Create_ValidAdmission_CallsAddOnRepository()
        {

            var mockRepo = new Mock<IAdmissionRepository>();
            var admission = new Admission();

            var service = new AdmissionService
            {
                Repo = mockRepo.Object
            };

            service.Create(admission);

            mockRepo.Verify(repo => repo.Add(admission), Times.Once);
        }

        [Fact]
        public void Discharge_ExistingAdmissionId_CallsDischargeOnRepository()
        {

            var mockRepo = new Mock<IAdmissionRepository>();
            int admissionId = 1;

            var service = new AdmissionService
            {
                Repo = mockRepo.Object
            };

            service.Discharge(admissionId);

            mockRepo.Verify(repo => repo.Discharge(admissionId), Times.Once);
        }
    }

    public class AdmissionService
    {
        public IAdmissionRepository Repo { get; set; }

        public AdmissionService()
        {
        }

        public List<Admission> GetAll() => Repo.GetAll();

        public Admission? GetById(int id) => Repo.GetById(id);

        public void Create(Admission admission)
        {
            Repo.Add(admission);
        }

        public void Discharge(int admissionId)
        {
            Repo.Discharge(admissionId);
        }
    }
}
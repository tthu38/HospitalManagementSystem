using Xunit;
using Moq;
using Repository;
using Service;
using Business;
using System.Collections.Generic;
using System.Reflection;

namespace HospitalApp.UnitTests
{
    public class DoctorServiceTests
    {
        private DoctorService CreateServiceWithMock(Mock<IDoctorRepository> mockRepo)
        {
            var service = new DoctorService();
            typeof(DoctorService)
                .GetField("repo", BindingFlags.NonPublic | BindingFlags.Instance)
                ?.SetValue(service, mockRepo.Object);
            return service;
        }

        [Fact]
        public void GetAll_ReturnsListOfDoctors()
        {
            var mockRepo = new Mock<IDoctorRepository>();
            mockRepo.Setup(r => r.GetAll()).Returns(new List<Doctor> { new Doctor(), new Doctor() });

            var service = CreateServiceWithMock(mockRepo);

            var result = service.GetAll();

            mockRepo.Verify(r => r.GetAll(), Times.Once);
        }

        [Fact]
        public void GetById_ExistingId_ReturnsDoctor()
        {
            var mockRepo = new Mock<IDoctorRepository>();
            mockRepo.Setup(r => r.GetById(1)).Returns(new Doctor());

            var service = CreateServiceWithMock(mockRepo);
            var result = service.GetById(1);

            mockRepo.Verify(r => r.GetById(1), Times.Once);
        }

        [Fact]
        public void Create_ValidDoctor_CallsAddOnRepository()
        {
            var mockRepo = new Mock<IDoctorRepository>();
            var service = CreateServiceWithMock(mockRepo);
            var doctor = new Doctor();

            service.Create(doctor);

            mockRepo.Verify(r => r.Add(doctor), Times.Once);
        }

        [Fact]
        public void Update_ValidDoctor_CallsUpdateOnRepository()
        {
            var mockRepo = new Mock<IDoctorRepository>();
            var service = CreateServiceWithMock(mockRepo);
            var doctor = new Doctor();

            service.Update(doctor);

            mockRepo.Verify(r => r.Update(doctor), Times.Once);
        }

        [Fact]
        public void SetActive_CallsSetActiveOnRepository()
        {
            var mockRepo = new Mock<IDoctorRepository>();
            var service = CreateServiceWithMock(mockRepo);

            service.SetActive(1, true);

            mockRepo.Verify(r => r.SetActive(1, true), Times.Once);
        }

        [Fact]
        public void Login_ValidCredentials_ReturnsDoctor()
        {
            var mockRepo = new Mock<IDoctorRepository>();
            mockRepo.Setup(r => r.Login("user", "pass")).Returns(new Doctor());

            var service = CreateServiceWithMock(mockRepo);
            var result = service.Login("user", "pass");

            mockRepo.Verify(r => r.Login("user", "pass"), Times.Once);
        }
    }
}

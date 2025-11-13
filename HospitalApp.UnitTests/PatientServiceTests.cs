using Xunit;
using Moq;
using Repository;
using Service;
using Business;
using System.Collections.Generic;
using System.Reflection;

namespace HospitalApp.UnitTests
{
    public class PatientServiceTests
    {
        private PatientService CreateServiceWithMock(Mock<IPatientRepository> mockRepo)
        {
            var service = new PatientService();
            typeof(PatientService)
                .GetField("repo", BindingFlags.NonPublic | BindingFlags.Instance)
                ?.SetValue(service, mockRepo.Object);
            return service;
        }

        [Fact]
        public void Register_CallsAddOnRepository()
        {
            var mockRepo = new Mock<IPatientRepository>();
            var service = CreateServiceWithMock(mockRepo);
            var patient = new Patient();

            service.Register(patient);

            mockRepo.Verify(r => r.Add(patient), Times.Once);
        }

        [Fact]
        public void Update_CallsUpdateOnRepository()
        {
            var mockRepo = new Mock<IPatientRepository>();
            var service = CreateServiceWithMock(mockRepo);
            var patient = new Patient();

            service.Update(patient);

            mockRepo.Verify(r => r.Update(patient), Times.Once);
        }

        [Fact]
        public void SetActive_CallsSetActiveOnRepository()
        {
            var mockRepo = new Mock<IPatientRepository>();
            var service = CreateServiceWithMock(mockRepo);

            service.SetActive(1, true);

            mockRepo.Verify(r => r.SetActive(1, true), Times.Once);
        }

        [Fact]
        public void Login_ValidCredentials_ReturnsPatient()
        {
            var mockRepo = new Mock<IPatientRepository>();
            mockRepo.Setup(r => r.Login("a", "b")).Returns(new Patient());

            var service = CreateServiceWithMock(mockRepo);
            var result = service.Login("a", "b");

            mockRepo.Verify(r => r.Login("a", "b"), Times.Once);
        }
    }
}

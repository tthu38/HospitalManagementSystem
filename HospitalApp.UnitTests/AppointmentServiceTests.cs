using Xunit;
using Moq;
using Repository;
using Service;
using Business;
using System.Reflection;

namespace HospitalApp.UnitTests
{
    public class AppointmentServiceTests
    {
        private AppointmentService CreateServiceWithMock(Mock<IAppointmentRepository> mockRepo)
        {
            var service = new AppointmentService();
            typeof(AppointmentService)
                .GetField("repo", BindingFlags.NonPublic | BindingFlags.Instance)
                ?.SetValue(service, mockRepo.Object);
            return service;
        }

        [Fact]
        public void Add_ValidAppointment_CallsRepositoryAdd()
        {
            var mockRepo = new Mock<IAppointmentRepository>();
            var service = CreateServiceWithMock(mockRepo);
            var a = new Appointment();

            service.Add(a);

            mockRepo.Verify(r => r.Add(a), Times.Once);
        }

        [Fact]
        public void Confirm_AppointmentId_CallsUpdateStatus()
        {
            var mockRepo = new Mock<IAppointmentRepository>();
            var service = CreateServiceWithMock(mockRepo);

            service.Confirm(1);

            mockRepo.Verify(r => r.UpdateStatus(1, "Confirmed"), Times.Once);
        }

        [Fact]
        public void Cancel_AppointmentId_CallsUpdateStatus()
        {
            var mockRepo = new Mock<IAppointmentRepository>();
            var service = CreateServiceWithMock(mockRepo);

            service.Cancel(1);

            mockRepo.Verify(r => r.UpdateStatus(1, "Cancelled"), Times.Once);
        }
    }
}

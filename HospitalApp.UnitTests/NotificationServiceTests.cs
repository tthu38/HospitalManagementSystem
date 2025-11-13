using Xunit;
using Moq;
using Repository;
using Service;
using Business;
using System.Collections.Generic;
using System.Reflection;

namespace HospitalApp.UnitTests
{
    public class NotificationServiceTests
    {
        private NotificationService CreateServiceWithMock(Mock<INotificationRepository> mockRepo)
        {
            var service = new NotificationService();
            typeof(NotificationService)
                .GetField("repo", BindingFlags.NonPublic | BindingFlags.Instance)
                ?.SetValue(service, mockRepo.Object);
            return service;
        }

        [Fact]
        public void Create_ValidNotification_CallsAdd()
        {
            var mockRepo = new Mock<INotificationRepository>();
            var service = CreateServiceWithMock(mockRepo);
            var notification = new Notification();

            service.Create(notification);

            mockRepo.Verify(r => r.Add(notification), Times.Once);
        }

        [Fact]
        public void MarkAsRead_CallsRepoOnce()
        {
            var mockRepo = new Mock<INotificationRepository>();
            var service = CreateServiceWithMock(mockRepo);

            service.MarkAsRead(1);

            mockRepo.Verify(r => r.MarkAsRead(1), Times.Once);
        }
    }
}

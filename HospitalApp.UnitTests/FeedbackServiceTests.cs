using Xunit;
using Moq;
using Repository;
using Service;
using Business;
using System.Collections.Generic;
using System.Reflection;

namespace HospitalApp.UnitTests
{
    public class FeedbackServiceTests
    {
        private FeedbackService CreateServiceWithMock(Mock<IFeedbackRepository> mockRepo)
        {
            var service = new FeedbackService();
            typeof(FeedbackService)
                .GetField("repo", BindingFlags.NonPublic | BindingFlags.Instance)
                ?.SetValue(service, mockRepo.Object);
            return service;
        }

        [Fact]
        public void Create_ValidFeedback_CallsAdd()
        {
            var mockRepo = new Mock<IFeedbackRepository>();
            var service = CreateServiceWithMock(mockRepo);
            var feedback = new Feedback();

            service.Create(feedback);

            mockRepo.Verify(r => r.Add(feedback), Times.Once);
        }

        [Fact]
        public void GetAll_ReturnsList()
        {
            var mockRepo = new Mock<IFeedbackRepository>();
            mockRepo.Setup(r => r.GetAll()).Returns(new List<Feedback>());
            var service = CreateServiceWithMock(mockRepo);

            var result = service.GetAll();

            mockRepo.Verify(r => r.GetAll(), Times.Once);
        }
    }
}

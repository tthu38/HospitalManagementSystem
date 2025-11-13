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

namespace HospitalApp.UnitTests
{
    public class AdminServiceTests
    {
        [Fact]
        public void Login_ValidCredentials_ReturnsAdmin()
        {

            var mockRepo = new Mock<IAdminRepository>();
            var expectedAdmin = new Admin();
            mockRepo.Setup(repo => repo.Login("testuser", "testpass")).Returns(expectedAdmin);

            var adminService = new AdminService();
            var fieldInfo = adminService.GetType().GetField("repo", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            fieldInfo?.SetValue(adminService, mockRepo.Object);

            var result = adminService.Login("testuser", "testpass");

        }

        [Fact]
        public void Login_InvalidCredentials_ReturnsNull()
        {

            var mockRepo = new Mock<IAdminRepository>();
            mockRepo.Setup(repo => repo.Login("testuser", "wrongpass")).Returns((Admin)null);

            var adminService = new AdminService();
            var fieldInfo = adminService.GetType().GetField("repo", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            fieldInfo?.SetValue(adminService, mockRepo.Object);

            var result = adminService.Login("testuser", "wrongpass");

        }

        [Fact]
        public void Add_ValidAdmin_CallsRepositoryAdd()
        {

            var mockRepo = new Mock<IAdminRepository>();
            var adminToAdd = new Admin();

            var adminService = new AdminService();
            var fieldInfo = adminService.GetType().GetField("repo", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            fieldInfo?.SetValue(adminService, mockRepo.Object);

            adminService.Add(adminToAdd);

            mockRepo.Verify(repo => repo.Add(adminToAdd), Times.Once);
        }
    }
}
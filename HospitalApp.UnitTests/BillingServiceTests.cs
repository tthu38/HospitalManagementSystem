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
    public class BillingServiceTests
    {
        [Fact]
        public void GetAll_ReturnsAllBillingsFromRepository()
        {

            var mockRepo = new Mock<IBillingRepository>();
            var expectedBillings = new List<Billing> { new Billing(), new Billing() };
            mockRepo.Setup(repo => repo.GetAll()).Returns(expectedBillings);

            var billingService = new BillingService();
            var fieldInfo = billingService.GetType().GetField("repo", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            fieldInfo.SetValue(billingService, mockRepo.Object);

            var actualBillings = billingService.GetAll();

            mockRepo.Verify(repo => repo.GetAll(), Times.Once);
        }

        [Fact]
        public void GetByPatient_ReturnsBillingsForPatientFromRepository()
        {

            var mockRepo = new Mock<IBillingRepository>();
            int patientId = 123;
            var expectedBillings = new List<Billing> { new Billing(), new Billing() };
            mockRepo.Setup(repo => repo.GetByPatient(patientId)).Returns(expectedBillings);

            var billingService = new BillingService();
            var fieldInfo = billingService.GetType().GetField("repo", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            fieldInfo.SetValue(billingService, mockRepo.Object);

            var actualBillings = billingService.GetByPatient(patientId);

            mockRepo.Verify(repo => repo.GetByPatient(patientId), Times.Once);
        }

        [Fact]
        public void Add_CallsAddOnRepository()
        {

            var mockRepo = new Mock<IBillingRepository>();
            var billing = new Billing();

            var billingService = new BillingService();
            var fieldInfo = billingService.GetType().GetField("repo", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            fieldInfo.SetValue(billingService, mockRepo.Object);

            billingService.Add(billing);

            mockRepo.Verify(repo => repo.Add(billing), Times.Once);
        }

        [Fact]
        public void MarkAsPaid_CallsMarkAsPaidOnRepository()
        {

            var mockRepo = new Mock<IBillingRepository>();
            int billId = 456;
            string method = "Credit Card";

            var billingService = new BillingService();
            var fieldInfo = billingService.GetType().GetField("repo", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            fieldInfo.SetValue(billingService, mockRepo.Object);

            billingService.MarkAsPaid(billId, method);

            mockRepo.Verify(repo => repo.MarkAsPaid(billId, method), Times.Once);
        }

        [Fact]
        public void GenerateBill_CallsGenerateForAdmissionOnRepository()
        {

            var mockRepo = new Mock<IBillingRepository>();
            int admissionId = 789;
            var expectedBilling = new Billing();
            mockRepo.Setup(repo => repo.GenerateForAdmission(admissionId)).Returns(expectedBilling);

            var billingService = new BillingService();
            var fieldInfo = billingService.GetType().GetField("repo", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            fieldInfo.SetValue(billingService, mockRepo.Object);

            var actualBilling = billingService.GenerateBill(admissionId);

            mockRepo.Verify(repo => repo.GenerateForAdmission(admissionId), Times.Once);
        }

        [Fact]
        public void GenerateBill_ReturnsNullWhenRepositoryReturnsNull()
        {

            var mockRepo = new Mock<IBillingRepository>();
            int admissionId = 789;
            mockRepo.Setup(repo => repo.GenerateForAdmission(admissionId)).Returns((Billing)null);

            var billingService = new BillingService();
            var fieldInfo = billingService.GetType().GetField("repo", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            fieldInfo.SetValue(billingService, mockRepo.Object);

            var actualBilling = billingService.GenerateBill(admissionId);

            mockRepo.Verify(repo => repo.GenerateForAdmission(admissionId), Times.Once);
        }
    }
}
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using MetroProject.API.Controllers;
using MetroProject.Application.DTOs;
using MetroProject.Domain.Interface;

namespace MetroProject.API.Tests.Controllers
{
    [TestFixture]
    public class PaymentsControllerTests
    {
        private Mock<ILogger<PaymentsController>> loggerMock;
        private Mock<IRepository<PaymentDTO>> repositoryMock;
        private PaymentsController controller;

        [SetUp]
        public void SetUp()
        {
            loggerMock = new Mock<ILogger<PaymentsController>>();
            repositoryMock = new Mock<IRepository<PaymentDTO>>();
            controller = new PaymentsController(loggerMock.Object, repositoryMock.Object);
        }

        [Test]
        public void Get_ReturnsOkWithPayments()
        {
            // Arrange
            var payments = new List<PaymentDTO>
            {
                new PaymentDTO { Id = 1, CustomerId = 1, Amount = 10, PaymentMethod = "Cash", TransactionId = "T1", PaymentDate = DateTime.Now, CreatedOn = DateTime.Now, UpdatedAt = DateTime.Now },
                new PaymentDTO { Id = 2, CustomerId = 2, Amount = 20, PaymentMethod = "Card", TransactionId = "T2", PaymentDate = DateTime.Now, CreatedOn = DateTime.Now, UpdatedAt = DateTime.Now }
            };
            repositoryMock.Setup(r => r.Get()).Returns(payments);

            // Act
            var result = controller.Get();

            // Assert
            Assert.That(result.Result, Is.InstanceOf<OkObjectResult>());
            var okResult = result.Result as OkObjectResult;
            Assert.That(okResult!.Value, Is.EqualTo(payments));
        }

        [Test]
        public void GetById_ExistingId_ReturnsOk()
        {
            // Arrange
            var payment = new PaymentDTO { Id = 1, CustomerId = 1, Amount = 10, PaymentMethod = "Cash", TransactionId = "T1", PaymentDate = DateTime.Now, CreatedOn = DateTime.Now, UpdatedAt = DateTime.Now };
            repositoryMock.Setup(r => r.Get()).Returns(new List<PaymentDTO> { payment });

            // Act
            var result = controller.GetById(1);

            // Assert
            Assert.That(result.Result, Is.InstanceOf<OkObjectResult>());
            var okResult = result.Result as OkObjectResult;
            Assert.That(okResult!.Value, Is.EqualTo(payment));
        }

        [Test]
        public void GetById_NonExistingId_ReturnsNotFound()
        {
            // Arrange
            repositoryMock.Setup(r => r.Get()).Returns(new List<PaymentDTO>());

            // Act
            var result = controller.GetById(99);

            // Assert
            Assert.That(result.Result, Is.InstanceOf<NotFoundResult>());
        }

        [Test]
        public void Create_ValidModel_ReturnsCreatedAtAction()
        {
            // Arrange
            var payment = new PaymentDTO { Id = 1, CustomerId = 1, Amount = 10, PaymentMethod = "Cash", TransactionId = "T1", PaymentDate = DateTime.Now, CreatedOn = DateTime.Now, UpdatedAt = DateTime.Now };
            repositoryMock.Setup(r => r.Create(It.IsAny<PaymentDTO>())).Returns(payment);

            // Act
            var result = controller.Create(payment);

            // Assert
            Assert.That(result.Result, Is.InstanceOf<CreatedAtActionResult>());
            var createdResult = result.Result as CreatedAtActionResult;
            Assert.That(createdResult!.Value, Is.EqualTo(payment));
        }

        [Test]
        public void Create_InvalidModel_ReturnsBadRequest()
        {
            // Arrange
            controller.ModelState.AddModelError("Amount", "Required");
            var payment = new PaymentDTO();

            // Act
            var result = controller.Create(payment);

            // Assert
            Assert.That(result.Result, Is.InstanceOf<BadRequestObjectResult>());
        }

        [Test]
        public void Update_ValidModel_ReturnsOk()
        {
            // Arrange
            var payment = new PaymentDTO { Id = 1, CustomerId = 1, Amount = 10, PaymentMethod = "Cash", TransactionId = "T1", PaymentDate = DateTime.Now, CreatedOn = DateTime.Now, UpdatedAt = DateTime.Now };
            repositoryMock.Setup(r => r.Update(It.IsAny<PaymentDTO>())).Returns(payment);

            // Act
            var result = controller.Update(1, payment);

            // Assert
            Assert.That(result.Result, Is.InstanceOf<OkObjectResult>());
            var okResult = result.Result as OkObjectResult;
            Assert.That(okResult!.Value, Is.EqualTo(payment));
        }

        [Test]
        public void Update_InvalidModel_ReturnsBadRequest()
        {
            // Arrange
            controller.ModelState.AddModelError("Amount", "Required");
            var payment = new PaymentDTO();

            // Act
            var result = controller.Update(1, payment);

            // Assert
            Assert.That(result.Result, Is.InstanceOf<BadRequestObjectResult>());
        }

        [Test]
        public void Delete_ExistingId_ReturnsNoContent()
        {
            // Arrange
            repositoryMock.Setup(r => r.Delete(1)).Returns(true);

            // Act
            var result = controller.Delete(1);

            // Assert
            Assert.That(result, Is.InstanceOf<NoContentResult>());
        }

        [Test]
        public void Delete_NonExistingId_ReturnsNotFound()
        {
            // Arrange
            repositoryMock.Setup(r => r.Delete(99)).Returns(false);

            // Act
            var result = controller.Delete(99);

            // Assert
            Assert.That(result, Is.InstanceOf<NotFoundResult>());
        }
    }
}
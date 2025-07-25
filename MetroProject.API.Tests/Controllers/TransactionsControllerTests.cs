using MetroProject.API.Controllers;
using MetroProject.Application.Repositories;
using MetroProject.Domain;
using MetroProject.Domain.DTOs;
using MetroProject.Domain.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;

namespace MetroProject.API.Tests.Controllers
{
    [TestFixture]
    public class TransactionsControllerTests
    {
        private Mock<ILogger<TransactionsController>> loggerMock;
        private TransactionsController controller;
        private Mock<IRepository<CheckoutCommandDTO>> repositoryMock;

        [SetUp]
        public void SetUp()
        {
            loggerMock = new Mock<ILogger<TransactionsController>>();
            repositoryMock = new Mock<IRepository<CheckoutCommandDTO>>();
            controller = new TransactionsController(loggerMock.Object, repositoryMock.Object);
        }

        [Test]
        public void Post_InvalidModel_ReturnsBadRequest()
        {
            // Arrange
            controller.ModelState.AddModelError("CustomerId", "Required");
            var dto = new CheckoutCommandDTO();

            // Act
            var result = controller.Post(dto);

            // Assert
            Assert.That(result.Result, Is.InstanceOf<BadRequestObjectResult>());
        }

        [Test]
        public void Post_ValidModel_ReturnsOk()
        {
            // Arrange
            var dto = new CheckoutCommandDTO
            {
                Id = 1,
                CustomerId = 1,
                Timestamp = DateTime.UtcNow,
                ArticlesQuantity = new[] { new ArticleQuantiy { ArticleId = 1, Quantity = 1 } },
                PaymentIds = new List<int> { 1 }
            };

            repositoryMock.Setup(r => r.Create(It.IsAny<CheckoutCommandDTO>())).Returns(dto);
            controller = new TransactionsController(loggerMock.Object, repositoryMock.Object);

            // Act
            var result = controller.Post(dto);

            // Assert
            var okResult = result.Result as OkObjectResult;
            Assert.That(okResult, Is.Not.Null);
            Assert.That(okResult.Value, Is.EqualTo(dto));
        }

        [Test]
        public void Post_ExceptionThrown_ReturnsInternalServerError()
        {
            // Arrange
            var dto = new CheckoutCommandDTO
            {
                Id = 1,
                CustomerId = 1,
                Timestamp = DateTime.UtcNow,
                ArticlesQuantity = new[] { new ArticleQuantiy { ArticleId = 1, Quantity = 1 } },
                PaymentIds = new List<int> { 1 }
            };

            repositoryMock.Setup(r => r.Create(It.IsAny<CheckoutCommandDTO>())).Throws(new Exception("Test exception"));
            controller = new TransactionsController(loggerMock.Object, repositoryMock.Object);

            // Act
            var result = controller.Post(dto);

            // Assert
            var objectResult = result.Result as ObjectResult;
            Assert.That(objectResult, Is.Not.Null);
            Assert.That(objectResult!.StatusCode, Is.EqualTo(500));
            Assert.That(objectResult.Value?.ToString(), Does.Contain("Test exception"));
        }
    }
}
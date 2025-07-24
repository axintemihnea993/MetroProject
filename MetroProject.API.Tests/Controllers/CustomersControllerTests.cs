using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using MetroProject.Application.DTOs;
using MetroProject.Domain.Interface;
using MetroProject.API.Controllers;

namespace MetroProject.API.Tests.Controllers
{
    [TestFixture]
    public class CustomersControllerTests
    {
        private Mock<IRepository<CustomerDTO>> repositoryMock;
        private Mock<ILogger<CustomersController>> loggerMock;
        private CustomersController controller;

        [SetUp]
        public void SetUp()
        {
            repositoryMock = new Mock<IRepository<CustomerDTO>>();
            loggerMock = new Mock<ILogger<CustomersController>>();
            controller = new CustomersController(loggerMock.Object, repositoryMock.Object);
        }

        [Test]
        public void Get_ReturnsOkWithCustomers()
        {
            // Arrange
            var customers = new List<CustomerDTO>
            {
                new CustomerDTO { Id = 1, Name = "Alice", Email = "alice@example.com" },
                new CustomerDTO { Id = 2, Name = "Bob", Email = "bob@example.com" }
            };
            repositoryMock.Setup(r => r.Get()).Returns(customers);

            // Act
            var result = controller.Get();

            // Assert
            Assert.That(result.Result, Is.InstanceOf<OkObjectResult>());
            var okResult = result.Result as OkObjectResult;
            Assert.That(okResult!.Value, Is.EqualTo(customers));
        }

        [Test]
        public void GetById_ExistingId_ReturnsOk()
        {
            // Arrange
            var customer = new CustomerDTO { Id = 1, Name = "Alice", Email = "alice@example.com" };
            repositoryMock.Setup(r => r.Get()).Returns(new List<CustomerDTO> { customer });

            // Act
            var result = controller.GetById(1);

            // Assert
            Assert.That(result.Result, Is.InstanceOf<OkObjectResult>());
            var okResult = result.Result as OkObjectResult;
            Assert.That(okResult!.Value, Is.EqualTo(customer));
        }

        [Test]
        public void GetById_NonExistingId_ReturnsNotFound()
        {
            // Arrange
            repositoryMock.Setup(r => r.Get()).Returns(new List<CustomerDTO>());

            // Act
            var result = controller.GetById(99);

            // Assert
            Assert.That(result.Result, Is.InstanceOf<NotFoundResult>());
        }

        [Test]
        public void Create_ValidModel_ReturnsCreatedAtAction()
        {
            // Arrange
            var customer = new CustomerDTO { Id = 1, Name = "Alice", Email = "alice@example.com" };
            repositoryMock.Setup(r => r.Create(It.IsAny<CustomerDTO>())).Returns(customer);

            // Act
            var result = controller.Create(customer);

            // Assert
            Assert.That(result.Result, Is.InstanceOf<CreatedAtActionResult>());
            var createdResult = result.Result as CreatedAtActionResult;
            Assert.That(createdResult!.Value, Is.EqualTo(customer));
        }

        [Test]
        public void Create_InvalidModel_ReturnsBadRequest()
        {
            // Arrange
            controller.ModelState.AddModelError("Name", "Required");
            var customer = new CustomerDTO();

            // Act
            var result = controller.Create(customer);

            // Assert
            Assert.That(result.Result, Is.InstanceOf<BadRequestObjectResult>());
        }

        [Test]
        public void Update_ValidModel_ReturnsOk()
        {
            // Arrange
            var customer = new CustomerDTO { Id = 1, Name = "Alice", Email = "alice@example.com" };
            repositoryMock.Setup(r => r.Update(It.IsAny<CustomerDTO>())).Returns(customer);

            // Act
            var result = controller.Update(customer);

            // Assert
            Assert.That(result.Result, Is.InstanceOf<OkObjectResult>());
            var okResult = result.Result as OkObjectResult;
            Assert.That(okResult!.Value, Is.EqualTo(customer));
        }

        [Test]
        public void Update_InvalidModel_ReturnsBadRequest()
        {
            // Arrange
            controller.ModelState.AddModelError("Name", "Required");
            var customer = new CustomerDTO();

            // Act
            var result = controller.Update(customer);

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
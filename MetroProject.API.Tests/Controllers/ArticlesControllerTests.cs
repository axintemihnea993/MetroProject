using System;
using System.Collections.Generic;
using System.Linq;
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
    public class ArticlesControllerTests
    {
        private Mock<IRepository<ArticleDTO>> repositoryMock;
        private Mock<ILogger<ArticlesController>> loggerMock;
        private ArticlesController controller;

        [SetUp]
        public void SetUp()
        {
            repositoryMock = new Mock<IRepository<ArticleDTO>>();
            loggerMock = new Mock<ILogger<ArticlesController>>();
            controller = new ArticlesController(loggerMock.Object, repositoryMock.Object);
        }

        [Test]
        public void Get_ReturnsOkWithArticles()
        {
            // Arrange
            var articles = new List<ArticleDTO>
            {
                new ArticleDTO { Id = 1, Name = "A", Price = 10, Stock = 5 },
                new ArticleDTO { Id = 2, Name = "B", Price = 20, Stock = 10 }
            };
            repositoryMock.Setup(r => r.Get()).Returns(articles);

            // Act
            var result = controller.Get();

            // Assert
            Assert.That(result.Result, Is.InstanceOf<OkObjectResult>());
            var okResult = result.Result as OkObjectResult;
            Assert.That(okResult!.Value, Is.EqualTo(articles));
        }

        [Test]
        public void GetById_ExistingId_ReturnsOk()
        {
            // Arrange
            var article = new ArticleDTO { Id = 1, Name = "A", Price = 10, Stock = 5 };
            repositoryMock.Setup(r => r.Get()).Returns(new List<ArticleDTO> { article });

            // Act
            var result = controller.GetById(1);

            // Assert
            Assert.That(result.Result, Is.InstanceOf<OkObjectResult>());
            var okResult = result.Result as OkObjectResult;
            Assert.That(okResult!.Value, Is.EqualTo(article));
        }

        [Test]
        public void GetById_NonExistingId_ReturnsNotFound()
        {
            // Arrange
            repositoryMock.Setup(r => r.Get()).Returns(new List<ArticleDTO>());

            // Act
            var result = controller.GetById(99);

            // Assert
            Assert.That(result.Result, Is.InstanceOf<NotFoundResult>());
        }

        [Test]
        public void Create_ValidModel_ReturnsCreatedAtAction()
        {
            // Arrange
            var article = new ArticleDTO { Id = 1, Name = "A", Price = 10, Stock = 5 };
            repositoryMock.Setup(r => r.Create(It.IsAny<ArticleDTO>())).Returns(article);

            // Act
            var result = controller.Create(article);

            // Assert
            Assert.That(result.Result, Is.InstanceOf<CreatedAtActionResult>());
            var createdResult = result.Result as CreatedAtActionResult;
            Assert.That(createdResult!.Value, Is.EqualTo(article));
        }

        [Test]
        public void Create_InvalidModel_ReturnsBadRequest()
        {
            // Arrange
            controller.ModelState.AddModelError("Name", "Required");
            var article = new ArticleDTO();

            // Act
            var result = controller.Create(article);

            // Assert
            Assert.That(result.Result, Is.InstanceOf<Microsoft.AspNetCore.Mvc.BadRequestObjectResult>());
        }

        [Test]
        public void Update_ValidModel_ReturnsOk()
        {
            // Arrange
            var article = new ArticleDTO { Id = 1, Name = "A", Price = 10, Stock = 5 };
            repositoryMock.Setup(r => r.Update(It.IsAny<ArticleDTO>())).Returns(article);

            // Act
            var result = controller.Update(1, article);

            // Assert
            Assert.That(result.Result, Is.InstanceOf<OkObjectResult>());
            var okResult = result.Result as OkObjectResult;
            Assert.That(okResult!.Value, Is.EqualTo(article));
        }

        [Test]
        public void Update_InvalidModel_ReturnsBadRequest()
        {
            // Arrange
            controller.ModelState.AddModelError("Name", "Required");
            var article = new ArticleDTO();

            // Act
            var result = controller.Update(1, article);

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
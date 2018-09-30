using System;
using System.Threading.Tasks;
using XOProject.Controller;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using Moq;

namespace XOProject.Tests
{
    public class PortfolioControllerTests
    {
        private readonly Mock<IPortfolioRepository> _portfolioRepositoryMock = new Mock<IPortfolioRepository>();

        private readonly PortfolioController _portfolioController;

        public PortfolioControllerTests()
        {
            _portfolioController = new PortfolioController(_portfolioRepositoryMock.Object);
        }

        [Test]
        public async Task Post_ShouldInsertPortfolio()
        {
            var hourRate = new Portfolio
            {
                Name = "Portfolio Test 01"
            };

            // Arrange

            // Act
            var result = await _portfolioController.Post(hourRate);

            // Assert
            Assert.NotNull(result);

            var createdResult = result as CreatedResult;
            Assert.NotNull(createdResult);
            Assert.AreEqual(201, createdResult.StatusCode);
        }
        
    }
}

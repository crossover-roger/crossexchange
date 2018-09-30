using System;
using System.Threading.Tasks;
using XOProject.Controller;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using Moq;

namespace XOProject.Tests
{
    public class TradeControllerTests
    {
        private readonly Mock<IPortfolioRepository> _portfolioRepositoryMock = new Mock<IPortfolioRepository>();
        private readonly Mock<IShareRepository> _shareRepositoryMock = new Mock<IShareRepository>();
        private readonly Mock<ITradeRepository> _tradeRepositoryMock = new Mock<ITradeRepository>();

        private readonly TradeController _tradeController;

        public TradeControllerTests()
        {
            _tradeController = new TradeController(_portfolioRepositoryMock.Object, _shareRepositoryMock.Object, _tradeRepositoryMock.Object);
        }

        [Test]
        public async Task Post_ShouldInsertTrade()
        {
            var portfolioId = Guid.NewGuid();
            var trade = new TradeViewModel
            {
                Symbol = "CBI",
                Quantity = 80,
                Action = OperationEnum.Buy
            };

            // Arrange

            // Act
            var result = await _tradeController.Post(portfolioId, trade);

            // Assert
            Assert.NotNull(result);

            var createdResult = result as CreatedResult;
            Assert.NotNull(createdResult);
            Assert.AreEqual(201, createdResult.StatusCode);
        }
        
    }
}

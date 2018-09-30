using System;
using System.Threading.Tasks;
using XOProject.Controller;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using Moq;
using System.Collections.Generic;
using System.Linq;

namespace XOProject.Tests
{
    public class ShareControllerTests
    {
        private readonly Mock<IShareRepository> _shareRepositoryMock = new Mock<IShareRepository>();

        private readonly ShareController _shareController;

        public ShareControllerTests()
        {
            _shareController = new ShareController(_shareRepositoryMock.Object);
        }

        [Test]
        public async Task Post_ShouldInsertShare()
        {
            var share = new Share
            {
                Symbol = "CBI"
            };

            // Arrange

            // Act
            var result = await _shareController.Post(share);

            // Assert
            Assert.NotNull(result);

            var createdResult = result as CreatedResult;
            Assert.NotNull(createdResult);
            Assert.AreEqual(201, createdResult.StatusCode);
        }

        [Test]
        public async Task Post_ShouldInsertShareRate()
        {
            var symbol = "CBI";
            var shareRate = new ShareRates
            {
                Value = 330.0M,
                TimeStamp = new DateTime(2018, 08, 17, 5, 0, 0)
            };

            var shares = new List<Share>()
                {
                    new Share()
                    {
                        Id = Guid.NewGuid(),
                        Symbol = symbol
                    }
                };

            // Arrange
            _shareRepositoryMock.Setup(repo => repo.Query())
                .Returns(shares.AsQueryable());
                

            // Act
            var result = await _shareController.PostRate(symbol, shareRate);

            // Assert
            Assert.NotNull(result);

            var createdResult = result as CreatedResult;
            Assert.NotNull(createdResult);
            Assert.AreEqual(201, createdResult.StatusCode);
        }
    }
}

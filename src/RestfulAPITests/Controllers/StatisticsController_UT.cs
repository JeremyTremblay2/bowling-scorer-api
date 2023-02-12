using DTOs;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Model.Players;
using Moq;
using RestfulAPI.Controllers;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace RestfulAPITests.Controllers
{
    [TestClass]
    public class StatisticsController_UT
    {
        // The controller that will be tested
        private static StatisticsController controller;
        private static Mock<IStatisticService> service;

        [TestInitialize]
        public void Init()
        {
            var logger = new NullLogger<StatisticsController>();
            service = new Mock<IStatisticService>();
            service.Setup(service => service.GetAll(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(Task.FromResult(StatisticsStub()));
            service.Setup(service => service.GetById(It.IsAny<int>()))
                .Returns(new Func<int, Task<Statistics>>((id) => Task.FromResult(FakeGetById(id))));
            service.Setup(service => service.AddStatistics(It.IsAny<Statistics>()))
                .Returns(new Func<Statistics, Task<bool>>((player) => Task.FromResult(FakeAddStatistics(player))));
            service.Setup(service => service.RemoveStatistics(It.IsAny<int>()))
                .Returns(new Func<int, Task<bool>>((id) => Task.FromResult(FakeDeleteStatistics(id))));
            service.Setup(service => service.EditStatistics(It.IsAny<Statistics>()))
                .Returns(new Func<Statistics, Task<bool>>((player) => Task.FromResult(FakeEditStatistics(player))));

            controller = new StatisticsController(logger, service.Object);
        }

        [TestMethod]
        public async Task GetAll_ReturnsOK()
        {
            // Arrange
            var loggerMock = new Mock<ILogger<StatisticsController>>();
            var statisticServiceMock = new Mock<IStatisticService>();
            var statistics = new List<Statistics>
            {
                new Statistics(),
                new Statistics()
            };
            var dtos = new List<StatisticDTO>
            {
                new StatisticDTO(),
                new StatisticDTO()
            };
            statisticServiceMock.Setup(s => s.GetAll(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(Task.FromResult(statistics.AsEnumerable()));
            var controller = new StatisticsController(loggerMock.Object, statisticServiceMock.Object);

            // Act
            var result = await controller.GetAll(1, 10);

            // Assert
            Assert.AreEqual(typeof(OkObjectResult), result.GetType());
            Assert.AreEqual(((OkObjectResult)result).StatusCode, (int)HttpStatusCode.OK);
            var objectResult = (OkObjectResult)result;
            Assert.AreEqual(typeof(List<StatisticDTO>), objectResult.Value.GetType());
            var listResult = (List<StatisticDTO>)objectResult.Value;
            Assert.AreEqual(listResult, dtos);
        }

        private Statistics? FakeGetById(int id)
        {
            Statistics? stat = StatisticsStub().Where(p => p.ID == id).FirstOrDefault();
            if (stat is null)
            {
                throw new StatisticsNotFoundException("This statistics doesn't exists.");
            }
            return stat;
        }

        private bool FakeEditStatistics(Statistics statistics)
        {
            if (statistics.ID == 2)
            {
                return true;
            }
            if (statistics.ID > 4)
            {
                throw new StatisticsNotFoundException("The statistics that you want to edit doesn't exists");
            }
            return false;
        }

        private bool FakeDeleteStatistics(int id)
        {
            // Simulate DB error
            if (id == 7)
            {
                return false;
            }
            if (id > 4)
            {
                throw new StatisticsNotFoundException("The statistics that you want to delete doesn't exists");
            }
            return true;
        }

        private bool FakeAddStatistics(Statistics statistics)
        {
            // Simulate alreday exists
            if (statistics.ID == 1)
            {
                throw new StatisticsAlreadyExistsException("A statistics with the same ID already exists");
            }
            // Simulate error in DB
            if (statistics.ID == 2)
            {
                return false;
            }
            return true;
        }

        private IEnumerable<Statistics> StatisticsStub()
        {
            return new List<Statistics>() {
                new Statistics(0, 2, 3, 10, 50),
                new Statistics(1, 1, 5, 8, 278),
                new Statistics(2, 2, 8, 20, 236),
                new Statistics(3, 0, 0, 5, 145),
                new Statistics(4, 1, 5, 14, 256)
            };
        }

        private List<StatisticDTO> StatisticsDTOStub()
        {
            return new List<StatisticDTO>() {
                 new StatisticDTO{ID = 0 , NumberOfVictory = 2, NumberOfDefeat = 3, NumberOfGames = 10, BestScore = 50 },
                 new StatisticDTO{ID = 1 , NumberOfVictory = 1, NumberOfDefeat = 5, NumberOfGames = 8, BestScore = 278 },
                 new StatisticDTO{ID = 2 , NumberOfVictory = 2, NumberOfDefeat = 8, NumberOfGames = 20, BestScore = 236 },
                 new StatisticDTO{ID = 3 , NumberOfVictory = 0, NumberOfDefeat = 0, NumberOfGames = 5, BestScore = 145 },
                 new StatisticDTO{ID = 4 , NumberOfVictory = 1, NumberOfDefeat = 5, NumberOfGames = 14, BestScore = 256 },
            };
        }

        [TestMethod]
        public async Task GetAllTest()
        {
            //Act
            var result = await controller.GetAll(0, 3);

            //Assert
            service.Verify(mock => mock.GetAll(0, 3), Times.Once);
            result.Should().BeAssignableTo<OkObjectResult>();
            var respResult = result as OkObjectResult;
            respResult.Should().NotBeNull();
            respResult?.Value.Should().BeAssignableTo<IEnumerable<StatisticDTO>>();

            List<StatisticDTO> excepted = StatisticsDTOStub();
            List<StatisticDTO>? resp = respResult?.Value as List<StatisticDTO>;
            resp.Should().NotBeNull();
            if (resp is not null)
            {
                excepted.Should().BeEquivalentTo(resp);
            }
        }

        [TestMethod]
        public async Task GetByIdTest()
        {
            //Act
            var result = await controller.GetById(1);

            //Assert
            service.Verify(mock => mock.GetById(It.IsAny<int>()), Times.Once);
            result.Should().BeAssignableTo<OkObjectResult>();
            var okResult = result as OkObjectResult;
            okResult.Should().NotBeNull();
            okResult?.Value.Should().BeAssignableTo<StatisticDTO>();
            okResult?.Value.Should().BeEquivalentTo(new StatisticDTO { ID = 1, NumberOfVictory = 1, NumberOfDefeat = 5, NumberOfGames = 8, BestScore = 278 });
        }

        [TestMethod]
        public async Task GetByIdNotFoundTest()
        {
            //Act
            var result = await controller.GetById(65);

            //Assert
            service.Verify(mock => mock.GetById(It.IsAny<int>()), Times.Once);
            result.Should().BeAssignableTo<NotFoundObjectResult>();
            var notFoundResult = result as NotFoundObjectResult;
            notFoundResult?.Value.Should().NotBeNull()
                .And.BeEquivalentTo("This statistics doesn't exists.");
        }

        [TestMethod]
        public async Task AddTest()
        {
            var stats = new StatisticDTO { ID = 6, NumberOfVictory = 2, NumberOfDefeat = 3, NumberOfGames = 10, BestScore = 50 };

            //Act
            var result = await controller.Add(stats);

            //Assert
            service.Verify(mock => mock.AddStatistics(It.IsAny<Statistics>()), Times.Once);
            result.Should().BeAssignableTo<OkObjectResult>();
            var okResult = result as OkObjectResult;
            okResult.Should().NotBeNull();
            okResult?.Value.Should().NotBeNull()
                .And.BeEquivalentTo("Successfuly added the statistics id : 6");
        }

        [TestMethod]
        public async Task AddStatisticsDBErrorTest()
        {
            var stat = new StatisticDTO { ID = 2, NumberOfVictory = 2, NumberOfDefeat = 8, NumberOfGames = 20, BestScore = 236 };

            //Act
            var result = await controller.Add(stat);

            //Assert
            service.Verify(mock => mock.AddStatistics(It.IsAny<Statistics>()), Times.Once);
            result.Should().BeAssignableTo<ObjectResult>();
            var badRequestResult = result as ObjectResult;
            badRequestResult?.Value.Should().NotBeNull();
            badRequestResult?.Value.Should().NotBeNull()
                .And.BeEquivalentTo("Error in the Data base.");
        }

        [TestMethod]
        public async Task EditTest()
        {
            var stat = new StatisticDTO { ID = 2, NumberOfVictory = 40, NumberOfDefeat = 40, NumberOfGames = 80, BestScore = 236 };

            var result = await controller.Edit(stat);

            service.Verify(mock => mock.EditStatistics(It.IsAny<Statistics>()), Times.Once);
            result.Should().BeAssignableTo<OkObjectResult>();
            var okResult = result as OkObjectResult;
            okResult?.Value.Should().NotBeNull();
            okResult?.Value.Should().NotBeNull()
                .And.BeEquivalentTo("Successfuly edited the statistics id : 2");
        }

        [TestMethod]
        public async Task EditFailedTest()
        {
            var stat = new StatisticDTO { ID = 7, NumberOfVictory = 2, NumberOfDefeat = 8, NumberOfGames = 20, BestScore = 236 };

            var result = await controller.Edit(stat);

            service.Verify(mock => mock.EditStatistics(It.IsAny<Statistics>()), Times.Once);
            result.Should().BeAssignableTo<BadRequestObjectResult>();
            var badRequestResult = result as BadRequestObjectResult;
            badRequestResult?.Value.Should().NotBeNull();
            badRequestResult?.Value.Should().NotBeNull()
                .And.BeEquivalentTo("The statistics that you want to edit doesn't exists");
        }

        [TestMethod]
        public async Task EditDBErrorTest()
        {
            var stat = new StatisticDTO { ID = -1, NumberOfVictory = 40, NumberOfDefeat = 40, NumberOfGames = 80, BestScore = 236 };

            var result = await controller.Edit(stat);

            service.Verify(mock => mock.EditStatistics(It.IsAny<Statistics>()), Times.Once);
            result.Should().BeAssignableTo<ObjectResult>();
            var badRequestResult = result as ObjectResult;
            badRequestResult?.Value.Should().NotBeNull();
            badRequestResult?.Value.Should().NotBeNull()
                .And.BeEquivalentTo("Error in the Data base while editing the statistics.");
        }

        [TestMethod]
        public async Task DeleteTest()
        {
            var result = await controller.Delete(1);

            service.Verify(mock => mock.RemoveStatistics(It.IsAny<int>()), Times.Once);
            result.Should().BeAssignableTo<OkObjectResult>();
            var okResult = result as OkObjectResult;
            okResult?.Value.Should().NotBeNull();
            okResult?.Value.Should().NotBeNull()
                .And.BeEquivalentTo("The statistics 1 was succesfully removed from the server.");
        }

        [TestMethod]
        public async Task DeleteFailedTest()
        {
            //Act
            var result = await controller.Delete(16);

            //Assert
            service.Verify(mock => mock.RemoveStatistics(It.IsAny<int>()), Times.Once);
            result.Should().BeAssignableTo<BadRequestObjectResult>();
            var badRequestResult = result as BadRequestObjectResult;
            badRequestResult?.Value.Should().NotBeNull();
            badRequestResult?.Value.Should().NotBeNull()
                .And.BeEquivalentTo("The statistics that you want to delete doesn't exists");
        }

        [TestMethod]
        public async Task DeleteDBErrorTest()
        {
            var result = await controller.Delete(7);

            service.Verify(mock => mock.RemoveStatistics(It.IsAny<int>()), Times.Once);
            result.Should().BeAssignableTo<ObjectResult>();
            var badRequestResult = result as ObjectResult;
            badRequestResult?.Value.Should().NotBeNull();
            badRequestResult?.Value.Should().NotBeNull()
                .And.BeEquivalentTo("Error in the Data base.");
        }
    }
}

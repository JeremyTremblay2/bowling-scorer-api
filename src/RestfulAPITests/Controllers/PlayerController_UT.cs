using DTOs;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging.Abstractions;
using Model;
using Moq;
using RestfulAPI.Controllers;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace RestfulAPITests.Controllers
{
    [TestClass]
    public class PlayerControllerTests
    {
        // The controller that will be tested
        private static PlayerController controller;
        private static Mock<IPlayerService> service;

        [TestInitialize]
        public void Init()
        {
            var logger = new NullLogger<PlayerController>();
            service = new Mock<IPlayerService>();
            service.Setup(service => service.GetAll(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(Task.FromResult(PlayerStub()));
            service.Setup(service => service.GetById(It.IsAny<int>()))
                .Returns(new Func<int, Task<Player>>((id) => Task.FromResult(FakeGetById(id))));
            service.Setup(service => service.AddPlayer(It.IsAny<Player>()))
                .Returns(new Func<Player, Task<bool>>((player) => Task.FromResult(FakeAddPlayer(player))));
            service.Setup(service => service.DeletePlayer(It.IsAny<int>()))
                .Returns(new Func<int, Task<bool>>((id) => Task.FromResult(FakeDeletePlayer(id))));
            service.Setup(service => service.EditPlayer(It.IsAny<Player>()))
                .Returns(new Func<Player, Task<bool>>((player) => Task.FromResult(FakeEditPlayer(player))));

            controller = new PlayerController(logger, service.Object);
        }

        private Player? FakeGetById(int id)
        {
            Player? p = PlayerStub().Where(p => p.ID == id).FirstOrDefault();
            if (p is null)
            {
                throw new FunctionnalException("This player doesn't exists.");   
            }
            return p;
        }

        private bool FakeEditPlayer(Player player)
        {
            if (player.ID == 2)
            {
                return true;
            }
            if (player.ID > 4)
            {
                throw new FunctionnalException("The player that you want to edit doesn't exists");
            }
            return false;
        }

        private bool FakeDeletePlayer(int id)
        {
            // Simulate DB error
            if (id == 7)
            {
                return false;
            }
            if (id > 4)
            {
                throw new FunctionnalException("The player that you want to delete doesn't exists");
            }
            return true;
        }

        private bool FakeAddPlayer(Player player)
        {
            // Simulate alreday exists
            if (player.ID == 1)
            {
                throw new FunctionnalException("A player with the same ID already exists");
            }
            // Simulate error in DB
            if (player.ID == 2)
            {
                return false;
            }
            return true;
        }

        private IEnumerable<Player> PlayerStub()
        {
            return new List<Player>() {
                new Player(0 , "Mickaël", "mickael.png"),
                new Player(1 , "Jeremy", "jeremy.png"),
                new Player(2 , "Come", "come.png"),
                new Player(3 , "Sonic", "sonic.png"),
                new Player(4 , "Mathis", "mathis.png")
            };
        }

        private List<PlayerDTO> PlayerStubDTO()
        {
            return new List<PlayerDTO>() {
                 new PlayerDTO{ID = 0 , Name = "Mickaël", Image = "mickael.png" },
                 new PlayerDTO{ID = 1 , Name = "Jeremy", Image = "jeremy.png" },
                 new PlayerDTO{ID = 2 , Name = "Come", Image = "come.png" },
                 new PlayerDTO{ID = 3 , Name = "Sonic", Image = "sonic.png" },
                 new PlayerDTO{ID = 4 , Name = "Mathis", Image = "mathis.png" }
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
            respResult?.Value.Should().BeAssignableTo<IEnumerable<PlayerDTO>>();

            List<PlayerDTO> excepted = PlayerStubDTO();
            List<PlayerDTO>? resp = respResult?.Value as List<PlayerDTO>;
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
            okResult?.Value.Should().BeAssignableTo<PlayerDTO>();
            okResult?.Value.Should().BeEquivalentTo(new PlayerDTO { ID = 1, Name = "Jeremy", Image = "jeremy.png" });
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
                .And.BeEquivalentTo("This player doesn't exists.");
        }

        [TestMethod]
        public async Task AddTest()
        {
            var player = new PlayerDTO { ID = 6, Image = "baptiste.png", Name = "Baptiste" };

            //Act
            var result = await controller.Add(player);

            //Assert
            service.Verify(mock => mock.AddPlayer(It.IsAny<Player>()), Times.Once);
            result.Should().BeAssignableTo<OkObjectResult>();
            var okResult = result as OkObjectResult;
            okResult.Should().NotBeNull();
            okResult?.Value.Should().NotBeNull()
                .And.BeEquivalentTo("Successfuly added the player id : 6");
        }

        [TestMethod]
        public async Task AddPlayerAlreadyExistsTest()
        {
            var player = new PlayerDTO { ID = 1, Image = "moi.png", Name = "Moi" };

            //Act
            var result = await controller.Add(player);

            //Assert
            service.Verify(mock => mock.AddPlayer(It.IsAny<Player>()), Times.Once);
            result.Should().BeAssignableTo<BadRequestObjectResult>();
            var badRequestResult = result as BadRequestObjectResult;
            badRequestResult.Should().NotBeNull();
            badRequestResult?.Value.Should().NotBeNull()
                .And.BeEquivalentTo("A player with the same ID already exists");
        }

        [TestMethod]
        public async Task AddPlayerDBErrorTest()
        {
            var player = new PlayerDTO { ID = 2, Image = "moi.png", Name = "Moi" };

            //Act
            var result = await controller.Add(player);

            //Assert
            service.Verify(mock => mock.AddPlayer(It.IsAny<Player>()), Times.Once);
            result.Should().BeAssignableTo<ObjectResult>();
            var badRequestResult = result as ObjectResult;
            badRequestResult?.Value.Should().NotBeNull();
            badRequestResult?.Value.Should().NotBeNull()
                .And.BeEquivalentTo("Error in the Data base.");
        }

        [TestMethod]
        public async Task EditTest()
        {
            var player = new PlayerDTO { ID = 2, Image = "modif.png", Name = "Modif" };

            var result = await controller.Edit(player);

            service.Verify(mock => mock.EditPlayer(It.IsAny<Player>()), Times.Once);
            result.Should().BeAssignableTo<OkObjectResult>();
            var okResult = result as OkObjectResult;
            okResult?.Value.Should().NotBeNull();
            okResult?.Value.Should().NotBeNull()
                .And.BeEquivalentTo("Successfuly edited the player id : 2");
        }

        [TestMethod]
        public async Task EditFailedTest()
        {
            var player = new PlayerDTO { ID = 7, Image = "modif.png", Name = "Modif" };

            var result = await controller.Edit(player);

            service.Verify(mock => mock.EditPlayer(It.IsAny<Player>()), Times.Once);
            result.Should().BeAssignableTo<BadRequestObjectResult>();
            var badRequestResult = result as BadRequestObjectResult;
            badRequestResult?.Value.Should().NotBeNull();
            badRequestResult?.Value.Should().NotBeNull()
                .And.BeEquivalentTo("The player that you want to edit doesn't exists");
        }

        [TestMethod]
        public async Task EditDBErrorTest()
        {
            var player = new PlayerDTO { ID = -1, Image = "modif.png", Name = "Modif" };

            var result = await controller.Edit(player);

            service.Verify(mock => mock.EditPlayer(It.IsAny<Player>()), Times.Once);
            result.Should().BeAssignableTo<ObjectResult>();
            var badRequestResult = result as ObjectResult;
            badRequestResult?.Value.Should().NotBeNull();
            badRequestResult?.Value.Should().NotBeNull()
                .And.BeEquivalentTo("Error in the Data base.");
        }

        [TestMethod]
        public async Task DeleteTest()
        {
            var result = await controller.Delete(1);

            service.Verify(mock => mock.DeletePlayer(It.IsAny<int>()), Times.Once);
            result.Should().BeAssignableTo<OkObjectResult>();
            var okResult = result as OkObjectResult;
            okResult?.Value.Should().NotBeNull();
            okResult?.Value.Should().NotBeNull()
                .And.BeEquivalentTo("Successfuly delete the player id : 1");
        }

        [TestMethod]
        public async Task DeleteFailedTest()
        {
            //Act
            var result = await controller.Delete(16);

            //Assert
            service.Verify(mock => mock.DeletePlayer(It.IsAny<int>()), Times.Once);
            result.Should().BeAssignableTo<BadRequestObjectResult>();
            var badRequestResult = result as BadRequestObjectResult;
            badRequestResult?.Value.Should().NotBeNull();
            badRequestResult?.Value.Should().NotBeNull()
                .And.BeEquivalentTo("The player that you want to delete doesn't exists");
        }

        [TestMethod]
        public async Task DeleteDBErrorTest()
        {
            var result = await controller.Delete(7);

            service.Verify(mock => mock.DeletePlayer(It.IsAny<int>()), Times.Once);
            result.Should().BeAssignableTo<ObjectResult>();
            var badRequestResult = result as ObjectResult;
            badRequestResult?.Value.Should().NotBeNull();
            badRequestResult?.Value.Should().NotBeNull()
                .And.BeEquivalentTo("Error in the Data base.");
        }
    }
}
using DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Model;
using Moq;
using RestfulAPI.Controllers;
using Services;

namespace TestAPI
{
    public class PlayerController_UT
    {
        [Fact]
        public async void GetAllTest()
        {
            //Arrange
            var _mockPlayerService = new Mock<IPlayerService>();
            _mockPlayerService.Setup(service => service.GetAll())
                .ReturnsAsync(new List<Player> { new Player("Mickael", "mickael.png") });
            var _mockLoger = new NullLogger<PlayerController>();
            var controller = new PlayerController(_mockLoger, _mockPlayerService.Object);

            //Act
            var result = await controller.GetAll();

            //Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsAssignableFrom<IEnumerable<PlayerDTO>>(okResult.Value);
            Assert.Equal(new List<PlayerDTO>() { new PlayerDTO { Name = "Mickael", Image = "mickael.png" } }, returnValue);
        }

        [Fact]
        public async void GetByIdFoundTest()
        {
            //Arrange
            var _mockPlayerService = new Mock<IPlayerService>();
            _mockPlayerService.Setup(service => service.GetById(It.IsAny<int>()))
                .Returns<int>(async (id) =>
                {
                    if (id == 1)
                    {
                        return new Player(id, "Mickael", "mickael.png");
                    }
                    throw new FunctionnalException("This player doesn't exists.");
                });
            var _mockLoger = new NullLogger<PlayerController>();
            var controller = new PlayerController(_mockLoger, _mockPlayerService.Object);

            //Act
            var result = await controller.GetById(1);

            //Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.NotNull(okResult.Value);
            var returnValue = Assert.IsAssignableFrom<PlayerDTO>(okResult.Value);
            Assert.Equal(new PlayerDTO { ID = 1, Name = "Mickael", Image = "mickael.png" }, returnValue);
        }

        [Fact]
        public async void GetByIdNotFoundTest()
        {
            //Arrange
            var _mockPlayerService = new Mock<IPlayerService>();
            _mockPlayerService.Setup(service => service.GetById(It.IsAny<int>()))
                .Returns<int>(async (id) =>
                {
                    if (id == 1)
                    {
                        return new Player(id, "Mickael", "mickael.png");
                    }
                    throw new FunctionnalException("This player doesn't exists.");
                });
            var _mockLoger = new NullLogger<PlayerController>();
            var controller = new PlayerController(_mockLoger, _mockPlayerService.Object);

            //Act
            var result = await controller.GetById(65);

            //Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.NotNull(notFoundResult.Value);
            Assert.Equal(notFoundResult.Value, "This player doesn't exists.");
        }

        [Fact]
        public async void AddPlayerTest()
        {
            //Arrange
            var _mockPlayerService = new Mock<IPlayerService>();
            _mockPlayerService.Setup(service => service.AddPlayer(It.IsAny<Player>()))
                .Returns<Player>(async (player) =>
                {
                    if (player.ID == 1)
                    {
                        throw new FunctionnalException("A player with the same ID already exists");
                    }
                    if (player.ID == 2)
                    {
                        throw new FunctionnalException("Failed to add the player (error while saving)");
                    }
                });
            var _mockLoger = new NullLogger<PlayerController>();
            var controller = new PlayerController(_mockLoger, _mockPlayerService.Object);
            var player = new PlayerDTO { ID = 3, Image = "moi.png", Name = "Moi" };

            //Act
            var result = await controller.Add(player);

            //Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.NotNull(okResult.Value);
            Assert.Equal(okResult.Value, "Successfuly added the player id : 3");
        }

        [Fact]
        public async void AddPlayerAlreadyExistsTest()
        {
            //Arrange
            var _mockPlayerService = new Mock<IPlayerService>();
            _mockPlayerService.Setup(service => service.AddPlayer(It.IsAny<Player>()))
                .Returns<Player>(async (player) =>
                {
                    if (player.ID == 1)
                    {
                        throw new FunctionnalException("A player with the same ID already exists");
                    }
                    if (player.ID == 2)
                    {
                        throw new FunctionnalException("Failed to add the player (error while saving)");
                    }
                });
            var _mockLoger = new NullLogger<PlayerController>();
            var controller = new PlayerController(_mockLoger, _mockPlayerService.Object);
            var player = new PlayerDTO { ID = 1, Image = "moi.png", Name = "Moi" };

            //Act
            var result = await controller.Add(player);

            //Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.NotNull(badRequestResult.Value);
            Assert.Equal(badRequestResult.Value, "A player with the same ID already exists");
        }

        [Fact]
        public async void AddPlayerSavingErrorTest()
        {
            //Arrange
            var _mockPlayerService = new Mock<IPlayerService>();
            _mockPlayerService.Setup(service => service.AddPlayer(It.IsAny<Player>()))
                .Returns<Player>(async (player) =>
                {
                    if (player.ID == 1)
                    {
                        throw new FunctionnalException("A player with the same ID already exists");
                    }
                    if (player.ID == 2)
                    {
                        throw new FunctionnalException("Failed to add the player (error while saving)");
                    }
                });
            var _mockLoger = new NullLogger<PlayerController>();
            var controller = new PlayerController(_mockLoger, _mockPlayerService.Object);
            var player = new PlayerDTO { ID = 2, Image = "moi.png", Name = "Moi" };

            //Act
            var result = await controller.Add(player);

            //Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.NotNull(badRequestResult.Value);
            Assert.Equal(badRequestResult.Value, "Failed to add the player (error while saving)");
        }

        [Fact]
        public async void EditPlayerTest()
        {
            //Arrange
            var _mockPlayerService = new Mock<IPlayerService>();
            _mockPlayerService.Setup(service => service.EditPlayer(It.IsAny<Player>()))
                .Returns<Player>(async (player) =>
                {
                    if (player.ID == 16)
                    {
                        throw new FunctionnalException("The player that you want to edit doesn't exists");
                    }
                });
            var _mockLoger = new NullLogger<PlayerController>();
            var controller = new PlayerController(_mockLoger, _mockPlayerService.Object);
            var player = new PlayerDTO { ID = 3, Image = "moi.png", Name = "Moi" };

            //Act
            var result = await controller.Edit(player);

            //Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.NotNull(okResult.Value);
            Assert.Equal(okResult.Value, "Successfuly edited the player id : 3");
        }

        [Fact]
        public async void EditPlayerFailedTest()
        {
            //Arrange
            var _mockPlayerService = new Mock<IPlayerService>();
            _mockPlayerService.Setup(service => service.EditPlayer(It.IsAny<Player>()))
                .Returns<Player>(async (player) =>
                {
                    if (player.ID == 16)
                    {
                        throw new FunctionnalException("The player that you want to edit doesn't exists");
                    }
                });
            var _mockLoger = new NullLogger<PlayerController>();
            var controller = new PlayerController(_mockLoger, _mockPlayerService.Object);
            var player = new PlayerDTO { ID = 16, Image = "moi.png", Name = "Moi" };

            //Act
            var result = await controller.Edit(player);

            //Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.NotNull(badRequestResult.Value);
            Assert.Equal(badRequestResult.Value, "The player that you want to edit doesn't exists");
        }

        [Fact]
        public async void DeletePlayerTest()
        {
            //Arrange
            var _mockPlayerService = new Mock<IPlayerService>();
            _mockPlayerService.Setup(service => service.DeletePlayer(It.IsAny<int>()))
                .Returns<int>(async (id) =>
                {
                    if (id == 16)
                    {
                        throw new FunctionnalException("The player that you want to delete doesn't exists");
                    }
                });
            var _mockLoger = new NullLogger<PlayerController>();
            var controller = new PlayerController(_mockLoger, _mockPlayerService.Object);

            //Act
            var result = await controller.Delete(1);

            //Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.NotNull(okResult.Value);
            Assert.Equal(okResult.Value, "Successfuly delete the player id : 1");
        }

        [Fact]
        public async void DeletePlayerFailedTest()
        {
            //Arrange
            var _mockPlayerService = new Mock<IPlayerService>();
            _mockPlayerService.Setup(service => service.DeletePlayer(It.IsAny<int>()))
                .Returns<int>(async (id) =>
                {
                    if (id == 16)
                    {
                        throw new FunctionnalException("The player that you want to delete doesn't exists");
                    }
                });
            var _mockLoger = new NullLogger<PlayerController>();
            var controller = new PlayerController(_mockLoger, _mockPlayerService.Object);

            //Act
            var result = await controller.Delete(16);

            //Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.NotNull(badRequestResult.Value);
            Assert.Equal(badRequestResult.Value, "The player that you want to delete doesn't exists");
        }
    }
}
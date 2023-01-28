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
            var controller = new PlayerController(_mockLoger , _mockPlayerService.Object);

            //Act
            var result = await controller.GetAll();

            //Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsAssignableFrom<IEnumerable<PlayerDTO>>(okResult.Value);
            Assert.Equal(new List<PlayerDTO>(){ new PlayerDTO { Name = "Mickael", Image = "mickael.png" }}, returnValue);
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


    }
}
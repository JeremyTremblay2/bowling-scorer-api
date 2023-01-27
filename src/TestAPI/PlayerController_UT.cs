using DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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
            var _mockLoger = Mock.Of<ILogger<PlayerController>>();
            var controller = new PlayerController(_mockLoger , _mockPlayerService.Object);

            //Act
            var result = await controller.GetAll();

            //Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsAssignableFrom<IEnumerable<PlayerDTO>>(okResult.Value);
            Assert.Equal(new List<PlayerDTO>(){ new PlayerDTO { Name = "Mickael", Image = "mickael.png" }}, returnValue);
        }
    }
}
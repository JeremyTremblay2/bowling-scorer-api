using DTOs;
using DTOtoModel;
using Microsoft.AspNetCore.Mvc;
using Model;
using Services;
using System.Numerics;
using System.Runtime.CompilerServices;

namespace RestfulAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayerController : Controller
    {
        private readonly ILogger _logger;
        private readonly IPlayerService _playerService;

        public PlayerController(ILogger<PlayerController> logger, IPlayerService playerService)
        {
            _logger = logger;
            _playerService = playerService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<PlayerDTO>))]
        public async Task<IActionResult> GetAll()
        {
            _logger.LogInformation("API Call : GetAll()");
            List<PlayerDTO> result = new();
            foreach (Player pl in await _playerService.GetAll())
            {
                result.Add(pl.ToDTO());
            }
            return Ok(result);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PlayerDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            _logger.LogInformation($"API Call : GetById(), Arguments = \"ID={id}\"");
            Player? player = await _playerService.GetById(id);
            if (player is null)
            {
                return NotFound("This player doesn't exists !");
            }
            return Ok(player.ToDTO());            
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Add([Bind("Id, Name, Image")] PlayerDTO playerDTO)
        {
            _logger.LogInformation($"API Call : Add(), Arguments = \"{playerDTO}\"");
            try
            {
                _playerService.AddPlayer(playerDTO.ToModel());
                return Ok();
            }
            catch (FunctionnalException e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Edit([Bind("Id, Name, Image")] PlayerDTO playerDTO)
        {
            _logger.LogInformation($"API Call : Edit(), Arguments = \"{playerDTO}\"");
            try
            {
                _playerService.EditPlayer(playerDTO.ToModel());
                return Ok();
            }
            catch (FunctionnalException e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Delete(int id)
        {
            _logger.LogInformation($"API Call : Delete(), Arguments = Id=\"{id}\"");
            try
            {
                _playerService.DeletePlayer(id);
                return Ok();
            }
            catch (FunctionnalException e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}

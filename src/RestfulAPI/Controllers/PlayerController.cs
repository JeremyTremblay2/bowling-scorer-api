using DTOs;
using DTOtoModel;
using Microsoft.AspNetCore.Mvc;
using Model;
using Services;
using System.Numerics;
using System.Runtime.CompilerServices;

namespace RestfulAPI.Controllers
{
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")]
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
        public async Task<IActionResult> GetAll(int page, int nbPlayers)
        {
            _logger.LogInformation("API Call : GetAll()");
            List<PlayerDTO> result = new();
            foreach (Player pl in await _playerService.GetAll(page, nbPlayers))
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
            try
            {
                Player player = await _playerService.GetById(id);
                return Ok(player.ToDTO());
            }
            catch (FunctionnalException e)
            {
                _logger.LogError(e.StackTrace);
                return NotFound(e.Message);
            }
                       
        }

        // UTILISER CREATED AT ACTION
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Add([Bind("Id, Name, Image")] PlayerDTO playerDTO)
        {
            _logger.LogInformation($"API Call : Add(), Arguments = \"{playerDTO}\"");
            try
            {
                if (await _playerService.AddPlayer(playerDTO.ToModel()))
                {
                    return Ok("Successfuly added the player id : " + playerDTO.ID);
                }
                else
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, "Error in the Data base.");
                }
            }
            catch (FunctionnalException e)
            {
                _logger.LogError(e.StackTrace);
                return BadRequest(e.Message);
            }
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Edit([Bind("Id, Name, Image")] PlayerDTO playerDTO)
        {
            _logger.LogInformation($"API Call : Edit(), Arguments = \"{playerDTO}\"");
            try
            {
                if (await _playerService.EditPlayer(playerDTO.ToModel()))
                {
                    return Ok("Successfuly edited the player id : " + playerDTO.ID);
                }
                else
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, "Error in the Data base.");
                }
            }
            catch (FunctionnalException e)
            {
                _logger.LogError(e.StackTrace);
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(int id)
        {
            _logger.LogInformation($"API Call : Delete(), Arguments = Id=\"{id}\"");
            try
            {
                if (await _playerService.DeletePlayer(id))
                {
                    return Ok("Successfuly delete the player id : " + id);
                }
                else
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, "Error in the Data base.");
                }
            }
            catch (FunctionnalException e)
            {
                _logger.LogError(e.StackTrace);
                return BadRequest(e.Message);
            }
        }
    }
}

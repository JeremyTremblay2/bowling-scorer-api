using DTOs;
using DTOtoModel;
using Microsoft.AspNetCore.Mvc;
using Model.Players;
using Services;

namespace RestfulAPI.Controllers
{
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class StatisticsController : Controller
    {
        private readonly ILogger _logger;
        private readonly IStatisticService _statisticService;

        public StatisticsController(ILogger<StatisticsController> logger, IStatisticService statisticService)
        {
            _logger = logger;
            _statisticService = statisticService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<StatisticDTO>))]
        public async Task<IActionResult> GetAll(int page, int nbStatistics)
        {
            _logger.LogInformation("Statistics API Call : GetAll()");
            List<StatisticDTO> result = new();
            foreach (Statistics stat in await _statisticService.GetAll(page, nbStatistics))
            {
                result.Add(stat.ToDTO());
            }
            return Ok(result);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(StatisticDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetById(int id)
        {
            _logger.LogInformation($"Statistics API Call : GetById(), Arguments = \"ID={id}\"");
            try
            {
                Statistics stats = await _statisticService.GetById(id);
                return Ok(stats.ToDTO());
            }
            catch (StatisticsNotFoundException e)
            {
                _logger.LogError("Statistics not found: " + e.StackTrace);
                return NotFound(e.Message);
            }
            catch (Exception e)
            {
                _logger.LogError("Internal error during the extractin of the statistics: " + e.StackTrace);
                return StatusCode(500, e.Message);
            }
        }

        // UTILISER CREATED AT ACTION
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Add([Bind("Id, NumberOfVictory, NumberOfDefeat, NumberOfGames, BestScore")] StatisticDTO statisticsDTO)
        {
            _logger.LogInformation($"Statistics API Call : Add(), Arguments = \"{statisticsDTO}\"");
            try
            {
                if (await _statisticService.AddStatistics(statisticsDTO.ToModel()))
                {
                    return Ok("Successfuly added the statistics id : " + statisticsDTO.ID);
                }
                else
                {
                    _logger.LogWarning("Database error while adding some statistics into the server.");
                    return StatusCode(StatusCodes.Status500InternalServerError, "Error in the Data base.");
                }
            }
            catch (StatisticsAlreadyExistsException e)
            {
                _logger.LogError("Statistics already exists: " + e.StackTrace);
                return BadRequest(e.Message);
            }
            catch (StatisticsNotValidException e)
            {
                _logger.LogError("Error while trying to add an invalid statistics: " + e.StackTrace);
                return BadRequest(e.Message);
            }
            catch (Exception e)
            {
                _logger.LogError("Internal error during the addition of some statistics: " + e.StackTrace);
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Edit([Bind("Id, NumberOfVictory, NumberOfDefeat, NumberOfGames, BestScore")] StatisticDTO statisticsDTO)
        {
            _logger.LogInformation($"Statistics API Call : Edit(), Arguments = \"{statisticsDTO}\"");
            try
            {
                if (await _statisticService.EditStatistics(statisticsDTO.ToModel()))
                {
                    return Ok("Successfuly edited the statistics id : " + statisticsDTO.ID);
                }
                else
                {
                    _logger.LogWarning("Database error while editing some statistics into the server.");
                    return StatusCode(StatusCodes.Status500InternalServerError, "Error in the Data base while editing the statistics.");
                }
            }
            catch (StatisticsNotFoundException e)
            {
                _logger.LogError("Statistics was not found and cannot be edited: " + e.StackTrace);
                return BadRequest(e.Message);
            }
            catch (StatisticsNotValidException e)
            {
                _logger.LogError("Error while trying to edit an invalid statistics: " + e.StackTrace);
                return BadRequest(e.Message);
            }
            catch (Exception e)
            {
                _logger.LogError("Internal error during the edition of some statistics: " + e.StackTrace);
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(int id)
        {
            _logger.LogInformation($"Statistics API Call : Delete(), Arguments = Id=\"{id}\"");
            try
            {
                if (await _statisticService.RemoveStatistics(id))
                {
                    return Ok("The statistics " + id + " was succesfully removed from the server.");
                }
                else
                {
                    _logger.LogError("Error during the suppression of the statistics " + id + ".");
                    return StatusCode(StatusCodes.Status500InternalServerError, "Error in the Data base.");
                }
            }
            catch (StatisticsNotFoundException e)
            {
                _logger.LogError("Statistics was not found and cannot be removed: " + e.StackTrace);
                return BadRequest(e.Message);
            }
            catch (Exception e)
            {
                _logger.LogError("Internal error during the suppression of some statistics: " + e.StackTrace);
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }
    }
}

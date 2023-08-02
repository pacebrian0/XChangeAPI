using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using XChangeAPI.Controllers.Interfaces;
using XChangeAPI.Logic.Interfaces;
using XChangeAPI.Models.DB;

namespace XChangeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class HistoryController : ControllerBase, IHistoryController
    {
        private readonly IHistoryLogic _logic;
        private readonly ILogger<HistoryController> _log;
        public HistoryController(IHistoryLogic logic, ILogger<HistoryController> log)
        {
            _logic = logic;
            _log = log;
        }

        [HttpGet("user/{userID}")]
        public async Task<ActionResult<IEnumerable<History>>> GetHistoryByUser(int userID)
        {
            try
            {
                return Ok(await _logic.GetHistoryByUser(userID));
            }
            catch (Exception ex)
            {
                _log.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("threshold/{userID}")]
        public async Task<ActionResult<bool>> IsUserThresholded(int userID)
        {
            try
            {
                return Ok(await _logic.IsUserThresholded(userID));
            }
            catch (Exception ex)
            {
                _log.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult> PostHistory(History history)
        {
            if (history == null)
                return BadRequest("Invalid request");
            try
            {
                await _logic.PostHistory(history);
                return Ok();
            }
            catch (Exception ex)
            {
                _log.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }
    }
}

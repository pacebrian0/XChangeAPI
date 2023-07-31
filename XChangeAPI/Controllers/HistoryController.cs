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

        [HttpGet]
        public async Task<ActionResult<IEnumerable<History>>> GetHistoryByUser(int userID)
        {
            return Ok(await _logic.GetHistoryByUser(userID));
        }

    }
}

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
    public class CurrencyController : ControllerBase, ICurrencyController
    {
        private readonly ICurrencyLogic _logic;
        private readonly ILogger<CurrencyController> _log;
        public CurrencyController(ICurrencyLogic logic, ILogger<CurrencyController> log)
        {
            _logic = logic;
            _log = log;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Currency>>> GetCurrencies()
        {
            return Ok(await _logic.GetCurrencies());

        }
    }
}

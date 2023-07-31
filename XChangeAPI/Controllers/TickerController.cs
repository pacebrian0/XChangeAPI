using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using XChangeAPI.Controllers.Interfaces;
using XChangeAPI.Logic.Interfaces;
using XChangeAPI.Models.DB;
using XChangeAPI.Models.DTO;

namespace XChangeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TickerController : ControllerBase, ITickerController
    {
        private readonly ITickerLogic _logic;
        public TickerController(ITickerLogic logic)
        {
            _logic = logic;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Ticker>>> GetTickers()
        {
            return Ok(await _logic.GetTickers());
        }

        [HttpGet("exchangerate")]
        public async Task<ActionResult<Ticker>> GetExchangeRate([FromBody] ExchangeRateDTO dto)
        {
            return Ok(await _logic.GetExchangeRate(dto.currency1, dto.currency2));
        }

        [HttpPost("exchange")]
        public async Task<ActionResult<float>> Exchange([FromBody] ExchangeDTO dto)
        {
            return Ok(await _logic.Exchange(dto.currency1, dto.currency2, dto.amount));

        }
    }
}

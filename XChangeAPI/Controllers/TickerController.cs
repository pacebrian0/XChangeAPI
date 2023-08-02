using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using XChangeAPI.Controllers.Interfaces;
using XChangeAPI.Logic.Interfaces;
using XChangeAPI.Models.DB;
using XChangeAPI.Models.DTO;
using XChangeAPI.Services.Interfaces;

namespace XChangeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TickerController : ControllerBase, ITickerController
    {
        private readonly ITickerLogic _logic;
        private readonly ILogger<TickerController> _logger;

        public TickerController(ITickerLogic logic, ILogger<TickerController> logger)
        {
            _logic = logic;
            _logger = logger;
        }


        [HttpPost("exchangerate")]
        public async Task<ActionResult<Ticker>> GetExchangeRate([FromBody] ExchangeRateDTO dto)
        {
            if (dto == null)
                return BadRequest("Invalid request");

            try
            {
                return Ok(await _logic.GetExchangeRate(dto.currency1.ToUpper(), dto.currency2.ToUpper()));

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("exchange")]
        public async Task<ActionResult<string>> Exchange([FromBody] ExchangeDTO dto)
        {
            if (dto == null)
                return BadRequest("Invalid request");

            try
            {
                var result = await _logic.Exchange(dto.currency1.ToUpper(), dto.currency2.ToUpper(), dto.amount, dto.userID);
                if (!result.HasValue)
                {
                    return BadRequest("Rate Limit exceeded!");
                }
                return Ok(result.Value.ToString("0.00"));

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
    }
}

using Microsoft.AspNetCore.Mvc;
using XChangeAPI.Models.DB;
using XChangeAPI.Models.DTO;

namespace XChangeAPI.Controllers.Interfaces
{
    public interface ITickerController
    {
        Task<ActionResult<string>> Exchange([FromBody] ExchangeDTO dto);
        Task<ActionResult<Ticker>> GetExchangeRate([FromBody] ExchangeRateDTO dto);
    }
}
using Microsoft.AspNetCore.Mvc;
using XChangeAPI.Models.DB;

namespace XChangeAPI.Controllers.Interfaces
{
    public interface ICurrencyController
    {
        Task<ActionResult<IEnumerable<Currency>>> GetCurrencies();
    }
}
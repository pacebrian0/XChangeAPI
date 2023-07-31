using Microsoft.AspNetCore.Mvc;
using XChangeAPI.Models.DB;

namespace XChangeAPI.Controllers.Interfaces
{
    public interface IHistoryController
    {
        Task<ActionResult<IEnumerable<History>>> GetHistoryByUser(int userID);
    }
}
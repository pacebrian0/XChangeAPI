using XChangeAPI.Models.DB;

namespace XChangeAPI.Logic.Interfaces
{
    public interface IHistoryLogic
    {
        Task<IEnumerable<History>> GetHistoryByUser(int userID);
    }
}
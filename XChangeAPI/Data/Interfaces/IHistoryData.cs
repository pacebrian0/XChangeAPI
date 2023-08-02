using XChangeAPI.Models.DB;

namespace XChangeAPI.Data.Interfaces
{
    public interface IHistoryData
    {
        Task<IEnumerable<History>> GetHistoryByUser(int userID);
        Task<bool> IsUserThresholded(int userID);
        Task PostHistory(History history);
    }
}
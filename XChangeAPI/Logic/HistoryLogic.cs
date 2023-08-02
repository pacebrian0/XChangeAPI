using XChangeAPI.Data.Interfaces;
using XChangeAPI.Logic.Interfaces;
using XChangeAPI.Models.DB;
using XChangeAPI.Services.Interfaces;

namespace XChangeAPI.Logic
{
    public class HistoryLogic : IHistoryLogic
    {
        private readonly IHistoryData _data;
        private readonly ILogger<HistoryLogic> _log;

        public HistoryLogic(IHistoryData data, ILogger<HistoryLogic> log)
        {
            _data = data;
            _log = log;
        }

        public async Task<IEnumerable<History>> GetHistoryByUser(int userID)
        {
            return await _data.GetHistoryByUser(userID);
        }

        public async Task<bool> IsUserThresholded(int userID)
        {
            return await _data.IsUserThresholded(userID);
        }

        public async Task PostHistory(History history)
        {
            await _data.PostHistory(history);
        }
    }
}

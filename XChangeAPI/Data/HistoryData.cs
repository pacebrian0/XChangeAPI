using XChangeAPI.Data.Interfaces;

namespace XChangeAPI.Data
{
    public class HistoryData: BaseDatabase, IHistoryData
    {

        private readonly ILogger _logger;

        public HistoryData(IConfiguration config, ILogger logger) : base(config)
        {
            _logger = logger;
        }
    }
}

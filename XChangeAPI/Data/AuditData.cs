using XChangeAPI.Data.Interfaces;

namespace XChangeAPI.Data
{
    public class AuditData : BaseDatabase, IAuditData
    {
        private readonly ILogger _logger;

        public AuditData(IConfiguration config, ILogger logger) : base(config) 
        {
            _logger = logger;
        }
    }
}

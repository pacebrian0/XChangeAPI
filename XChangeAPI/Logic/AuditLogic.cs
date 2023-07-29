using XChangeAPI.Data;
using XChangeAPI.Data.Interfaces;

namespace XChangeAPI.Logic
{
    public class AuditLogic
    {
        private readonly IAuditData _data;

        public AuditLogic(IAuditData data)
        {
            _data = data ?? throw new ArgumentNullException(nameof(data));
        }
    }
}

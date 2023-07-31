using Dapper;
using MySqlConnector;
using XChangeAPI.Data.Interfaces;
using XChangeAPI.Models.DB;

namespace XChangeAPI.Data
{
    public class HistoryData : BaseDatabase, IHistoryData
    {

        private readonly ILogger<HistoryData> _logger;

        public HistoryData(IConfiguration config, ILogger<HistoryData> logger) : base(config)
        {
            _logger = logger;
        }

        public async Task<IEnumerable<History>> GetHistoryByUser(int userID)
        {

            try
            {
                const string sql = @"SELECT `HISTORY`.`id`,
                                        `HISTORY`.`name`,
                                        `HISTORY`.`abbreviation`,
                                        `HISTORY`.`status`,
                                        `HISTORY`.`checkedOn`,
                                        `HISTORY`.`createdOn`,
                                        `HISTORY`.`createdBy`,
                                        `HISTORY`.`modifiedOn`,
                                        `HISTORY`.`modifiedBy`,
                                    FROM `conciergedb`.`HISTORY`
                                    ";
                using (var conn = new MySqlConnection(_dbConn))
                {
                    return await conn.QueryAsync<History>(sql);

                }
            }
            catch (Exception e)
            {

                throw;
            }


        }
    }
}

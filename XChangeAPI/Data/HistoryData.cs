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
                                        `HISTORY`.`ticker`,
                                        `HISTORY`.`user`,
                                        `HISTORY`.`timestamp`,
                                        `HISTORY`.`status`
                                    FROM `XCHANGE`.`HISTORY`
                                    ";
                using (var conn = new MySqlConnection(_dbConn))
                {
                    return await conn.QueryAsync<History>(sql);

                }
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                throw;
            }


        }

        public async Task<bool> IsUserThresholded(int userID)
        {
            try
            {
                const string sql = @"SELECT 
                                        `HISTORY`.`timestamp`
                                    FROM `XCHANGE`.`HISTORY`
                                    WHERE user = @userID
                                    AND status = 'A'
                                    ORDER BY timestamp desc
                                    LIMIT 9,1
                                    ";
                using (var conn = new MySqlConnection(_dbConn))
                {
                    var date = await conn.ExecuteScalarAsync<DateTime?>(sql, new { userID });
                    if (!date.HasValue) return false; // not thresholded

                    if (date.Value.AddMinutes(30) > DateTime.UtcNow) return true; //thresholded

                    return false;

                }
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                throw;
            }

        }

        public async Task PostHistory(History history)
        {
            if (history is null)
            {
                throw new ArgumentNullException(nameof(history));
            }

            try
            {
                const string sql = @"INSERT INTO  `XCHANGE`.`HISTORY`
                                    (
                                        `ticker`,
                                        `user`,
                                        `timestamp`,
                                        `status`
                                    )
                                    VALUES
                                    (
                                        @ticker,
                                        @user,
                                        @timestamp,
                                        @status
                                    )
                                    ";
                using (var conn = new MySqlConnection(_dbConn))
                {
                    await conn.ExecuteAsync(sql, new { history.ticker, history.user, history.timestamp, history.status });

                }
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                throw;
            }

        }
    }
}

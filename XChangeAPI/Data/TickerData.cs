using Dapper;
using MySqlConnector;
using XChangeAPI.Data.Interfaces;
using XChangeAPI.Models.DB;

namespace XChangeAPI.Data
{
    public class TickerData : BaseDatabase, ITickerData
    {
        private readonly ILogger<TickerData> _logger;

        public TickerData(IConfiguration config, ILogger<TickerData> logger) : base(config)
        {
            _logger = logger;
        }

        public async Task<IEnumerable<Ticker>> GetTickers()
        {
            try
            {
                const string sql = @"SELECT `TICKER`.`id`,
                                        `TICKER`.`name`,
                                        `TICKER`.`currency1`,
                                        `TICKER`.`currency2`,
                                        `TICKER`.`status`,
                                        `TICKER`.`exchangerate`,
                                        `TICKER`.`checkedOn`,
                                        `TICKER`.`createdOn`,
                                        `TICKER`.`createdBy`,
                                        `TICKER`.`modifiedOn`,
                                        `TICKER`.`modifiedBy`,
                                    FROM `conciergedb`.`TICKER`
                                    ";
                using (var conn = new MySqlConnection(_dbConn))
                {
                    return await conn.QueryAsync<Ticker>(sql);

                }
            }
            catch (Exception e)
            {

                throw;
            }

        }



        public async Task<Ticker> GetExchangeRate(string curr1, string curr2)
        {
            if (string.IsNullOrEmpty(curr1))
            {
                throw new ArgumentException($"'{nameof(curr1)}' cannot be null or empty.", nameof(curr1));
            }

            if (string.IsNullOrEmpty(curr2))
            {
                throw new ArgumentException($"'{nameof(curr2)}' cannot be null or empty.", nameof(curr2));
            }

            try
            {
                const string sql = @"SELECT 
                                        `TICKER`.`currency1`,
                                        `TICKER`.`currency2`,
                                        `TICKER`.`exchangerate`,
                                    FROM `conciergedb`.`TICKER`
                                    WHERE `TICKER`.`status` = 'A'
                                    ";
                using (var conn = new MySqlConnection(_dbConn))
                {
                    return await conn.QuerySingleOrDefaultAsync<Ticker>(sql);

                }
            }
            catch (Exception e)
            {

                throw;
            }

        }


        public async Task UpdateExchangeRate(string curr1, string curr2, float amt)
        {

            if (string.IsNullOrEmpty(curr1))
            {
                throw new ArgumentException(nameof(curr1));
            }

            if (curr2 is null)
            {
                throw new ArgumentNullException(nameof(curr2));
            }

            const string sql = @"UPDATE `conciergedb`.`TICKER`
                                    SET `exchangerate` = @amt,
                                        `modifiedOn` = UTC_TIMESTAMP(),
                                        `modifiedBy` = @modifiedBy
                                    WHERE `TICKER`.`currency1` = @curr1
                                    AND   `TICKER`.`currency2` = @curr2";

            using (var conn = new MySqlConnection(_dbConn))
            {
                await conn.ExecuteAsync(sql, new { curr1, curr2, modifiedBy = 1, amt });
            }

        }



    }


}

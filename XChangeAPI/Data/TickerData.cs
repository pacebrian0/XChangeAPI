using Dapper;
using MySqlConnector;
using XChangeAPI.Data.Interfaces;
using XChangeAPI.Models.DB;

namespace XChangeAPI.Data
{
    public class TickerData : BaseDatabase, ITickerData
    {
        private readonly ILogger _logger;

        public TickerData(IConfiguration config, ILogger logger) : base(config)
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


        
    }
}

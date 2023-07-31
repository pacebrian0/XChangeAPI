using Dapper;
using MySqlConnector;
using XChangeAPI.Data.Interfaces;
using XChangeAPI.Models.DB;

namespace XChangeAPI.Data
{
    public class CurrencyData : BaseDatabase, ICurrencyData
    {
        private readonly ILogger<CurrencyData> _logger;

        public CurrencyData(IConfiguration config, ILogger<CurrencyData> logger) : base(config)
        {
            _logger = logger;
        }

        public async Task<IEnumerable<Currency>> GetCurrencies()
        {
            try
            {
                const string sql = @"SELECT `CURRENCY`.`id`,
                                        `CURRENCY`.`name`,
                                        `CURRENCY`.`abbreviation`,
                                        `CURRENCY`.`status`,
                                        `CURRENCY`.`checkedOn`,
                                        `CURRENCY`.`createdOn`,
                                        `CURRENCY`.`createdBy`,
                                        `CURRENCY`.`modifiedOn`,
                                        `CURRENCY`.`modifiedBy`,
                                    FROM `conciergedb`.`CURRENCY`
                                    ";
                using (var conn = new MySqlConnection(_dbConn))
                {
                    return await conn.QueryAsync<Currency>(sql);

                }
            }
            catch (Exception e)
            {

                throw;
            }

        }

    }
}

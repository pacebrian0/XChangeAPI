using Dapper;
using MySqlConnector;
using XChangeAPI.Data.Interfaces;
using XChangeAPI.Models.DB;

namespace XChangeAPI.Data
{
    public class UserData: BaseDatabase, IUserData
    {
        private readonly ILogger _logger;

        public UserData(IConfiguration config, ILogger logger) : base(config)
        {
            _logger = logger;
        }


        public async Task<IEnumerable<User>> GetUsers()
        {
            try
            {
                const string sql = @"SELECT `User`.`id`,
                                        `User`.`name`,
                                        `User`.`surname`,
                                        `User`.`email`,
                                        `User`.`createdOn`,
                                        `User`.`createdBy`,
                                        `User`.`modifiedOn`,
                                        `User`.`modifiedBy`,
                                        `User`.`password_hash` as passwordHash
                                    FROM `conciergedb`.`User`
                                    ";
                using (var conn = new MySqlConnection(_dbConn))
                {
                    return await conn.QueryAsync<User>(sql);

                }
            }
            catch (Exception e)
            {

                throw;
            }

        }
        public async Task<User> GetUserById(int id)
        {
            try
            {
                const string sql = @"SELECT `User`.`id`,
                                        `User`.`name`,
                                        `User`.`surname`,
                                        `User`.`email`,
                                        `User`.`createdOn`,
                                        `User`.`createdBy`,
                                        `User`.`modifiedOn`,
                                        `User`.`modifiedBy`,
                                        `User`.`password_hash` as passwordHash
                                    FROM `conciergedb`.`User`
                                    WHERE id=@id
                                    ";
                using (var conn = new MySqlConnection(_dbConn))
                {
                    return await conn.QueryFirstOrDefaultAsync<User>(sql, new { id });

                }
            }
            catch (Exception e)
            {

                throw;
            }

        }

        public async Task<User> GetUserByEmail(string email)
        {
            try
            {
                const string sql = @"SELECT `User`.`id`,
                                        `User`.`name`,
                                        `User`.`surname`,
                                        `User`.`email`,
                                        `User`.`createdOn`,
                                        `User`.`createdBy`,
                                        `User`.`modifiedOn`,
                                        `User`.`modifiedBy`,
                                        `User`.`password_hash` as passwordHash
                                    FROM `conciergedb`.`User`
                                    WHERE email=@email
                                    ";
                using (var conn = new MySqlConnection(_dbConn))
                {
                    return await conn.QueryFirstOrDefaultAsync<User>(sql, new { email });

                }
            }
            catch (Exception e)
            {

                throw;
            }

        }

        public async Task<int> PostUser(User user)
        {
            try
            {
                const string sql = @"
                            INSERT INTO `conciergedb`.`User`
                            (
                            `name`,
                            `surname`,
                            `email`,
                            `createdOn`,
                            `createdBy`,
                            `modifiedOn`,
                            `modifiedBy`,
                            `password_hash`)
                            VALUES
                            (
                            @name,
                            @surname,
                            @email,
                            UTC_TIMESTAMP(),
                            0,
                            UTC_TIMESTAMP(),
                            0,
                            @passwordHash); 
                            SELECT id from `conciergedb`.`User` where email = @email;";

                using (var conn = new MySqlConnection(_dbConn))
                {
                    return await conn.ExecuteScalarAsync<int>(sql, new { user.name, user.surname, user.email, user.passwordhash });
                }


            }
            catch (Exception e)
            {

                throw;
            }

        }



        public async Task UpdateUser(User user, bool local)
        {
            try
            {
                const string sql = @"
                            UPDATE `conciergedb`.`User`
                            SET `name` = @name,
                                `surname` = @surname,
                                `email` = @email,
                                `modifiedOn` = UTC_TIMESTAMP(),
                                `modifiedBy` = 0
                            WHERE id = @id";
                using (var conn = new MySqlConnection(_dbConn))
                {
                    await conn.ExecuteAsync(sql, new { user.name, user.surname, user.email, user.id });
                }


            }
            catch (Exception e)
            {

                throw;
            }

        }

        public async Task DeleteUser(User user, bool local)
        {
            try
            {
                const string sql = @"
                            DELETE FROM `conciergedb`.`User`
                            WHERE id = @id";
                using (var conn = new MySqlConnection(_dbConn))
                {
                    await conn.ExecuteAsync(sql, new { user.id });
                }

            }
            catch (Exception e)
            {

                throw;
            }

        }
    }
}


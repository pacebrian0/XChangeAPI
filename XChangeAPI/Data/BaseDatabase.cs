﻿namespace XChangeAPI.Data
{
    public abstract class BaseDatabase
    {
        protected readonly string _dbConn;

        public BaseDatabase(IConfiguration configuration)
        {
            _dbConn = configuration.GetConnectionString("sqlConnString") ?? throw new Exception("Please set up connection strings");
        }
    }
}

using Dapper;
using Microsoft.Extensions.Configuration;
using RecipeRepositoryApi.Models;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace RecipeRepositoryApi.DataAccess
{
    public class SqlDbAccess : ISqlDbAccess
    {
        private readonly IConfiguration _config;

        public SqlDbAccess(IConfiguration config)
        {
            _config = config;
        }

        public async Task<IEnumerable<T>> LoadData<T, U>(string storedProceudure,
            U parameters, string connectionId = "Default")
        {
            using IDbConnection connection = new SqlConnection(_config.GetConnectionString(connectionId));

            return await connection.QueryAsync<T>(storedProceudure, parameters,
                commandType: CommandType.StoredProcedure);
        }


        public async Task<T> SaveData<T, U>(string storedProcedure, U parameters, string connectionId = "Default")
        {
            using IDbConnection connection = new SqlConnection(_config.GetConnectionString(connectionId));

            var res = await connection.QueryFirstOrDefaultAsync<T>(storedProcedure, parameters, commandType: CommandType.StoredProcedure);

            return res;

        }

        public async Task<User> GetEntityByEmail(string email)
        {
            using IDbConnection connection = new SqlConnection(_config.GetConnectionString("Default"));

            var res = await connection.QueryFirstOrDefaultAsync<User>("GetUserByEmail", new { Email = email }, commandType: CommandType.StoredProcedure);

            return res;
        }

        public async Task<int> SaveUserFavorite(int userId, int recipeId)
        {
            using IDbConnection connection = new SqlConnection(_config.GetConnectionString("Default"));

            var res = await connection.ExecuteScalarAsync<int>("AddUserFavorite", new { UserId = userId, RecipeID = recipeId }, commandType: CommandType.StoredProcedure);

            return res;
        }


    }


}


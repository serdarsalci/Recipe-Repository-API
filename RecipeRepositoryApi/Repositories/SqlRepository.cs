using RecipeRepositoryApi.DataAccess;
using RecipeRepositoryApi.DTOs;
using RecipeRepositoryApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RecipeRepositoryApi.Repositories
{
    public class SqlRepository : ISqlRepository
    {
        private readonly ISqlDbAccess _db;

        public SqlRepository(ISqlDbAccess db)
        {
            _db = db;
        }
        public async Task<IEnumerable<User>> GetAllUsers()
        {

            var res = await _db.LoadData<User, dynamic>("GetAllUsers", new { }, "Default");

            return res;
        }

        public async Task<User> RegisterUser(RegisterUserDto user)
        {
            var res = await _db.SaveData<User, RegisterUserDto>("SaveUser", user, "Default");

            return res;
        }


        public async Task<User> GetUserByEmail(string email)
        {
            var res = await _db.GetEntityByEmail(email);

            return res;

        }



        public async Task<IEnumerable<int>> GetUserFavorites(int id)
        {
            var res = await _db.LoadData<int, dynamic>("GetUserFavorites", new { Id = id }, "Default");
            return res;
        }

        public async Task<IEnumerable<int>> AddUserFavorite(int id, int recipeId)
        {
            var res = await _db.LoadData<int, dynamic>("AddUserFavorite", new { UserId = id, RecipeID = recipeId }, "Default");
            return res;
        }


    }
}

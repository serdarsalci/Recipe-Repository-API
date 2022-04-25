using RecipeRepositoryApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RecipeRepositoryApi.DataAccess
{
    public interface ISqlDbAccess
    {
        Task<IEnumerable<T>> LoadData<T, U>(string storedProceudure, U parameters, string connectionId = "Default");
        Task<T> SaveData<T, U>(string storedProcedure, U parameters, string connectionId = "Default");
        Task<User> GetEntityByEmail(string email);
        Task<int> SaveUserFavorite(int userId, int recipeId);

    }
}
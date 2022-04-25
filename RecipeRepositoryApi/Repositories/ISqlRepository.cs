using RecipeRepositoryApi.DTOs;
using RecipeRepositoryApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RecipeRepositoryApi.Repositories
{
    public interface ISqlRepository
    {
        Task<IEnumerable<User>> GetAllUsers();

        Task<User> RegisterUser(RegisterUserDto user);
        Task<User> GetUserByEmail(string email);
        Task<IEnumerable<int>> GetUserFavorites(int id);
        Task<IEnumerable<int>> AddUserFavorite(int id, int recipeId);
    }
}

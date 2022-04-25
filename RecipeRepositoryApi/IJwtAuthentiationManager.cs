using RecipeRepositoryApi.DTOs;
using RecipeRepositoryApi.Models;
using System.Threading.Tasks;

namespace RecipeRepositoryApi
{
    public interface IJwtAuthentiationManager
    {
        public Task<AuthenticateResponseDto> Authenticate(string userEmail, string password);
        public string GenerateToken(User user);
    }
}

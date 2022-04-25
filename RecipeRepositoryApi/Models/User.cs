namespace RecipeRepositoryApi.Models
{
    public class User
    {
        public int Id { get; init; }
        public string Email { get; init; }

        public string HashPassword { get; init; }
    }
}

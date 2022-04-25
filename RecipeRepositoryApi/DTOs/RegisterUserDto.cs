using System.ComponentModel.DataAnnotations;

namespace RecipeRepositoryApi.DTOs
{
    public class RegisterUserDto
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; init; }

        [Required]
        public string HashPassword { get; init; }
    }
}

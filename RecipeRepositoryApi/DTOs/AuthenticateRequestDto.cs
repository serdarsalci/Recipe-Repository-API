using System.ComponentModel.DataAnnotations;

namespace RecipeRepositoryApi.DTOs
{
    public class AuthenticateRequestDto
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; init; }

        [Required]
        public string Password { get; init; }

        //public string HashPassword { get; set; }
    }
}

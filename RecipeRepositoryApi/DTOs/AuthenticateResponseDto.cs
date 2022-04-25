namespace RecipeRepositoryApi.DTOs
{
    public class AuthenticateResponseDto
    {

        public int Id { get; set; }
        public string Email { get; set; }



        public string Token { get; set; }



        public AuthenticateResponseDto(int id, string email, string token)
        {
            Id = id;
            Email = email;
            Token = token;

        }
    }
}

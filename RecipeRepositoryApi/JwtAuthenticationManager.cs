using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RecipeRepositoryApi.DTOs;
using RecipeRepositoryApi.Models;
using RecipeRepositoryApi.Repositories;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace RecipeRepositoryApi
{
    public class JwtAuthenticationManager : IJwtAuthentiationManager
    {
        private ISqlRepository _repository;
        private object entity;
        private readonly IConfiguration _config;
        public JwtAuthenticationManager(ISqlRepository repository, IConfiguration config)
        {
            _repository = repository;
            _config = config;
        }



        //public string Authenticate(string userEmail, string password)
        //{
        //    return null;
        //}



        public async Task<AuthenticateResponseDto> Authenticate(string email, string password)
        {
            var user = await _repository.GetUserByEmail(email);

            if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.HashPassword))
            {
                return null;
            }

            var token = GenerateToken(user);

            return new AuthenticateResponseDto(user.Id, user.Email, token);
        }


        public string GenerateToken(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JwtConfig:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {

                new Claim(ClaimTypes.Email, user.Email),

                new Claim (ClaimTypes.Name, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Aud, _config["JwtConfig:Audience"]),
                new Claim(JwtRegisteredClaimNames.Iss, _config["JwtConfig:Issuer"])



            };

            var token = new JwtSecurityToken(issuer: _config["JwtConfig:Issuer"],
                                            audience: _config["JwtConfig:Audience"],
                                            claims: claims,
                                            expires: DateTime.Now.AddMinutes(600),
                                            signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);

        }


    }
}

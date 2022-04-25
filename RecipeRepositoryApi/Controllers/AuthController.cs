using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RecipeRepositoryApi.DTOs;
using RecipeRepositoryApi.Repositories;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace RecipeRepositoryApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ISqlRepository _sqlRepo;
        private readonly IJwtAuthentiationManager _jwtManager;

        public AuthController(ISqlRepository sqlRepo, IJwtAuthentiationManager jwtMan)
        {
            _sqlRepo = sqlRepo;
            _jwtManager = jwtMan;
        }


        [Authorize(Roles = "Admin")]
        [HttpGet("getall")]
        public async Task<IActionResult> GetAllUsers()
        {
            try
            {
                var res = await _sqlRepo.GetAllUsers();
                if (res == null) return NotFound();
                return Ok(res);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                return StatusCode(500, e.Message);
            }
        }


        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser([FromBody] AuthenticateRequestDto requestDto)
        {
            /// Todo check if email already exists 

            // hash password
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(requestDto.Password, BCrypt.Net.BCrypt.GenerateSalt());

            RegisterUserDto user = new()
            {
                Email = requestDto.Email,
                HashPassword = hashedPassword
            };

            var res = await _sqlRepo.RegisterUser(user);
            if (res == null)
            {
                return StatusCode(500);
            }

            // Generate jwt token
            var token = _jwtManager.GenerateToken(res);

            AuthenticateResponseDto autRes = new(res.Id, res.Email, token);

            return Ok(autRes);
        }


        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] AuthenticateRequestDto authRequest)
        {
            var response = await _jwtManager.Authenticate(authRequest.Email, authRequest.Password);

            if (response == null)
            {
                return BadRequest(new { message = "Email or password is incorrect" });
            }
            return Ok(response);
        }



    }
}

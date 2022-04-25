using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RecipeRepositoryApi.Repositories;
using System.Security.Claims;
using System.Threading.Tasks;

namespace RecipeRepositoryApi.Controllers
{





    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class RecipeController : ControllerBase
    {
        private readonly ISqlRepository _sqlRepo;
        public RecipeController(ISqlRepository sqlRepo)
        {
            _sqlRepo = sqlRepo;
        }

        [HttpGet]
        [Route("getfavorites")]
        public async Task<IActionResult> GetUserFavorites()
        {
            var userId = User.FindFirstValue(ClaimTypes.Name);
            var id = int.Parse(userId);
            if (userId == null) return Unauthorized();
            var res = await _sqlRepo.GetUserFavorites(id);
            return Ok(res);
        }

        [HttpPost]
        [Route("addFavorite/{id}")]
        public async Task<IActionResult> AddUserFavorite([FromRoute] string id)
        {
            var userId = User.FindFirstValue(ClaimTypes.Name);
            var user = int.Parse(userId);
            if (userId == null) return Unauthorized();
            var res = await _sqlRepo.AddUserFavorite(user, int.Parse(id));
            return Ok(res);
        }

    }
}

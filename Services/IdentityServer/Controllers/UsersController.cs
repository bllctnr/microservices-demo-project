using IdentityServer.Entities;
using IdentityServer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using static IdentityServer4.IdentityServerConstants;
using System.IdentityModel.Tokens.Jwt;

namespace IdentityServer.Controllers
{
    [Authorize(LocalApi.PolicyName)] // Claim based policy
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        public UsersController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(SignUp signUp) 
        {
            var user = new ApplicationUser
            {
                Email = signUp.Email,
                UserName = signUp.UserName,
                City = signUp.City
            };
            var result = await _userManager.CreateAsync(user, signUp.Password);
            if (!result.Succeeded)
            {
                return BadRequest(result.Errors.Select(error => error.Description).ToList());
            }
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetUser() 
        {
            var userIdClaim = User.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Sub);
            
            if (userIdClaim == null)
            {
                return BadRequest();
            }

            var user = await _userManager.FindByIdAsync(userIdClaim.Value);

            if (user == null) 
            {
                return BadRequest();
            }

            return Ok(new { Id = user.Id, UserName = user.UserName, Email = user.Email, City = user.City });

        }
    }
}

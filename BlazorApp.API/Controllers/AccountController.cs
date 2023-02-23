using AutoMapper;
using BlazorApp.API.Data;
using BlazorApp.API.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BlazorApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class AccountController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<AccountController> _logger;
        private readonly IConfiguration _configuration;

        public AccountController(IMapper mapper, UserManager<ApplicationUser> userManager, ILogger<AccountController> logger, IConfiguration configuration)
        {
            _mapper = mapper;
            _userManager = userManager;
            _logger = logger;
            _configuration = configuration;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            try
            {
                var user = _mapper.Map<ApplicationUser>(model);
                var result = await _userManager.CreateAsync(user, model.Password);
                if (!result.Succeeded)
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(error.Code, error.Description);
                    }
                    return BadRequest(ModelState);
                }

                await _userManager.AddToRoleAsync(user, model.Role ?? "Users");
                return Accepted();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ">>> " + nameof(Register));
                return Problem("Error Registering", statusCode: 500);
            }
        }

        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<AuthResponse>> Login(LoginModel model)
        {
            try
            {
                var user = await _userManager.FindByNameAsync(model.UserName);
                var isValidPassword = await _userManager.CheckPasswordAsync(user, model.Password);

                if (user == null || !isValidPassword)
                {
                    return Unauthorized(model);
                }

                var tokenString = await generateToken(user);

                var response = new AuthResponse
                {
                    UserId = user.Id,
                    //Email = user.Email,
                    Token = tokenString,
                    UserName = user.UserName,
                };

                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ">>> " + nameof(Login));
                return Problem("Error Login", statusCode: 500);
            }
        }

        private async Task<string> generateToken(ApplicationUser user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:Key"]!));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var roles = await _userManager.GetRolesAsync(user);
            var roleClaims = roles.Select(x => new Claim(ClaimTypes.Role, x)).ToList();

            var userClaims = await _userManager.GetClaimsAsync(user);

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                //new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(CustomClaimTypes.UserId, user.Id),
                new Claim(ClaimTypes.Name, user.UserName),
            }
            .Union(userClaims)
            .Union(roleClaims);

            var token = new JwtSecurityToken(
                issuer: _configuration["JwtSettings:Issuer"],
                audience: _configuration["JwtSettings:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(Convert.ToInt32(_configuration["JwtSettings:Duration"])),
                signingCredentials: credentials
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}

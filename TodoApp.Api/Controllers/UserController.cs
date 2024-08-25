using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TodoApp.Models;
using TodoApp.Service.Contracts;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Cors;
using System.Text;
using Google.Apis.Auth;
using TodoApp.Data.Entites;

namespace TodoApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowAll")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IAuthenticationService _authService;
        private readonly IConfiguration _configuration;

        public UserController(IUserService userService,
                 IAuthenticationService authService,
                 IConfiguration configuration
)
        {
            _userService = userService;
            _authService = authService;
            _configuration = configuration;

        }
        [HttpPost("/Register")]
        [Produces("application/json")]

        public async Task<ActionResult<TokenModel>> Register(UserDTO user)
        {
            if (await _userService.IsExistingUser(user.Username))
            {
                return Conflict();
            }
            ServiceResult res = await _userService.Register(user);
            if (res.IsSucceeded)
            {
                var token = await GenerateJWT(((User)res.Value).Id.ToString());
                return Ok(token);

            }
            else
            {
                return BadRequest(res.Message);
            }

        }
        [HttpPost("/Login")]
        public async Task<ActionResult<TokenModel>> Login([FromBody] LoginModel loginDetails)
        {
            ServiceResult loginResult = await _userService.Login(loginDetails);
            if (loginResult.IsSucceeded)
            {
                var token = await GenerateJWT((string)loginResult.Value);
                return Ok(token);
            }
            else
            {
                return BadRequest();
            }
        }
       
        [HttpPost("/Login/Google")]
        public async Task<ActionResult<TokenModel>> GoogleLogin([FromBody] TokenModel tokenDetails)
        {
            var payload = await VerifyGoogleToken(tokenDetails.Token);
            if (payload == null)
            {
                return BadRequest("Invalid Google token");
            }
            if (await _userService.IsExistingUser(payload.Email))
            {
                var result = await _userService.GetUserId(payload.Email);
                if (!result.IsSucceeded) { return  BadRequest(result.Message); }
                return await GenerateJWT((string)result.Value);
            }
            else
            {
                ServiceResult res = await _userService.RegisterGoogleDetails(payload);
                if (res.IsSucceeded)
                {
                    var token = await GenerateJWT((string)res.Value);
                    return Ok(token);

                }
                else
                {
                    return BadRequest(res.Message);
                }
            }

        }

        private async Task<GoogleJsonWebSignature.Payload> VerifyGoogleToken(string token)
        {
            var settings = new GoogleJsonWebSignature.ValidationSettings()
            {
                Audience = new List<string> { _configuration["Authentication:Google:ClientId"] }
            };

            var payload = await GoogleJsonWebSignature.ValidateAsync(token, settings);
            return payload;
        }
        private async Task<TokenModel> GenerateJWT(string userId)
        {
            ServiceResult userDetailsResult = await _userService.GetUserDetails(userId);
            if (!userDetailsResult.IsSucceeded) {
                throw new Exception("Cannot Fetch UserDetails");
            }
            UserView userdetails = (UserView)userDetailsResult.Value;
            var claims = new[] {
                        new Claim(ClaimTypes.Sid, userId),
                        new Claim(ClaimTypes.NameIdentifier, userdetails.Username),
                        new Claim(ClaimTypes.Email, userdetails.Email),
                        new Claim("Name",userdetails.Name),

                    };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWTSettings:JWT_Secret"]));
            var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                _configuration["JWTSettings:Issuer"],
                _configuration["JWTSettings:Audience"],
                claims,
                expires: DateTime.UtcNow.AddMinutes(100),
                signingCredentials: signIn);

                var jetToken=(new JwtSecurityTokenHandler().WriteToken(token)).ToString();
                 return  new TokenModel() { Token = jetToken };
        }
       
    }
}

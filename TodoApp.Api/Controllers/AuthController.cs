
//using Microsoft.AspNetCore.Cors;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.IdentityModel.Tokens;
//using System.IdentityModel.Tokens.Jwt;
//using System.Security.Claims;
//using System.Text;
//using TodoApp.Models;
//using TodoApp.Models.Entites;
//using TodoApp.Service.Contracts;

//namespace EmployeeApp.API.Controllers
//{
//    [Route("/LoginJWT")]
//    [ApiController]
//    [EnableCors("AllowAll")]
//    public class AuthController : Controller
//    {
//        private readonly IConfiguration _configuration;
//        private readonly IUserService _userServices;
//        public AuthController(IConfiguration configuration, IUserService userService)
//        {
//            _configuration = configuration;
//            _userServices = userService;
//        }
//        [HttpPost]
//        [Produces("text/plain")]
//        public async Task<ActionResult<string>> Login([FromBody] LoginModel loginDetails)
//        {
//            if (await _userServices.GetUser(loginDetails.Username)!=null)
//            {
//                User user = _userServices.GetUser(loginDetails.Username).Result;
//                var claims = new[] {
//                        new Claim("UserId", user.Id.ToString()),
//                        new Claim("UserName", user.UserName),
//                        //new Claim("Email", user.Email),
//                        //new Claim("Password", user.Password),
//                        //new Claim("Role",user.Role)
//                    };
//                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWTSettings:JWT_Secret"]));
//                var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
//                var token = new JwtSecurityToken(
//                    _configuration["JWTSettings:Issuer"],
//                    _configuration["JWTSettings:Audience"],
//                    claims,
//                    expires: DateTime.UtcNow.AddMinutes(100),
//                    signingCredentials: signIn);

//                return Ok((new JwtSecurityTokenHandler().WriteToken(token)).ToString());
//            }
//            else
//            {
//                return BadRequest();
//            }
//        }
//    }
//}

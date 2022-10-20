using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UniversityApiBackend.Helpers;
using UniversityApiBackend.Models.DataModels;

namespace UniversityApiBackend.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly JwtSettings _jwtSettings;

        public AccountController(JwtSettings jwtSettings)
        {
            _jwtSettings = jwtSettings;
        }

        public IEnumerable<User> Logins = new List<User>() {
            new User(){
                Id = 1,
                Email = "admin@mail.com",
                Name = "Admin",
                Password = "Password"
            },
            new User(){
                Id = 1,
                Email = "user1@mail.com",
                Name = "User1",
                Password = "Juan"
            }
        };

        [HttpPost]
        public IActionResult GetToken(UserLogins userLogin)
        {
            try
            {
                var token = new UserTokens();
                var valid = Logins.Any(user => user.Name.Equals(userLogin.UserName, StringComparison.OrdinalIgnoreCase));

                if (valid)
                {
                    var user = Logins.FirstOrDefault(user => user.Name.Equals(userLogin.UserName, StringComparison.OrdinalIgnoreCase));

                    token = JwtHelpers.GetTokenKey(new UserTokens()
                    {
                        UserName = user.Name,
                        EmailId = user.Email,
                        Id = user.Id,
                        GuidId = Guid.NewGuid()
                    }, _jwtSettings);
                }
                else {
                    return BadRequest("Wrong credentials");
                }
                return Ok(token);
            }
            catch (Exception ex) {
                throw new Exception("GetToken Error", ex);
            }
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrator")]
        public IActionResult GetUserList() 
        {
            return Ok(Logins);
        }
    }
}

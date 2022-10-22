using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using UniversityApiBackend.DataAccess;
using UniversityApiBackend.Helpers;
using UniversityApiBackend.Models.DataModels;

namespace UniversityApiBackend.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IStringLocalizer<AccountController> _stringLocalizer;

        private readonly UniversityDBContext _context;

        private readonly ILogger<AccountController> _logger;

        private readonly JwtSettings _jwtSettings;

        public AccountController(JwtSettings jwtSettings, 
            UniversityDBContext context, 
            IStringLocalizer<AccountController> stringLocalizer,
            ILogger<AccountController> logger)
        {
            _context = context;
            _jwtSettings = jwtSettings;
            _stringLocalizer = stringLocalizer;
            _logger = logger;
        }

        //public IEnumerable<User> Logins = new List<User>() {
        //    new User(){
        //        Id = 1,
        //        Email = "admin@mail.com",
        //        Name = "Admin",
        //        Password = "Password"
        //    },
        //    new User(){
        //        Id = 1,
        //        Email = "user1@mail.com",
        //        Name = "User1",
        //        Password = "Juan"
        //    }           
        //};

        [HttpPost]
        public async Task<IActionResult> GetToken(UserLogins userLogin)
        {
            try
            {
                var token = new UserTokens();
                
                var user = await _context.Users
                    .Include(p => p.Roles)
                    .FirstOrDefaultAsync(user =>
                        user.Name == userLogin.UserName
                        );

                var valid = user.Password == userLogin.Password;
                //var valid = Logins.Any(user => user.Name.Equals(userLogin.UserName, StringComparison.OrdinalIgnoreCase));

                if (valid)
                {
                    //var user = Logins.FirstOrDefault(user => user.Name.Equals(userLogin.UserName, StringComparison.OrdinalIgnoreCase));
                    var greetings = _stringLocalizer["Greetings"];

                    token = JwtHelpers.GetTokenKey(new UserTokens()
                    {
                        UserName = user.Name,
                        EmailId = user.Email,
                        Id = user.Id,
                        GuidId = Guid.NewGuid(),
                        Roles = user.Roles.Select(p => p.Name).ToArray(),
                        Greetings = $"{greetings} {user.Name}",
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
        public async Task<IActionResult> GetUserList() 
        {
            var Logins = await _context.Users.ToListAsync();
            return Ok(Logins);
        }
    }
}

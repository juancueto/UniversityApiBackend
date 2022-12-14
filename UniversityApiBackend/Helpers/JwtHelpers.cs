using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using UniversityApiBackend.Models.DataModels;

namespace UniversityApiBackend.Helpers
{
    public static class JwtHelpers
    {
        public static IEnumerable<Claim> GetClaims(this UserTokens userAccounts, Guid id) 
        { 
            List<Claim> claims = new List<Claim>() 
            {
                new Claim("Id", userAccounts.Id.ToString()),
                new Claim(ClaimTypes.Name, userAccounts.UserName),
                new Claim(ClaimTypes.Email, userAccounts.EmailId),
                new Claim(ClaimTypes.NameIdentifier, id.ToString()),
                new Claim(ClaimTypes.Expiration, DateTime.UtcNow.AddDays(1).ToString("MMM ddd dd yyyy HH:mm:ss tt"))
            };

            foreach (var role in userAccounts.Roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            } 

            //if (userAccounts.UserName == "Admin")
            //{
            //    claims.Add(new Claim(ClaimTypes.Role, "Administrator"));
            //}
            //else {
            //    claims.Add(new Claim(ClaimTypes.Role, "User"));
            //    claims.Add(new Claim("UserOnly", "User 1"));
            //}

            return claims;
        }

        public static IEnumerable<Claim> GetClaims(this UserTokens userAccounts, out Guid id) 
        {
            id = Guid.NewGuid();
            return GetClaims(userAccounts, id);
        }

        public static UserTokens GetTokenKey(UserTokens model, JwtSettings jwtSettings) 
        {
            try
            {
                var userToken = new UserTokens();
                if (model == null)
                    throw new ArgumentNullException(nameof(model));

                // Obtain SECRET KEY
                var key = System.Text.Encoding.ASCII.GetBytes(jwtSettings.IssuerSigningkey);

                Guid Id;
                // Expire in 1 day
                DateTime expireTime = DateTime.UtcNow.AddDays(1);

                // Validity
                userToken.Validity = expireTime.TimeOfDay;

                // Generate JWT
                var jwt = new JwtSecurityToken(
                    issuer: jwtSettings.ValidIssuer,
                    audience: jwtSettings.ValidAudience,
                    claims: GetClaims(model, out Id),
                    notBefore: new DateTimeOffset(DateTime.Now).DateTime,
                    expires: new DateTimeOffset(expireTime).DateTime,
                    signingCredentials: new SigningCredentials(
                            new SymmetricSecurityKey(key),
                            SecurityAlgorithms.HmacSha256
                        )
                    );

                userToken.Token = new JwtSecurityTokenHandler().WriteToken(jwt);
                userToken.UserName = model.UserName;
                userToken.Id = model.Id;
                userToken.GuidId = Id;
                userToken.Roles = model.Roles;
                userToken.Greetings = model.Greetings;
                return userToken;
            }
            catch (Exception ex) 
            {
                throw new Exception("Error generating JWT", ex);
            }
        }
    }
}

using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using PharmaSupplyAuthorizationApi.Model;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace PharmaSupplyAuthorizationApi.Repository
{
    public class AuthUserRepository : IAuthUser
    {
        private IConfiguration _config;
        static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(AuthUserRepository));

        private readonly Dictionary<string, string> users = new Dictionary<string, string>() {
            { "admin","admin"},
            {"admin2","admin2" }
        };
        public AuthUserRepository(IConfiguration config)
        {
          //  _log4net.Info("AuthenticationRepository constructor initiated.");
            _config = config;
        }
        public string GenerateToken(Users user)
        {
            try
            {
                _log4net.Info("Token generation started");
                if (!users.Any(u => u.Key == user.Email && u.Value == user.Password))
                {
                    _log4net.Info("User Credentials are not correct so token cannot be generated.");
                    return null;
                }
                _log4net.Info("User Credentials are correct.");
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_config["Jwt:Key"]);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim(ClaimTypes.Name, user.Email)
                    }),
                    Expires = DateTime.UtcNow.AddMinutes(30),
                    SigningCredentials = new SigningCredentials(
                        new SymmetricSecurityKey(key),
                        SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                _log4net.Info("Token is generated successfully.");
                return tokenHandler.WriteToken(token);
            }
            catch (Exception exception)
            {
                _log4net.Error("Exception found while generating the token " + exception.Message);
                return null;
            }
        }
    }
}

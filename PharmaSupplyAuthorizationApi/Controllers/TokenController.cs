using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PharmaSupplyAuthorizationApi.Model;
using PharmaSupplyAuthorizationApi.Service;

namespace PharmaSupplyAuthorizationApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly IAuthUserService _service;

        static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(TokenController));
        public TokenController(IAuthUserService service)
        {
            _service = service;
        }
        [HttpPost]
        [Route("AuthenticateUser")]
        public IActionResult AuthenticateUser(Users user)
        {
            

            try
            {
                var token = _service.AuthenticationUser(user);
                if (string.IsNullOrEmpty(token))
                {
                    _log4net.Info("Unauthorized User.");
                    return Unauthorized();
                }
                _log4net.Info("Authorized User.");
                return Ok(new { tokenString = token });
            }
            catch (Exception exception)
            {
                _log4net.Error("Exception found while authenticating the user=" + exception.Message);
                return new StatusCodeResult(500);

            }
        }

    }
    
}

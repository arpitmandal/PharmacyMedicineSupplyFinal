using PharmaSupplyAuthorizationApi.Model;
using PharmaSupplyAuthorizationApi.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PharmaSupplyAuthorizationApi.Service
{
    public class AuthUserService : IAuthUserService
    {

        private readonly IAuthUser _repo;

        static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(AuthUserService));

        public AuthUserService(IAuthUser repo)
        {
            _log4net.Info("Authuserservice constructor initiated");
            _repo = repo;
            
        }

        public string AuthenticationUser(Users user)
        {
            string token = null;
            try
            {
                _log4net.Info("In AuthenticationUser method of the service .Repository GenerateToken function is called.");
                token = _repo.GenerateToken(user);
                _log4net.Info("Token string is returned to the Controller.");
            }
            catch (Exception exception)
            {
                _log4net.Error("Exception found while returning the token to controller =" + exception.Message);
            }
            return token;
        }
    }
}

using PharmaSupplyAuthorizationApi.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PharmaSupplyAuthorizationApi.Service
{
    public interface IAuthUserService
    {
        public string AuthenticationUser(Users user );
    }
}

using PharmaSupplyAuthorizationApi.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PharmaSupplyAuthorizationApi.Repository
{
   public  interface IAuthUser
    {
        public string GenerateToken(Users user);
    }
}

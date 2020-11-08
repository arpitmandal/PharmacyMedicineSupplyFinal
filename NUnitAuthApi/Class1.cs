using AuthApi;
using AuthApi.Controllers;
using AuthApi.Repository;
using AuthApi.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Moq;
using NUnit.Framework;

namespace NUnitAuthApi
{
    class Class1
    {
      // AuthUserService authService;
        Mock<IConfiguration> config = new Mock<IConfiguration>();
        Mock<IAuthUser> authRepo;
        [SetUp]
        public void Setup()
        {
            Users user = new Users() { Email = "admin", Password = "admin" };
            string token = "abc123";
            authRepo = new Mock<IAuthUser>();
            authRepo.Setup(s => s.UserLogin(user)).Returns(token);
            authRepo.Setup(u=>u.)
          //  authService = new AuthUserService(authRepo.Object);
       
        }

        [Test]
        public void Test1()
        {
            Users newUser = new Users() { Email = "admin", Password = "admin" };
            IAuthUser aur = authRepo.Object;
            var data = aur.UserLogin(newUser);
            Assert.IsNotNull(data);
        }
    }
}

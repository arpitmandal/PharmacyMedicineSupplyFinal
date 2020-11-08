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
    public class Tests
    {

        Mock<IAuthUserService> authService;


        Mock<TokenController> con;
         
            
            TokenController controller;
        Mock<IConfiguration> config = new Mock<IConfiguration>();
        Mock<IAuthUser> authRepo;
        [SetUp]
        public void Setup()
        {
           

            authService = new Mock<IAuthUserService>(authRepo.Object);

            Users user = new Users() { Email="admin",Password="admin" };

            authService.Setup(s => s.UserLoginService(user)).Returns(user);

            con = new Mock<TokenController>(config.Object, authService.Object);
           
            //controller = new TokenController(config.Object, authService.Object);
        }

        [Test]
        public void Test1()
        {

            Users newUser = new Users() { Email="admin", Password="admin"};
            var data = controller.Login(newUser);
            Assert.IsNotNull(data);
        }
    }
}
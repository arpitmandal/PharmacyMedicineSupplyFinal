using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Moq;
using NUnit.Framework;
using PharmaSupplyAuthorizationApi.Controllers;
using PharmaSupplyAuthorizationApi.Model;
using PharmaSupplyAuthorizationApi.Repository;
using PharmaSupplyAuthorizationApi.Service;
using System;

namespace AuthApiUnitTest
{
    public class Tests
    {
        private Mock<IConfiguration> _config;
        private IAuthUser _repository;
        private IAuthUserService _service;
        private TokenController _controller;
        Users user1;
        Users user2;

        [SetUp]
        public void Setup()
        {
            _config = new Mock<IConfiguration>();
            _config.Setup(c => c["Jwt:Key"]).Returns("ThisismySecretKey");
            _repository = new AuthUserRepository(_config.Object);
            _service = new AuthUserService(_repository);
            _controller = new TokenController(_service);
            user1 = new Users()
            {
                Email = "admin",
                Password = "admin"
            };
            user2 = new Users()
            {
                Email = "Admi",
                Password = "admin5"
            };


        }

        [Test]
        public void AuthenticateUserControllerPositiveTest()
        {
            var result = _controller.AuthenticateUser(user1);
            Assert.IsNotNull(result);
        }

        [Test]
        public void AuthenticateUserControllerNegativeTest()
        {

            try
            {
                var result = _controller.AuthenticateUser(user2);
                var response = result as ObjectResult;
                Assert.AreEqual(401, response.StatusCode);
            }
            catch (Exception e)
            {
                Assert.AreEqual("Object reference not set to an instance of an object.", e.Message);
            }
        }

        [Test]
        public void AuthenticateUserServicePositiveTest()
        {
            var result = _service.AuthenticationUser(user1);
            Assert.IsNotNull(result);
        }

        [Test]
        public void AuthenticateUserServiceNegativeTest()
        {
            var result = _service.AuthenticationUser(user2);
            Assert.IsNull(result);
        }

        [Test]
        public void GenerateTokenRepositoryPositiveTest()
        {
            var result = _repository.GenerateToken(user1);
            Assert.IsNotNull(result);
        }

        [Test]
        public void GenerateTokenRepositoryNegativeTest()
        {
            var result = _repository.GenerateToken(user2);
            Assert.IsNull(result);
        }
    }
}

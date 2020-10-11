using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RedBrain.Authentication.Controllers;
using RedBrain.Authentication.Models.Users;
using System;

namespace RedBrain.Authentication.Tests
{
    [TestClass]
    public class UsersControllerTests : BaseTest
    {
        readonly UsersController _controller;
        readonly int _testRunId;
        
        public UsersControllerTests()
        {
            _controller = new UsersController(UserService, Mapper, AppSettings);
            _testRunId = new Random().Next();
        }

        [TestMethod]
        public void UserRegistrationTest()
        {
            //arrange
            var model = GetRegisterModel(_testRunId);

            //act
            var result = _controller.Register(model);
            var okResult = result as OkResult;

            //assert
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
        }

        [TestMethod]
        public void UserLoginTest()
        {
            UserRegistrationTest();
            var registerModel = GetRegisterModel(_testRunId);
            var response = _controller.Authenticate(new AuthenticateModel
            {
                Password = registerModel.Password,
                Username = registerModel.Username,
                Tenant = registerModel.Tenant
            });
            var okObjectResult = (response as OkObjectResult);

            //assert
            Assert.IsNotNull(okObjectResult);
            Assert.AreEqual(200, okObjectResult.StatusCode);
            Assert.IsNotNull(okObjectResult.Value);

            var authResult = okObjectResult.Value as AuthenticationResultModel;
            Assert.IsNotNull(authResult);

            Assert.IsNotNull(authResult.Id);
            Assert.AreEqual(registerModel.Username, authResult.Username);
            Assert.IsNotNull(authResult.Token);
            Assert.AreEqual("Bearer", authResult.TokenType);
            Assert.AreEqual(registerModel.Tenant, authResult.Tenant);
            Assert.AreEqual(registerModel.FirstName, authResult.FirstName);
            Assert.AreEqual(registerModel.LastName, authResult.LastName);
            Assert.AreEqual(registerModel.Mobile, authResult.Mobile);
            Assert.AreEqual(registerModel.Email, authResult.Email);
        }

        #region Helper methods

        private RegisterModel GetRegisterModel(int testRunId)
        {
            return new RegisterModel
            {
                Username = $"saikat.adak.{testRunId}",
                Email = "saikat@adak.com",
                FirstName = "Saikat",
                LastName = "Adak",
                Mobile = "1234567890",
                Tenant = "UnitTest",
                Password = "p12390"
            };
        }

        #endregion

    }
}

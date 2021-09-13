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
        readonly UsersController _usersController;
        protected readonly int _testRunId;
        
        public UsersControllerTests()
        {
            _usersController = new UsersController(UserService, Mapper, AppSettings);
            _testRunId = new Random().Next();
        }

        [TestMethod]
        public void UserRegistrationTest()
        {
            //arrange
            var model = GetRegisterModel(_testRunId);

            //act
            var result = _usersController.Register(model);
            var okResult = result as OkResult;

            //assert
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
        }

        #region Helper methods

        protected RegisterModel GetRegisterModel(int testRunId)
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

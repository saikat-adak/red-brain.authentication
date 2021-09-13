using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RedBrain.Authentication.Controllers;
using RedBrain.Authentication.Models.Users;

namespace RedBrain.Authentication.Tests
{
    [TestClass]
    public class SessionsControllerTests : UsersControllerTests
    {
        readonly SessionsController _sessionsController;
        
        public SessionsControllerTests()
        {
            _sessionsController = new SessionsController(UserService, Mapper, AppSettings);
        }

        [TestMethod]
        public void UserLoginTest()
        {
            UserRegistrationTest();
            var registerModel = GetRegisterModel(_testRunId);
            var response = _sessionsController.Authenticate(new AuthenticateModel
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

            //var token = authResult.Token;

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

    }
}

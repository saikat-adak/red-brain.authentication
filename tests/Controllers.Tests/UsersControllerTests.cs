using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RedBrain.Authentication.Controllers;
using RedBrain.Authentication.Models.Users;

namespace RedBrain.Authentication.Tests
{
    [TestClass]
    public class UsersControllerTests : BaseTest
    {
        [TestMethod]
        public void UserRegistrationTest()
        {
            //arrange
            var controller = new UsersController(UserService, Mapper, AppSettings);
            var registerModel = new RegisterModel
            {
                Username = "saikat.adak",
                Email = "saikat@adak.com",
                FirstName="Saikat",
                LastName = "Adak",
                Mobile="1234567890",
                Tenant = "UnitTest",
                Password = "p12390"
            };

            //act
            var result = controller.Register(registerModel);
            var okResult = result as OkResult;

            //assert
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
        }
    }
}

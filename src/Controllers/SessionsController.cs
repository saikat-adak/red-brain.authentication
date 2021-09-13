using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using RedBrain.Authentication.Entities;
using RedBrain.Authentication.Helpers;
using RedBrain.Authentication.Models.Users;
using RedBrain.Authentication.Services;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace RedBrain.Authentication.Controllers
{
    [Authorize]
    [ApiController]
    [Route("v1/[controller]")]
    public class SessionsController : ControllerBase
    {
        #region Private fields

        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly AppSettings _appSettings;

        #endregion

        #region ctors

        public SessionsController(
            IUserService userService,
            IMapper mapper,
            IOptions<AppSettings> appSettings)
        {
            _userService = userService;
            _mapper = mapper;
            _appSettings = appSettings.Value;
        }

        #endregion

        #region Public methods

        [AllowAnonymous]
        [HttpPost()]
        public IActionResult Authenticate([FromBody] AuthenticateModel model)
        {
            User user = _userService.Authenticate(model.Username, model.Password, model.Tenant);

            if (user == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            byte[] key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = _appSettings.Host,
                Audience = _appSettings.Host,
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor); //TODO: why token signature is not valid
            string tokenString = tokenHandler.WriteToken(token);

            //TODO: insert a row in Sessions table

            return Ok(new AuthenticationResultModel
            {
                Id = user.Id,
                Username = user.Username,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Mobile = user.Mobile,
                Tenant = user.Tenant,
                TokenType = "Bearer",
                Token = tokenString
            });
        }

        [HttpGet()]
        public IActionResult AuthenticateToken()
        {
            User user = GetLoggedInUser();

            if (user == null)
                return BadRequest(new { message = "No user found in the database" });

            // TODO: log that a token with user id abc validated

            var userModel = _mapper.Map<UserModel>(user);
            return Ok(userModel);
        }

        [HttpDelete()]
        public IActionResult LogOut()
        {
            throw new NotImplementedException();
            //TODO: There should be a sessions table and remove the token from that table during log out
        }

        #endregion

        #region Helper methods

        private User GetLoggedInUser()
        {
            int userId = int.Parse(this.User.Identities.FirstOrDefault().Name);
            return _userService.GetById(userId);
        }

        private bool IsTenantMatching(int id)
        {
            return _userService.GetById(id).Tenant == GetLoggedInUser().Tenant;
        }

        #endregion

    }
}

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
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace RedBrain.Authentication.Controllers
{
    [Authorize]
    [ApiController]
    [Route("v1/[controller]")]
    public class UsersController : ControllerBase
    {
        #region Private fields

        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly AppSettings _appSettings;

        #endregion

        #region ctors

        public UsersController(
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
        [HttpPost]
        public IActionResult Register([FromBody] RegisterModel model)
        {
            // map model to entity
            User user = _mapper.Map<User>(model);

            try
            {
                _userService.Create(user, model.Password);
                return Ok(); //TODO: convert it to 201
            }
            catch (AppException ex)
            {
                // return error message if there was an exception
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            IEnumerable<User> users = _userService.GetAll(GetLoggedInUser().Tenant);
            IList<UserModel> model = _mapper.Map<IList<UserModel>>(users);
            return Ok(model);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            //check if the provided user id belongs to currently loggedin user's tenant
            if (!IsTenantMatching(id)) return Unauthorized("Not authorized to access this user");

            User user = _userService.GetById(id);
            UserModel model = _mapper.Map<UserModel>(user);
            return Ok(model);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] UpdateModel model)
        {
            //check if the provided user id belongs to currently loggedin user's tenant
            if (!IsTenantMatching(id)) return Unauthorized("Not authorized to modify this user");

            // map model to entity and set id
            User user = _mapper.Map<User>(model);
            user.Id = id;

            try
            {
                _userService.Update(user, model.Password);
                return Ok();
            }
            catch (AppException ex)
            {
                // return error message if there was an exception
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            //check if the provided user id belongs to currently loggedin user's tenant
            if (!IsTenantMatching(id)) return Unauthorized("Not authorized to delete this user");

            _userService.Delete(id);
            return Ok();
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

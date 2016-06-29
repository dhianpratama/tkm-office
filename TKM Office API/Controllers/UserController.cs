using System;
using System.Linq;
using System.Web.Http;
using Core;
using Core.Models.Master;
using Core.Services;
using TKM_Office_API.RequestParam;

namespace TKM_Office_API.Controllers
{
    public class UserController : BaseController
    {
        private readonly IUserService _userService;
        private readonly ISecurityService _securityService;

        public UserController(IUserService userService, ISecurityService securityService)
        {
            _userService = userService;
            _securityService = securityService;
        }

        [AllowAnonymous]
        public IHttpActionResult Register(UserRegistration userRegistration)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (_userService.Exist(userRegistration.UserName)) return BadRequest("User already exist.");
            _userService.CreateUser(userRegistration.UserName, userRegistration.Password);
            return Ok();
        }

        [Authorize]
        public IHttpActionResult GetCurrentUser()
        {
            return Ok(_securityService.GetCurrentUser());
        }

        [Authorize]
        public IHttpActionResult GetCurrentDefaultInstitution()
        {
            return Ok(_securityService.GetCurrentUserDefaultInstitution());
        }

        [Authorize]
        public IHttpActionResult FetchAllWithPagination(BaseSearchQueryModel searchQuery)
        {
            try
            {
                var data = _userService.FetchAllWithPagination(ref searchQuery);
                return Ok(new PageResponseInfo()
                {
                    Data = data.ToList<object>(),
                    TotalData = searchQuery.TotalData
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        public IHttpActionResult CreateNewUser(MasterUserCreate data)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                _userService.CreateNewUser(data.UserName, data.FullName, data.Password);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        public IHttpActionResult UpdateUserData(MasterUserUpdate data)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                _userService.UpdateUserData(data.UserId, data.FullName, data.DefaultInstitutionId, data.Institutions.ToList());
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpPost]
        public IHttpActionResult Delete(MasterUser data)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                _userService.Delete(data);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        public IHttpActionResult ChangePassword(UserChangePassword data)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                _userService.ChangePassword(data.UserName, data.OldPassword, data.Password);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        public IHttpActionResult ResetPassword(UserResetPassword data)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                _userService.ResetPassword(data.UserName, data.Password);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

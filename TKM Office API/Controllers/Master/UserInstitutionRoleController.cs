using System;
using System.Linq;
using System.Web.Http;
using Core;
using Core.Models.Master;
using Core.Services.Master;

namespace TKM_Office_API.Controllers.Master
{
    public class UserInstitutionRoleController : BaseController
    {
        private readonly IUserInstitutionRoleService _userInstitutionRoleService;

        public UserInstitutionRoleController(IUserInstitutionRoleService userInstitutionRoleService)
        {
            _userInstitutionRoleService = userInstitutionRoleService;
        }

        [Authorize]
        public IHttpActionResult Save(MasterUserInstitutionRole data)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                _userInstitutionRoleService.Save(data);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpPost]
        public IHttpActionResult Delete(MasterUserInstitutionRole data)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                _userInstitutionRoleService.Delete(data);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        public IHttpActionResult FetchAll()
        {
            try
            {
                return Ok(_userInstitutionRoleService.FetchAll());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        public IHttpActionResult FetchOne(long userInstitutionRoleId)
        {
            try
            {
                return Ok(_userInstitutionRoleService.FetchOne(userInstitutionRoleId));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        public IHttpActionResult FetchAllWithPagination(BaseSearchQueryModel searchQuery, long userId)
        {
            try
            {
                var data = _userInstitutionRoleService.FetchAllWithPagination(ref searchQuery, userId);
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
        public IHttpActionResult FetchAllByUserAndInstitution(long userId, long institutionId)
        {
            try
            {
                var data = _userInstitutionRoleService.FetchAllByUserAndInstitution(userId, institutionId);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
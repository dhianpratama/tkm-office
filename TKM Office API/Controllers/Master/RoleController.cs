using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Core;
using Core.Models.Master;
using Core.Services.Master;

namespace TKM_Office_API.Controllers.Master
{
    public class RoleController : BaseController
    {
        private readonly IRoleService _roleService;

        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        [Authorize]
        public IHttpActionResult Save(MasterRole data)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                _roleService.Save(data);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpPost]
        public IHttpActionResult Delete(MasterRole data)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                _roleService.Delete(data);
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
                return Ok(_roleService.FetchAll());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        public IHttpActionResult FetchOne(long uomId)
        {
            try
            {
                return Ok(_roleService.FetchOne(uomId));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        public IHttpActionResult FetchAllWithPagination(BaseSearchQueryModel searchQuery)
        {
            try
            {
                var data = _roleService.FetchAllWithPagination(ref searchQuery);
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
    }
}
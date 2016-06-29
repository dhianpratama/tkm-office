using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Core.Models.Sys;
using Core.Services.Sys;

namespace TKM_Office_API.Controllers.Sys
{
    public class SysConfigurationController : BaseController
    {
        private readonly ISysConfigurationService _configurationService;

        public SysConfigurationController(ISysConfigurationService configurationService)
        {
            _configurationService = configurationService;
        }

        public IHttpActionResult Save(SysConfiguration data)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                _configurationService.Save(data);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        public IHttpActionResult BulkSave(List<SysConfiguration> data)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                _configurationService.BulkSave(data);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        public IHttpActionResult FetchAll()
        {
            try
            {
                return Ok(_configurationService.FetchAll());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        public IHttpActionResult GetConfig(string configKey)
        {
            try
            {
                return Ok(_configurationService.GetConfig(configKey));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
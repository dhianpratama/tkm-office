using System;
using System.Linq;
using System.Web.Http;
using Core;
using Core.Models.Master;
using Core.Services.Master;
using System.Collections.Generic;

namespace TKM_Office_API.Controllers.Master
{
    public class BinsController : BaseController
    {
        private readonly IBinService _binService;

        public BinsController(IBinService binService)
        {
            _binService = binService;
        }

        [Authorize]
        public IHttpActionResult Save(MasterBin data)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                _binService.Save(data);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        public IHttpActionResult SaveList(List<MasterBin> data)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                _binService.Save(data);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpPost]
        public IHttpActionResult Delete(MasterBin data)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                _binService.Delete(data);
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
                return Ok(_binService.FetchAll());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        public IHttpActionResult FetchAllByReaderModule(MasterReaderModule readerModule)
        {
            try
            {
                return Ok(_binService.FetchAllByReaderModule(readerModule.ReaderModuleId));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        public IHttpActionResult FetchOne(long shelveId)
        {
            try
            {
                return Ok(_binService.FetchOne(shelveId));
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
                var data = _binService.FetchAllWithPagination(ref searchQuery);
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

        
        public IHttpActionResult FetchBin(MasterBinQuery query)
        {
            try
            {
                return Ok(_binService.FetchOne(query));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
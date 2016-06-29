using System;
using System.Linq;
using System.Web.Http;
using Core;
using Core.Models.Master;
using Core.Services.Master;

namespace TKM_Office_API.Controllers.Master
{
    public class ReaderModuleController : BaseController
    {
        private readonly IReaderModuleService _readerModuleService;

        public ReaderModuleController(IReaderModuleService readerModuleService)
        {
            _readerModuleService = readerModuleService;
        }

        [Authorize]
        public IHttpActionResult Save(MasterReaderModule data)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                _readerModuleService.Save(data);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpPost]
        public IHttpActionResult Delete(MasterReaderModule data)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                _readerModuleService.Delete(data);
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
                return Ok(_readerModuleService.FetchAll());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        public IHttpActionResult FetchAllByShelve(MasterShelve shelve)
        {
            try
            {
                return Ok(_readerModuleService.FetchAllByShelve(shelve.ShelveId));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        public IHttpActionResult FetchOneByCode(MasterReaderModule module)
        {
            try
            {
                return Ok(_readerModuleService.FetchOne(module.ReaderModuleCode));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        public IHttpActionResult FetchOne(long readerModuleId)
        {
            try
            {
                return Ok(_readerModuleService.FetchOne(readerModuleId));
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
                var data = _readerModuleService.FetchAllWithPagination(ref searchQuery);
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
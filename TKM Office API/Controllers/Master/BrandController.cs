using System;
using System.Linq;
using System.Web.Http;
using Core;
using Core.Models.Master;
using Core.Services.Master;

namespace TKM_Office_API.Controllers.Master
{
    public class BrandController : BaseController
    {
        private readonly IBrandService _brandService;

        public BrandController(IBrandService brandService)
        {
            _brandService = brandService;
        }

        [Authorize]
        public IHttpActionResult Save(MasterBrand data)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                _brandService.Save(data);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpPost]
        public IHttpActionResult Delete(MasterBrand data)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                _brandService.Delete(data);
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
                return Ok(_brandService.FetchAll());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        public IHttpActionResult FetchOne(long brandId)
        {
            try
            {
                return Ok(_brandService.FetchOne(brandId));
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
                var data = _brandService.FetchAllWithPagination(ref searchQuery);
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
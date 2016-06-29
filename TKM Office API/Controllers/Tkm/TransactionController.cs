using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Core;
using Core.Models.Tkm;
using Core.Services;
using Core.Services.Tkm;

namespace TKM_Office_API.Controllers.Tkm
{
    public class TransactionController : BaseController
    {
        private readonly ITransactionService _transactionService;

        public TransactionController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        [Authorize]
        public IHttpActionResult Save(TkmTransaction data)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                _transactionService.Save(data);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpPost]
        public IHttpActionResult Delete(TkmTransaction data)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                _transactionService.Delete(data);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        public IHttpActionResult FetchOne(long transactionId)
        {
            try
            {
                return Ok(_transactionService.FetchOne(transactionId));
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
                var data = _transactionService.FetchAllWithPagination(ref searchQuery);
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
        public IHttpActionResult GenerateReferenceNumber()
        {
            try
            {
                var refNum = _transactionService.GenerateReferenceNumber();
                return Ok(new Dictionary<string, string>()
                {
                    {"Data", refNum},
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
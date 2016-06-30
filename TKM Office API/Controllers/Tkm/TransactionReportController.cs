using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Core.Services.Tkm;
using TKM_Office_API.RequestParam;

namespace TKM_Office_API.Controllers.Tkm
{
    public class TransactionReportController : BaseController
    {
        private readonly ITransactionReportService _service;

        public TransactionReportController(ITransactionReportService service)
        {
            _service = service;
        }

        [Authorize]
        [HttpPost]
        public IHttpActionResult GetReportData(TransactionReportParam filter)
        {
            try
            {
                var data = _service.GetReportData(filter.DateFrom, filter.DateTo);
                return Ok(new Dictionary<string, object>()
                {
                    {"Data", data},
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
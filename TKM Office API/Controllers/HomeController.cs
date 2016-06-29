using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace TKM_Office_API.Controllers
{
    public class HomeController : ApiController
    {
        [AllowAnonymous]
        [HttpGet]
        public IHttpActionResult Index()
        {
            return Redirect(string.Format("http://{0}/Web/", HttpContext.Current.Request.Url.Authority));
        }
    }
}

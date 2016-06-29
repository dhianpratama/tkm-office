using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Results;

namespace TKM_Office_API
{
    public class BaseController : ApiController
    {

        protected internal virtual OkNegotiatedContentResult<T> Ok<T>(T content)
        {
            return base.Ok(JsonConvert.DeserializeObject<T>(
                JsonConvert.SerializeObject(content, Formatting.Indented,
                        new JsonSerializerSettings()
                        {
                            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                        })));
        }
    }
}
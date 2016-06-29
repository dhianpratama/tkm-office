using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using Core.Services;

namespace TKM_Office_API.Security
{
    public class WebApiAuthorizeAttribute : AuthorizeAttribute
    {
        public ISecurityService SecurityService { get; set; }
        public string AclKey { get; set; }

        protected override bool IsAuthorized(HttpActionContext actionContext)
        {
            return SecurityService.GetCurrentUser() != null && SecurityService.GetCurrentUserAcl().Contains(AclKey);
        }
    }
}
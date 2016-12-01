using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

//using System.Web.Http.Routing;

namespace Acctive.Extensions
{
    public static class UrlExtensions
    {
        public static string Absolute(this System.Web.Mvc.UrlHelper url, string relativeUrl)
        {
            //System.Web.Mvc.UrlHelper url = new System.Web.Mvc.UrlHelper();
            var request = url.RequestContext.HttpContext.Request;

            return string.Format("{0}://{1}{2}",
                (request.IsSecureConnection) ? "https" : "http",
                request.Headers["Host"],
                VirtualPathUtility.ToAbsolute(relativeUrl));
        }
    }
}
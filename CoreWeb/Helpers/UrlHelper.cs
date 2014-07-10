using System;
using System.Web;

namespace CoreWeb.Helpers
{
    public class UrlHelper
    {
        /// <summary>
        /// Gets root url to website.
        /// i.e. http://mydomain.com.au/
        /// </summary>
        /// <returns></returns>
        public static string GetRootUrl()
        {
            return HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority);
        }
    }
}

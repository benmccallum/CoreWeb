using System;
using System.Web;

namespace CoreWeb.Helpers
{
    public class CookieHelper
    {
        public static void WriteMultiValue(string cookieName, string key, string value, int expriesDays = -1)
        {
            HttpCookie cookie = new HttpCookie(cookieName);
            cookie.Values[key] = value;
            cookie.Expires = DateTime.MaxValue;
            if (expriesDays > 0)
            {
                cookie.Expires = System.DateTime.Now.AddDays(expriesDays);
            }
            HttpContext.Current.Response.Cookies.Add(cookie);
        }

        public static string ReadMultiCookie(string cookieName, string key)
        {
            var result = string.Empty;
            var cookie = HttpContext.Current.Request.Cookies[cookieName];
            if (cookie != null)
            {
                result = Convert.ToString(cookie.Values[key]);
            }
            return result;
        }

        public static string ReadCookie(string cookieName)
        {
            var result = string.Empty;
            var cookie = HttpContext.Current.Request.Cookies[cookieName];
            if (cookie != null)
            {
                result = Convert.ToString(cookie.Value);
            }
            return result;
        }
    }
}
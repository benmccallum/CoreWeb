using CoreSystem.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace CoreWeb.Helpers
{
    public static class EncryptionHelper
    {
        public static string EncryptAndEncode(string input)
        {
            return HttpServerUtility.UrlTokenEncode(TripleDESHelper.EncryptBytes(Encoding.UTF8.GetBytes(input)));
        }

        public static string DecryptAfterDecode(string input)
        {
            return Encoding.UTF8.GetString(TripleDESHelper.DecryptBytes(HttpServerUtility.UrlTokenDecode(input)));
        }
    }
}

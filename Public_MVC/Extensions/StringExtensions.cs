using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Public_MVC.Extensions
{
    public static class StringExtensions
    {
        public static byte[] FromBase64String(this string base64String)
        {
            return Convert.FromBase64String(base64String);
        }
    }

}
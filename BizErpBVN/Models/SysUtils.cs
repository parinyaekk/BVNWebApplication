using System;
using System.IO;
using System.Text;
using System.Security.Cryptography;
using System.Web;

namespace BizErpBVN.Models
{

    public static class SysUtils
    {

        private static string gSecKey = "BLZ#563646@bizzERP$2021";
        public static string GetComputeHash(string txtValue, string seckey = null)
        {
            if (string.IsNullOrEmpty(seckey)) seckey = gSecKey;

            HMACSHA1 hmac = new HMACSHA1(Encoding.UTF8.GetBytes(seckey));
            byte[] buff = hmac.ComputeHash(Encoding.UTF8.GetBytes(txtValue));
            string hex = BitConverter.ToString(buff);
            hex = hex.Replace("-", "").ToLower();
            return hex;
        }

        public static string GetIpAddr()
        {
            HttpContext context = HttpContext.Current;
            //HttpContext.Current.Request.UserHostAddress.ToString();
            //ServerVariables["HTTP_X_FORWARDED_FOR"];
            string ipAddr = context.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            //string ipAddr = context.Request.UserHostAddress.ToString();

            if (string.IsNullOrEmpty(ipAddr))
            {
                ipAddr = context.Request.ServerVariables["REMOTE_ADDR"];
            }

            return ipAddr;
        }
    }

}
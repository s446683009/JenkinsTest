using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Encodings.Web;

namespace SCR.Test
{
    static class Program
    {
        static void Main(string[] args)
        {
            try
            {
                string str = null;

                str.AddQueryString("1","2");

                Console.WriteLine(str);
                //SMTPHelper.SendEmail("william.li@plaza-network.com", "Test", "Test Email");
                Console.ReadLine();
            }
            catch (Exception e) {
                Console.WriteLine(e.Message);
            }
         
        }
        public static string AddQueryString(this string url, string query)
        {
            if (!url.Contains("?"))
            {
                url += "?";
            }
            else if (!url.EndsWith("&"))
            {
                url += "&";
            }

            return url + query;
        }


        public static string AddQueryString(this string url, string name, string value)
        {
            return url.AddQueryString(name + "=" + UrlEncoder.Default.Encode(value));
        }
    }

}

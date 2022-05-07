using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;

namespace SCR.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
 

                SMTPHelper.SendEmail("william.li@plaza-network.com", "Test", "Test Email");
                Console.ReadLine();
            }
            catch (Exception e) {
                Console.WriteLine(e.Message);
            }
         
        }
    }
}

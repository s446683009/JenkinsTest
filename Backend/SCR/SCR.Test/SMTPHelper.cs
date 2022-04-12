using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace SCR.Test
{
    public  class SMTPHelper
    {

        public static void SendEmail(string toAddress, string subject, string body)
        {
            try
            {
                var client = new SmtpClient();
                client.Host = "10.52.100.133";
                client.Port = 25;
                client.EnableSsl = false;

                client.UseDefaultCredentials = false;

                client.Credentials = new NetworkCredential("partners@plaza-network.com", "Plaza@2022");
                client.DeliveryMethod = SmtpDeliveryMethod.Network;

                var mail = new MailMessage();
                mail.From = new MailAddress("wasnoreply@plaza-network.com");

                string[] rec = toAddress.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (var item in rec)
                {
                    mail.To.Add(new MailAddress(item));
                }

             


                mail.Subject = subject;
                mail.Body = body;
                mail.BodyEncoding = Encoding.UTF8;
                mail.IsBodyHtml = true;

                client.Send(mail);
                Console.WriteLine("发送成功");
            }
            catch (Exception e)
            {
                throw e;

            }


        }


    }
}

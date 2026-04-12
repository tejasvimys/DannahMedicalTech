using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace TinnitusTrioBO
{
   public  class Logger
    {
       public static void SendErrorReport(string errorMessage)
       {
           try
           {
               var message = new MailMessage();
               var smtp = new SmtpClient();

               message.From = new MailAddress("tinnitustrio.danaah@gmail.com");
               message.To.Add(new MailAddress("tejasvi.qits@gmail.com"));
               message.Subject = "Error in Tinnitus Trio Application";
               message.Body = errorMessage;

               smtp.Port = 587;
               smtp.Host = "smtp.gmail.com";
               smtp.EnableSsl = true;
               smtp.UseDefaultCredentials = false;
               smtp.Credentials = new NetworkCredential("tinnitustrio.danaah@gmail.com", "danaah@#77");
               smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
               smtp.Send(message);
           }
           catch (Exception ex)
           {
               throw;
           }
       }

    }
}

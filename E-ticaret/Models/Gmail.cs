using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace E_ticaret.Models
{

    //Bu class iletişim sayfasında mail göndermek için
    public static class Gmail
    {
        public static void SendMail(string body)
        {
            var fromAddress = new MailAddress("hostingsolutionss@gmail.com", "Hosting Solutions Geribildirim");
            var toAddress = new MailAddress("hostingsolutionss@gmail.com");
            const string subject = "Hosting Solutions Geribildirim";

            using (var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 25,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, "yazilimstaj")
                //trololol kısmı e-posta adresinin şifresi
            })
            {
                using (var message = new MailMessage(fromAddress, toAddress) { Subject = subject, Body = body })
                {
                    smtp.Send(message);
                }
            }
        }
    }
}
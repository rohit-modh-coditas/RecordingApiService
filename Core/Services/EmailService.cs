using Application.Common.Interfaces;
using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public class EmailService : IEmailService
    {
        public void SendEmail(string fromName, string fromAddress, string to, string body, string subject, string cc = "", string bcc = "", bool isBodyHtml = false)
        {
            var mailClient = new SmtpMailClient();
            mailClient.FromName = fromName;
            mailClient.FromAddress = fromAddress;
            mailClient.To = to.Split(',').ToList();
            mailClient.Body = body;
            mailClient.Subject = subject;
            if (!String.IsNullOrEmpty(cc))
            {
                mailClient.Cc = cc.Split(',').ToList();
            }
            if (!String.IsNullOrEmpty(bcc))
            {
                mailClient.Bcc = bcc.Split(',').ToList();
            }
            mailClient.IsBodyHtml = isBodyHtml;
            mailClient.Send();
        }
    }
}

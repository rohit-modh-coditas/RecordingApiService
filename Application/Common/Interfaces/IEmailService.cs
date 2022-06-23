using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Interfaces
{
    public interface IEmailService
    {
        public void SendEmail(string fromName, string fromAddress, string to, string body, string subject, string cc = "", string bcc = "", bool isBodyHtml = false);

    }
}

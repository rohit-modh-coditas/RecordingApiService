using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Common
{
    public class SmtpMailClient
    {
        private readonly SmtpClient _client;
        private MailMessage _message;

        public string FromName { get; set; }

        public string FromAddress { get; set; }

        public List<string> To { get; set; }

        public List<string> Cc { get; set; }

        public List<string> Bcc { get; set; }

        public string Subject { get; set; }

        public string Body { get; set; }

        public bool IsBodyHtml { get; set; }

        public List<Attachment> Attachments { get; set; }

        public List<KeyValuePair<string, string>> ReplacementTags { get; set; }

        public SmtpMailClient()
        {
            //default constructor will use smtp client config by default
            Init();
            _client = new SmtpClient();
        }

        public SmtpMailClient(string smtpServer)
        {
            Init();
            _client = new SmtpClient(smtpServer);
        }

        public SmtpMailClient(string smtpServer, int port)
        {
            Init();
            _client = new SmtpClient(smtpServer, port);
        }

        public SmtpMailClient(string smtpServer, int port, NetworkCredential credentials, bool useSsl = false)
        {
            Init();
            _client = new SmtpClient(smtpServer, port) { Credentials = credentials, EnableSsl = useSsl };
        }

        private void Init()
        {
            ReplacementTags = new List<KeyValuePair<string, string>>();
            Attachments = new List<Attachment>();
            Cc = new List<string>();
            To = new List<string>();
            Bcc = new List<string>();
        }

        /// <summary>
        /// Validates that all of the necessary properties have been set prior to sending
        /// </summary>
        private void ValidateProperties()
        {
            if (String.IsNullOrEmpty(Subject))
                throw new Exception("Email must contain a subject");
            if (String.IsNullOrEmpty(FromAddress))
                throw new Exception("Email must contain a from address");
            if (String.IsNullOrEmpty(FromName))
                throw new Exception("Email must contain a from name");
            if (To.Count == 0)
                throw new Exception("Email recipient list cannot be empty");
        }

        /// <summary>
        /// Configure the message with all the properites prior to sending
        /// </summary>
        private void ConfigureMessage()
        {
            _message = new MailMessage();

            var from = new MailAddress(FromAddress, FromName, System.Text.Encoding.UTF8);
            _message.From = from;
            foreach (string individual in To)
            {
                var add = new MailAddress(individual);
                _message.To.Add(add);
            }

            foreach (string individual in Cc)
            {
                var add = new MailAddress(individual);
                _message.CC.Add(add);
            }

            foreach (string individual in Bcc)
            {
                var add = new MailAddress(individual);
                _message.Bcc.Add(add);
            }

            _message.Subject = Subject;
            _message.SubjectEncoding = System.Text.Encoding.UTF8;

            //finally replace any tags in the body with the replacementTags property
            foreach (KeyValuePair<string, string> item in ReplacementTags)
            {
                Body = Body.Replace(item.Key, item.Value);
            }

            _message.Body = Body;
            _message.IsBodyHtml = IsBodyHtml;
            _message.BodyEncoding = System.Text.Encoding.UTF8;

            //add the attachments
            foreach (Attachment at in Attachments)
            {
                _message.Attachments.Add(at);
            }
        }

        /// <summary>
        /// Sends an email synchronously based on the properties that have been set in this class after created
        /// </summary>
        public void Send()
        {
            //validate the necessary properties are set prior to sending
            ValidateProperties();

            //configure the message with all the properites prior to sending
            ConfigureMessage();

            _client.Send(_message);

            _message.Dispose();
        }

        /// <summary>
        /// Sends an email asynchronously based on the properties that have been set in this class after created.  This will
        /// return the status back to the callback routine passed in.  
        /// </summary>
        /// <param name="callback"></param>
        public void SendAsync(SendCompletedEventHandler callback)
        {
            //validate the necessary properties are set prior to sending
            ValidateProperties();

            //configure the message with all the properites prior to sending
            ConfigureMessage();

            //create a new token for the async
            string token = Guid.NewGuid().ToString();
            _client.SendCompleted += callback;

            _client.SendAsync(_message, token);
        }
    }
}

using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spring25.BlCapstone.BE.Services.Untils
{
    public static class EmailHelper
    {
        private static readonly string _smtpServer;
        private static readonly int _port;
        private static readonly string _senderName;
        private static readonly string _senderEmail;
        private static readonly string _username;
        private static readonly string _password;

        static EmailHelper()
        {
            _smtpServer = GetEnvironmentVariable("EMAIL_SMTP_SERVER");
            _port = int.Parse(GetEnvironmentVariable("EMAIL_PORT"));
            _senderName = "bfarmx - Blockchain FarmXperience";
            _senderEmail = GetEnvironmentVariable("EMAIL_SENDER_EMAIL");
            _username = GetEnvironmentVariable("EMAIL_USERNAME");
            _password = GetEnvironmentVariable("EMAIL_PASSWORD").Replace("-", " ");
        }

        private static string GetEnvironmentVariable(string key)
        {
            var value = Environment.GetEnvironmentVariable(key);
            if (string.IsNullOrEmpty(value))
            {
                throw new Exception($"'{key}' configuartion is missing...");
            }
            return value;
        }

        public static async Task SendMail(string to, string subject, string username, string body)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(_senderName, _senderEmail));
            message.To.Add(new MailboxAddress(username, to));
            message.Subject = subject;

            var bodyBuilder = new BodyBuilder
            {
                HtmlBody = body
            };
            message.Body = bodyBuilder.ToMessageBody();

            using (var client = new MailKit.Net.Smtp.SmtpClient())
            {
                try
                {
                    await client.ConnectAsync(_smtpServer, _port, MailKit.Security.SecureSocketOptions.StartTls);
                    await client.AuthenticateAsync(_username, _password);
                    await client.SendAsync(message);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
                finally
                {
                    await client.DisconnectAsync(true);
                }
            }
        }

        public static string GetEmailBody(string templateName, Dictionary<string, string> variables)
        {
            var assembly = typeof(EmailHelper).Assembly;

            var resourceName = assembly.GetManifestResourceNames()
                .FirstOrDefault(x => x.EndsWith(templateName));

            if (resourceName == null)
            {
                throw new Exception($"Template '{templateName}' not found in embedded resources.");
            }

            string template;

            using (var stream = assembly.GetManifestResourceStream(resourceName))
            using (var reader = new StreamReader(stream))
            {
                template = reader.ReadToEnd();
            }

            foreach (var variable in variables)
            {
                template = template.Replace(variable.Key, variable.Value);
            }

            return template;
        }
    }
}

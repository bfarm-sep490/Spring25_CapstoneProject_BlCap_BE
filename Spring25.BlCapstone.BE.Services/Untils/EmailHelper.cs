﻿using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spring25.BlCapstone.BE.Services.Untils
{
    public class EmailHelper
    {
        private readonly string _smtpServer;
        private readonly int _port;
        private readonly string _senderName;
        private readonly string _senderEmail;
        private readonly string _username;
        private readonly string _password;

        public EmailHelper()
        {
            _smtpServer = GetEnvironmentVariable("EMAIL_SMTP_SERVER");
            _port = int.Parse(GetEnvironmentVariable("EMAIL_PORT"));
            _senderName = GetEnvironmentVariable("EMAIL_SENDER_NAME");
            _senderEmail = GetEnvironmentVariable("EMAIL_SENDER_EMAIL");
            _username = GetEnvironmentVariable("EMAIL_USERNAME");
            _password = GetEnvironmentVariable("EMAIL_PASSWORD");
        }

        private string GetEnvironmentVariable(string key)
        {
            var value = Environment.GetEnvironmentVariable(key);
            if (string.IsNullOrEmpty(value))
            {
                throw new Exception($"'{key}' configuartion is missing...");
            }
            return value;
        }

        public async Task SendMail(string to, string subject, string username, string body)
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
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MimeKit;
using MimeKit.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace Survey_Project.Models
{
    //public class Email
    
        namespace Project.Services
    {
        /// Using a static class to store sensitive credentials
        /// for simplicity. Ideally these should be stored in
        /// configuration files
        public static class Constants
        {
            public static string SenderName => "<sender_name>";
            public static string SenderEmail => "<sender_email>";
            public static string EmailPassword => "email_password";
            public static string SmtpHost => "<smtp_host>";
            public static int SmtpPort => 993;
        }
        public class EmailService : IEmailSender
        {
            public Task SendEmailAsync(string recipientEmail, string subject, string message)
            {
                MimeMessage mimeMessage = new MimeMessage();
                mimeMessage.From.Add(new MailboxAddress(Constants.SenderName, Constants.SenderEmail));
                mimeMessage.To.Add(new MailboxAddress("", recipientEmail));
                mimeMessage.Subject = subject;

                mimeMessage.Body = new TextPart(TextFormat.Html)
                {
                    Text = message,
                };

                using (var client = new SmtpClient())
                {

                    client.ServerCertificateValidationCallback = (s, c, h, e) => true;

                    client.Connect(Constants.SmtpHost, Constants.SmtpPort, false);

                    client.AuthenticationMechanisms.Remove("XOAUTH2");

                    // Note: only needed if the SMTP server requires authentication
                    client.Authenticate(Constants.SenderEmail, Constants.EmailPassword);

                    client.Send(mimeMessage);

                    client.Disconnect(true);
                    return Task.FromResult(0);
                }
            }
        }

    }
}


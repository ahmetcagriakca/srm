using Fix.Delivery.Config;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Text;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Fix.Delivery
{
    public class EmailService : IEmailService
    {
        private readonly EmailConfig ec;

        public EmailService(IOptions<EmailConfig> emailConfig)
        {
            this.ec = emailConfig.Value;
        }

        public async Task SendEmailAsync(String email, String subject, String message)
        {
            try
            {
                var emailMessage = new MimeMessage();

                emailMessage.From.Clear();
                emailMessage.From.Add(new MailboxAddress(ec.FromName, ec.FromAddress));

                emailMessage.To.Clear();
                emailMessage.To.Add(new MailboxAddress("", email));
                emailMessage.Subject = subject;
                emailMessage.Body = new TextPart(TextFormat.Html) { Text = message };

                using (var client = new SmtpClient())
                {
                    client.LocalDomain = ec.LocalDomain;

                    await client.ConnectAsync(ec.MailServerAddress, Convert.ToInt32(ec.MailServerPort), SecureSocketOptions.Auto).ConfigureAwait(false);
                    await client.AuthenticateAsync(new NetworkCredential(ec.UserId, ec.UserPassword));
                    await client.SendAsync(emailMessage).ConfigureAwait(false);
                    await client.DisconnectAsync(true).ConfigureAwait(false);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public async Task SendEmailAsync(string[] to, string subject, string message, List<string> attachments, string[] cc = null, string[] bcc = null)
        {
            try
            {
                var emailMessage = new MimeMessage();
                emailMessage.From.Clear();
                emailMessage.From.Add(new MailboxAddress(ec.FromName, ec.FromAddress));

                if (to?.Length > 0)
                {
                    emailMessage.To.Clear();
                    foreach (string email in to)
                    {
                        emailMessage.To.Add(new MailboxAddress("", email));
                    }
                }
                if (cc?.Length > 0)
                {
                    emailMessage.Cc.Clear();
                    foreach (string email in cc)
                    {
                        emailMessage.Cc.Add(new MailboxAddress("", email));
                    }
                }

                if (bcc?.Length > 0)
                {
                    emailMessage.Bcc.Clear();
                    foreach (string email in bcc)
                    {
                        emailMessage.Bcc.Add(new MailboxAddress("", email));
                    }
                }
                emailMessage.Subject = subject;

                emailMessage.Body = new TextPart(TextFormat.Html) { Text = message.Replace("\n", "<br>") };

                using (var client = new SmtpClient())
                {
                    client.LocalDomain = ec.LocalDomain;

                    await client.ConnectAsync(ec.MailServerAddress, Convert.ToInt32(ec.MailServerPort), SecureSocketOptions.Auto).ConfigureAwait(false);
                    await client.AuthenticateAsync(new NetworkCredential(ec.UserId, ec.UserPassword));
                    await client.SendAsync(emailMessage).ConfigureAwait(false);
                    await client.DisconnectAsync(true).ConfigureAwait(false);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}

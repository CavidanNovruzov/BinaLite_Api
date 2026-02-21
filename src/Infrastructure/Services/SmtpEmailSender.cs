
using System.Net;
using System.Net.Mail;
using System.Text;

namespace Infrastructure.Services
{
    public class SmtpEmailSender : IEmailSender
    {
        private readonly SmtpOptions _options;

        public SmtpEmailSender(SmtpOptions options)
        {
            _options = options;
        }

        public async Task SendEmailAsync(
            string to,
            string subject,
            string htmlBody,
            string? plainBody = null,
            CancellationToken ct = default)
        {
            if (!_options.SendEmails)
                return;

            if (string.IsNullOrWhiteSpace(to))
                return;

            var toAddress = to.Trim();

            using var client = new SmtpClient(_options.SmtpHost, _options.SmtpPort)
            {
                EnableSsl = _options.UseSsl,
                DeliveryMethod = SmtpDeliveryMethod.Network
            };

            if (!string.IsNullOrWhiteSpace(_options.UserName))
            {
                client.Credentials = new NetworkCredential(
                    _options.UserName,
                    _options.Password);
            }

            var from = new MailAddress(_options.FromEmail, _options.FromName);
            var toAddr = new MailAddress(toAddress);

            var body = !string.IsNullOrEmpty(htmlBody)
                ? htmlBody
                : (plainBody ?? string.Empty);

            using var message = new MailMessage(from, toAddr)
            {
                Subject = subject,
                Body = body,
                IsBodyHtml = !string.IsNullOrEmpty(htmlBody)
            };

            await client.SendMailAsync(message, ct);
        }
    }
}

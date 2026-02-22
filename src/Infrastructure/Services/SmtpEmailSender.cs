
using Application.Options;
using Application.Abstracts.Services.Auth;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;



namespace Infrastructure.Services;

public class SmtpEmailSender : IEmailSender
{
    private readonly SmtpOptions _options;

    public SmtpEmailSender(IOptions<SmtpOptions> options)
    {
        _options = options.Value;
    }

    public async Task SendEmailAsync(
        string to,
        string subject,
        string htmlBody,
        string? plainBody = null,
        CancellationToken ct = default)
    {
        if (!_options.SendEmails || string.IsNullOrWhiteSpace(to))
            return;

        try
        {
            using var client = new SmtpClient(_options.SmtpHost, _options.SmtpPort)
            {
                EnableSsl = _options.UseSsl,
                DeliveryMethod = SmtpDeliveryMethod.Network
            };

            if (!string.IsNullOrWhiteSpace(_options.UserName))
                client.Credentials = new NetworkCredential(_options.UserName, _options.Password);

            var from = new MailAddress(_options.FromEmail, _options.FromName);
            var toAddr = new MailAddress(to.Trim());

            using var message = new MailMessage(from, toAddr)
            {
                Subject = subject,
                Body = !string.IsNullOrEmpty(htmlBody) ? htmlBody : (plainBody ?? string.Empty),
                IsBodyHtml = !string.IsNullOrEmpty(htmlBody)
            };

            await client.SendMailAsync(message, ct);
        }
        catch (Exception ex)
        {
            Console.WriteLine("SMTP Error: " + ex.Message);
            throw;
        }
    }
}

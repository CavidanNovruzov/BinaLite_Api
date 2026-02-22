

namespace Application.Abstracts.Services.Auth;

public interface IEmailSender
{
    Task SendEmailAsync(
        string to,
        string subject,
        string htmlBody,
        string? plainBody = null,
        CancellationToken ct = default);
}

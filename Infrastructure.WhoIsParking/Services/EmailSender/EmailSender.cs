using Domain.WhoIsParking.Models;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Text;

namespace Infrastructure.WhoIsParking.Services.EmailSender;
/// <summary>
/// Email service for Asp.Net Identity Endpoints
/// </summary>
internal class EmailSender : IEmailSender<ApplicationUser>
{
    private readonly EmailOptions _emailOptions;

    public EmailSender(IOptions<EmailOptions> emailOptions)
    {
        _emailOptions = emailOptions.Value;

        if (string.IsNullOrEmpty(_emailOptions.Email) ||
            string.IsNullOrEmpty(_emailOptions.Password) ||
            string.IsNullOrEmpty(_emailOptions.Host) ||
            _emailOptions.Port == 0)
        {
            throw new InvalidOperationException("Email configuration is invalid. Check the EMAIL_CONFIG section.");
        }

    }

    public async Task SendConfirmationLinkAsync(ApplicationUser user, string email, string confirmationLink)
        => await SendEmail(email, "Email verifizieren", confirmationLink /*TODO: link has to be as button or just link fr*/).ConfigureAwait(false);

    public async Task SendPasswordResetLinkAsync(ApplicationUser user, string email, string resetLink) // is not used by identity
        => await SendEmail(email, "Password zurücksetzen Link", resetLink ).ConfigureAwait(false);

    public async Task SendPasswordResetCodeAsync(ApplicationUser user, string email, string resetCode) 
        => await SendEmail(email, "Password zurücksetzen Code", resetCode).ConfigureAwait(false);

    private async Task SendEmail(string to, string subject, string body)
    {
        var mimeMessage = CreateMimeMessage(to, subject, body);

        using var smtp = new SmtpClient();

        await smtp.ConnectAsync(_emailOptions.Host, _emailOptions.Port, SecureSocketOptions.StartTls)
            .ConfigureAwait(false);

        await smtp.AuthenticateAsync(_emailOptions.Email, _emailOptions.Password)
            .ConfigureAwait(false);

        await smtp.SendAsync(mimeMessage)
            .ConfigureAwait(false);

        await smtp.DisconnectAsync(true)
            .ConfigureAwait(false);
    }

    private MimeMessage CreateMimeMessage(string to, string subject, string body)
    {
        var mimeMessage = new MimeMessage();

        mimeMessage.From.Add(MailboxAddress.Parse(_emailOptions.Email));
        mimeMessage.To.Add(MailboxAddress.Parse(to));
        mimeMessage.Subject = subject;
        mimeMessage.Body = new TextPart(TextFormat.Html) { Text = body };

        return mimeMessage;
    }
}

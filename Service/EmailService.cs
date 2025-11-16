using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using System.Threading.Tasks;

namespace Service
{
    public class EmailService
    {
        private readonly string fromEmail = "nguyenthingochuyen11.1cbq@gmail.com";
        private readonly string appPassword = "pmpv lhpc goxy whdn";

        public async Task SendEmailAsync(string to, string subject, string body)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Hospital System", fromEmail));
            message.To.Add(new MailboxAddress("", to));
            message.Subject = subject;
            message.Body = new TextPart("plain") { Text = body };

            using var client = new SmtpClient();
            await client.ConnectAsync("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
            await client.AuthenticateAsync(fromEmail, appPassword);
            await client.SendAsync(message);
            await client.DisconnectAsync(true);
        }
    }
}

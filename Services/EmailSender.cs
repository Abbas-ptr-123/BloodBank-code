using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;
namespace BloodBank.Services
{
    public class EmailSender : IEmailSender
    {
        private readonly SmtpSettings _smtpSettings;

        public EmailSender(IOptions<SmtpSettings> smtpSettings)
        {
            _smtpSettings = smtpSettings.Value;
        }

        public async Task SendEmailAsync(string email, string subject, string message)
        {
            var mailMessage = new MailMessage
            {
                From = new MailAddress("your-email@example.com"),
                Subject = subject,
                Body = message,
                IsBodyHtml = true
            };
            mailMessage.To.Add(email);

            using (var smtpClient = new SmtpClient(_smtpSettings.Server, _smtpSettings.Port))
            {
                smtpClient.Credentials = new NetworkCredential(_smtpSettings.User, _smtpSettings.Password);
                smtpClient.EnableSsl = true;
                await smtpClient.SendMailAsync(mailMessage);
            }
        }
    }
}


//public async Task SendEmailAsync(string email, string subject, string message)
//{
//    var mailMessage = new MailMessage
//    {
//        From = new MailAddress("your-email@example.com"),
//        Subject = subject,
//        Body = message,
//        IsBodyHtml = true
//    };
//    mailMessage.To.Add(email);

//    using (var smtpClient = new SmtpClient(_smtpServer, _smtpPort))
//    {
//        smtpClient.Credentials = new NetworkCredential(_smtpUser, _smtpPass);
//        smtpClient.EnableSsl = true;
//        await smtpClient.SendMailAsync(mailMessage);

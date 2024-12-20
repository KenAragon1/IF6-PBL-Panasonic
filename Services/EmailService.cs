using System.Net.Mail;


public static class EmailService
{
    public static async Task SendAsync(string email, string subject, string body)
    {
        using (var client = new SmtpClient("smtp.yourserver.com"))
        {
            var message = new MailMessage("no-reply@yourdomain.com", email, subject, body)
            {
                IsBodyHtml = true
            };
            await client.SendMailAsync(message);
        }
    }
}
// EmailService.cs
using MailKit.Net.Smtp;
using MimeKit;

public class EmailService
{
    public async Task SendEmailAsync(string email, string subject, string message)
    {
        var emailMessage = new MimeMessage();
        emailMessage.From.Add(new MailboxAddress("Course Management System", "fox76459@gmail.com"));
        emailMessage.To.Add(new MailboxAddress("", email));
        emailMessage.Subject = subject;
        emailMessage.Body = new TextPart("plain") { Text = message };

        using (var client = new SmtpClient())
        {
            await client.ConnectAsync("smtp.gmail.com", 465 , false);
            await client.AuthenticateAsync("fox76459@gmail.com", "Yg1rb76yYg1rb76y");
            await client.SendAsync(emailMessage);
            await client.DisconnectAsync(true);
        }
    }
}
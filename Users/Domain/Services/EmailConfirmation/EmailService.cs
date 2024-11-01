using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using PostmarkDotNet;
using PostmarkDotNet.Model;


namespace Users.Services.EmailConfirmation;

public class SmtpSettings
{
    public string Host { get; set; }
    public int Port { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
}

public class EmailService(IOptions<SmtpSettings> smtpSettings)
{
    private readonly SmtpSettings _smtpSettings = smtpSettings.Value;

    public async Task SendEmailAsync(string toEmail, string subject, string textBody, string htmlBody)
    {
        var message = new PostmarkMessage()
        {
            To = toEmail,
            From = "postmark@francovp.com",
            TrackOpens = true,
            Subject = subject,
            TextBody = textBody,
            HtmlBody = htmlBody,
            MessageStream = "psyshield",
            Tag = subject,
            Headers = new HeaderCollection(new Dictionary<string, string> { { "X-PM-Message-Stream", "psyshield" } })
        };

        var client = new PostmarkClient("f059afb6-44cd-40a3-8d9a-d2834b03e161");
        await client.SendMessageAsync(message);

        // if (sendResult.Status != PostmarkStatus.Success){ 
            
        // }
        // else { /* Resolve issue.*/ }
    }
}

using MimeKit;

namespace MarteliveryAPI.Services.EmailServices
{
    public class Message
    {
        public List<MailboxAddress> To { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }

        public IFormFileCollection Attachments { get; set; }

        public Message(IEnumerable<string> to, string subject, string content, IFormFileCollection attachments)
        {
            To = [.. to.Select(x => new MailboxAddress(string.Empty, x))];
            Subject = subject;
            Content = content;
            Attachments = attachments;
        }
    }
}

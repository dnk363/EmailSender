using EmailSender.Interfaces;

namespace EmailSender.Models
{
    class Message : IMessage
    {
        public string SenderName { get; set; }
        public string SenderEmail { get; set; }
        public string RecieverEmail { get; set; }
        public string MessageSubject { get; set; }
        public string MessageBody { get; set; }
    }
}

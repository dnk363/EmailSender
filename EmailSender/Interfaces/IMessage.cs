using System;
using System.Collections.Generic;
using System.Text;

namespace EmailSender.Interfaces
{
    public interface IMessage
    {
        public string SenderName { get; set; }

        public string SenderEmail { get; set; }

        public string RecieverEmail { get; set; }

        public string MessageSubject { get; set; }

        public string MessageBody { get; set; }
    }
}

using EmailSender.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmailSender.Models
{
    class EmailSettings : IEmailSettings
    {
        public bool EnableSSL { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }
        public string UserName { get; set; }
        public string UserPassword { get; set; }
        public string UserEmail { get; set; }
    }
}

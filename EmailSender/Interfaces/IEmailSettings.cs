using System;
using System.Collections.Generic;
using System.Text;

namespace EmailSender.Interfaces
{
    public interface IEmailSettings
    {
        public bool EnableSSL { get; set; }

        public string Host { get; set; }

        public int Port { get; set; }

        public string UserEmail { get; set; }

        public string UserPassword { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace EmailSender.Interfaces
{
    public interface IFormMessageService
    {
        public virtual string GetMessage(ISiteSettings siteSettings) { return ""; }
    }
}

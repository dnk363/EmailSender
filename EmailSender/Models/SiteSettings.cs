using EmailSender.Interfaces;

namespace EmailSender.Models
{
    class SiteSettings : ISiteSettings
    {
        public string SiteUrl { get; set; }
        public string TableClassID { get; set; }
        public string CompareValue { get; set; }
    }
}

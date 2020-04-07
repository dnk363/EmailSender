using EmailSender.Interfaces;

namespace EmailSender.Models
{
    public class SiteSettings : ISiteSettings
    {
        public string SiteUrl { get; set; }
        public string TableClassID { get; set; }
        public string ColumnToCompare { get; set; }
        public string CompareValue { get; set; }
        public string NotNullColumn { get; set; }
    }
}

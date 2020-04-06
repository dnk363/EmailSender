using System;
using System.Collections.Generic;
using System.Text;

namespace EmailSender.Interfaces
{
    public interface ISiteSettings
    {
        public string SiteUrl { get; set; }
        public string TableClassID { get; set; }
        public string ColumnToCompare { get; set; }
        public string CompareValue { get; set; }
        public string NotNullColumn { get; set; }
    }
}

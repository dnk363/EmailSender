using EmailSender.Services;
using EmailSender.Models;
using NUnit.Framework;
using System;

namespace NUnitTests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void FormMessageTest()
        {
            FormMessageService formMessageService = new FormMessageService();
            SiteSettings siteSettingsEmpty = new SiteSettings()
            {
                SiteUrl = string.Empty,
                TableClassID = string.Empty,
                ColumnToCompare = string.Empty,
                CompareValue = string.Empty,
                NotNullColumn = string.Empty
            };
            SiteSettings siteSettingsNull = new SiteSettings()
            {
                SiteUrl = null,
                TableClassID = null,
                ColumnToCompare = null,
                CompareValue = null,
                NotNullColumn = null
            };
            SiteSettings siteSettingsRight = new SiteSettings()
            {
                SiteUrl = "https://www.eurosport.ru/football/bundesliga/standing.shtml",
                TableClassID = "standing-table",
                ColumnToCompare = "\u0412",
                CompareValue = "10",
                NotNullColumn = "\u0418"
            };

            Assert.NotNull(formMessageService.GetMessage(siteSettingsRight));
            Assert.NotNull(formMessageService.GetMessage(siteSettingsNull));
            Assert.NotNull(formMessageService.GetMessage(siteSettingsEmpty));
        }
    }
}
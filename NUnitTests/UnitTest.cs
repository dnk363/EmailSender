using EmailSender.Services;
using EmailSender.Models;
using EmailSender.ViewModels;
using NUnit.Framework;
using System;
using System.ComponentModel;
using System.Linq;
using System.Collections;
using System.Globalization;

namespace NUnitTests
{
    public class Tests
    {
        private readonly string PATH = $"{Environment.CurrentDirectory}\\appSettings.json";

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

        [Test]
        public void DataIOTest()
        {
            DataIOService dataIOService = new DataIOService(PATH);

            BindingList<ViewSettings> bindingListSave = new BindingList<ViewSettings>()
            {
                new ViewSettings()
                {
                    EnableSSL = true,
                    Host = "host",
                    Port = 1,
                    UserEmail = "email",
                    Name = "name",
                    UserPassword = "password",
                    TimeStartSettings = "* * * * * *",
                    MessageBody = "body",
                    MessageSubject = "subject",
                    RecieverEmail = "email",
                    SenderEmail = "email",
                    SenderName = "name",
                    SiteUrl = "https://www.eurosport.ru/football/bundesliga/standing.shtml",
                    TableClassID = "standing-table",
                    ColumnToCompare = "\u0412",
                    CompareValue = "10",
                    NotNullColumn = "\u0418"
                }
            };

            dataIOService.SaveData(bindingListSave);

            BindingList<ViewSettings> bindingListLoad = dataIOService.LoadData();

            Comparer comparer = new Comparer(CultureInfo.CurrentCulture);

            var viewSettingsLoad = bindingListLoad.FirstOrDefault();
            var viewSettingsSave = bindingListSave.FirstOrDefault();

            Assert.AreEqual(comparer.Compare(viewSettingsLoad, viewSettingsSave), 0);
        }
    }
}
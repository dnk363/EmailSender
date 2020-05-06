using EmailSender.ViewModels;
using EmailSender.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using EmailSender.Models;

namespace EmailSender
{
    public partial class MainWindow : Window
    {
        private readonly string PATH = $"{Environment.CurrentDirectory}\\appSettings.json";
        private BindingList<ViewSettings> _dataSettingsList;
        private DataIOService _dataIOService;
        private List<ShedulerService> _shedulerServices = new List<ShedulerService>();

        public MainWindow()
        {
            InitializeComponent();
            Logger.Info("Programm is running");
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            stopButton.IsEnabled = false;
            
            _dataIOService = new DataIOService(PATH);

            try
            {
                _dataSettingsList = _dataIOService.LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Close();
            }

            
            gridEmailSender.ItemsSource = _dataSettingsList;
            itemEmailSender.DataContext = _dataSettingsList;
            _dataSettingsList.ListChanged += DataSettingsList_ListChanged;

            FormMessageService formMessageService = new FormMessageService();
        }

        private void DataSettingsList_ListChanged(object sender, ListChangedEventArgs e)
        {
            if ( e.ListChangedType == ListChangedType.ItemAdded
              || e.ListChangedType == ListChangedType.ItemDeleted
              || e.ListChangedType == ListChangedType.ItemChanged)
            {
                try
                {
                    _dataIOService.SaveData(sender);
                }
                catch (Exception ex)
                {
                    Logger.Error($"Cannot save settings. Message:{ex.Message}");
                    Close();
                }
            }
        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            EmailService emailService = new EmailService();
            FormMessageService formMessageService = new FormMessageService();

            foreach (var settingsList in _dataSettingsList)
            {
                EmailSettings emailSettings = new EmailSettings()
                {
                    EnableSSL = settingsList.EnableSSL,
                    Host = settingsList.Host,
                    Port = settingsList.Port,
                    UserEmail = settingsList.UserEmail,
                    UserName = settingsList.UserEmail,
                    UserPassword = settingsList.UserPassword,
                    TimeStartSettings = settingsList.TimeStartSettings
                };

                Message message = new Message()
                {
                    MessageBody = settingsList.MessageBody,
                    MessageSubject = settingsList.MessageSubject,
                    RecieverEmail = settingsList.RecieverEmail,
                    SenderEmail = settingsList.SenderEmail,
                    SenderName = settingsList.SenderName
                };

                SiteSettings siteSettings = new SiteSettings()
                {
                    SiteUrl = settingsList.SiteUrl,
                    TableClassID = settingsList.TableClassID,
                    ColumnToCompare = settingsList.ColumnToCompare,
                    CompareValue = settingsList.CompareValue,
                    NotNullColumn= settingsList.NotNullColumn
                };

                ShedulerService shedulerService = new ShedulerService(emailService, formMessageService, emailSettings, message, siteSettings);

                shedulerService.StartNowSync();

                _shedulerServices.Add(shedulerService);
            }

            startButton.IsEnabled = false;
            stopButton.IsEnabled = true;
            Logger.Info($"Scheduled [{_dataSettingsList.Count}] tasks launched");
        }

        private void StopButton_Click(object sender, RoutedEventArgs e)
        {
            foreach (var service in _shedulerServices)
            {
                service.Stop();
            }
            stopButton.IsEnabled = false;
            startButton.IsEnabled = true;
            Logger.Info($"Scheduled [{_dataSettingsList.Count}] tasks stoped");
        }

        private void gridEmailSender_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            itemEmailSender.DataContext = gridEmailSender.CurrentItem;
        }
    }
}

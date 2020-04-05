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
            _dataSettingsList.ListChanged += _dataSettingsList_ListChanged;

            FormMessageService formMessageService = new FormMessageService();
        }

        private void _dataSettingsList_ListChanged(object sender, ListChangedEventArgs e)
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
                    MessageBox.Show(ex.Message);
                    Close();
                }
            }
        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            EmailService emailService = new EmailService();

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
                    CompareValue = settingsList.CompareValue,
                    
                };

                ShedulerService shedulerService = new ShedulerService(emailService, emailSettings, message, siteSettings);

                shedulerService.StartNowAsync();

                _shedulerServices.Add(shedulerService);
            }

            startButton.IsEnabled = false;
            stopButton.IsEnabled = true;
        }

        private void StopButton_Click(object sender, RoutedEventArgs e)
        {
            foreach (var service in _shedulerServices)
            {
                service.Stop();
            }
            stopButton.IsEnabled = false;
            startButton.IsEnabled = true;
        }

        private void gridEmailSender_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            itemEmailSender.DataContext = gridEmailSender.CurrentItem;
        }
    }
}

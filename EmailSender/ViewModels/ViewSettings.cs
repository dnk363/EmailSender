using EmailSender.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace EmailSender.ViewModels
{
    class ViewSettings : IEmailSettings, IMessage, INotifyPropertyChanged
    {
        private bool _enableSSL;
        private string _host;
        private int _port;
        private string _userEmail;
        private string _userPassword;
        private string _senderName;
        private string _senderEmail;
        private string _recieverEmail;
        private string _messageSubject;
        private string _messageBody;
        private string _siteUrl;
        private string _tableClassID;

        public bool EnableSSL
        {
            get { return _enableSSL; }
            set
            {
                if (_enableSSL == value)
                    return;
                _enableSSL = value;
                OnPropertyChanged("EnableSSL");
            }
        }
        public string Host
        {
            get { return _host; }
            set
            {
                if (_host == value)
                    return;
                _host = value;
                OnPropertyChanged("Host");
            }
        }
        public int Port
        {
            get { return _port; }
            set
            {
                if (_port == value)
                    return;
                _port = value;
                OnPropertyChanged("Port");
            }
        }
        public string UserEmail
        {
            get { return _userEmail; }
            set
            {
                if (_userEmail == value)
                    return;
                _userEmail = value;
                OnPropertyChanged("UserEmail");
            }
        }
        public string UserPassword
        {
            get { return _userPassword; }
            set
            {
                if (_userPassword == value)
                    return;
                _userPassword = value;
                OnPropertyChanged("UserPassword");
            }
        }

        public string SenderName
        {
            get { return _senderName; }
            set
            {
                if (_senderName == value)
                    return;
                _senderName = value;
                OnPropertyChanged("SenderName");
            }
        }
        public string SenderEmail
        {
            get { return _senderEmail; }
            set
            {
                if (_senderEmail == value)
                    return;
                _senderEmail = value;
                OnPropertyChanged("SenderEmail");
            }
        }
        public string RecieverEmail
        {
            get { return _recieverEmail; }
            set
            {
                if (_recieverEmail == value)
                    return;
                _recieverEmail = value;
                OnPropertyChanged("RecieverEmail");
            }
        }
        public string MessageSubject
        {
            get { return _messageSubject; }
            set
            {
                if (_messageSubject == value)
                    return;
                _messageSubject = value;
                OnPropertyChanged("MessageSubject");
            }
        }
        public string MessageBody
        {
            get { return _messageBody; }
            set
            {
                if (_messageBody == value)
                    return;
                _messageBody = value;
                OnPropertyChanged("MessageBody");
            }
        }
        public string SiteUrl
        {
            get { return _siteUrl; }
            set
            {
                if (_siteUrl == value)
                    return;
                _siteUrl = value;
                OnPropertyChanged("SiteUrl");
            }
        }
        public string TableClassID
        {
            get { return _tableClassID; }
            set
            {
                if (_tableClassID == value)
                    return;
                _tableClassID = value;
                OnPropertyChanged("TableClassID");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

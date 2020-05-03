using EmailSender.Interfaces;
using System;
using System.ComponentModel;

namespace EmailSender.ViewModels
{
    public class ViewSettings : IEmailSettings, IMessage, ISiteSettings, INotifyPropertyChanged, IComparable
    {
        private string _name;
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
        private string _columnToCompare;
        private string _compareValue;
        private string _timeStartSettings;
        private string _notNullColumn;

        public string Name
        {
            get { return _name; }
            set
            {
                if (_name == value)
                    return;
                _name = value;
                OnPropertyChanged("Name");
            }
        }

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

        public string CompareValue
        {
            get { return _compareValue; }
            set
            {
                if (_compareValue == value)
                    return;
                _compareValue = value;
                OnPropertyChanged("CompareValue");
            }
        }

        public string TimeStartSettings
        {
            get { return _timeStartSettings; }
            set
            {
                if (_timeStartSettings == value)
                    return;
                _timeStartSettings = value;
                OnPropertyChanged("TimeStartSettings");
            }
        }

        public string NotNullColumn
        {
            get { return _notNullColumn; }
            set
            {
                if (_notNullColumn == value)
                    return;
                _notNullColumn = value;
                OnPropertyChanged("NotNullColumn");
            }
        }

        public string ColumnToCompare
        {
            get { return _columnToCompare; }
            set
            {
                if (_columnToCompare == value)
                    return;
                _columnToCompare = value;
                OnPropertyChanged("ColumnToCompare");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public int CompareTo(object obj)
        {
            ViewSettings otherViewSettings = obj as ViewSettings;
            if(Name.CompareTo(otherViewSettings.Name) == 0 &&
               EnableSSL.CompareTo(otherViewSettings.EnableSSL) == 0 &&
               Host.CompareTo(otherViewSettings.Host) == 0 &&
               Port.CompareTo(otherViewSettings.Port) == 0 &&
               UserEmail.CompareTo(otherViewSettings.UserEmail) == 0 &&
               UserPassword.CompareTo(otherViewSettings.UserPassword) == 0 &&
               SenderName.CompareTo(otherViewSettings.SenderName) == 0 &&
               SenderEmail.CompareTo(otherViewSettings.SenderEmail) == 0 &&
               RecieverEmail.CompareTo(otherViewSettings.RecieverEmail) == 0 &&
               MessageSubject.CompareTo(otherViewSettings.MessageSubject) == 0 &&
               MessageBody.CompareTo(otherViewSettings.MessageBody) == 0 &&
               SiteUrl.CompareTo(otherViewSettings.SiteUrl) == 0 &&
               TableClassID.CompareTo(otherViewSettings.TableClassID) == 0 &&
               CompareValue.CompareTo(otherViewSettings.CompareValue) == 0 &&
               TimeStartSettings.CompareTo(otherViewSettings.TimeStartSettings) == 0 &&
               NotNullColumn.CompareTo(otherViewSettings.NotNullColumn) == 0 &&
               ColumnToCompare.CompareTo(otherViewSettings.ColumnToCompare) == 0)
                return 0;
            return 1;
        }

        protected virtual void OnPropertyChanged(string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

using EmailSender.Interfaces;
using EmailSender.ViewModels;
using EmailSender.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace EmailSender
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly string PATH = $"{Environment.CurrentDirectory}\\appSettings.json";
        private BindingList<ViewSettings> _dataSettingsList;
        private DataIOService _dataIOService;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
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
            _dataSettingsList.ListChanged += _dataSettingsList_ListChanged;
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

        }
    }
}

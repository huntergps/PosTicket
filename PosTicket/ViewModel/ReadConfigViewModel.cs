using System;
using MaterialDesignThemes.Wpf;
using MaterialDesignMessageBox;
using MaterialDesignColors;
using System.Windows.Input;
using PosTicket.Repository.Interface;
using PosTicket.Models;
using System.Windows;
using System.Collections.Generic;
using System.Threading;

namespace PosTicket.ViewModel
{
    public class ReadConfigViewModel : NavigableControlViewModel
    {
        private ReadConfig readConfig { get; set; }

        private WriteConfig writeConfig { get; set; }
        private ReadPrinter readPrinter { get; set; }
        public ICommand SaveConfigCommand { get; set; }
        public ICommand CloseWindowCommand { get; set; }

        public ReadConfigViewModel()
        {
            SaveConfigCommand = new RelayCommand(SaveConfigClick);
            readConfig = new ReadConfig();
            readPrinter = new ReadPrinter();
        }
        private void CloseWindow(object sender)
        {
            Window winObj = (Window)sender;
            if (winObj != null)
            {
                winObj.Close();
            }
        }
        private string _serverURLValue;
        public string ServerURLValue
        {
            get { return _serverURLValue; }
            set
            {
                _serverURLValue = value;
                RaisePropertyChanged("ServerURLValue");
            }
        }
        private string _ipAddressValue;
        public string IpAddressValue
        {
            get { return _ipAddressValue; }
            set
            {
                _ipAddressValue = value;
                RaisePropertyChanged("IpAddressValue");
            }
        }
        private string _selectedTicketPrinter;
        public string SelectedTicketPrinter
        {
            get { return _selectedTicketPrinter; }
            set
            {
                _selectedTicketPrinter = value;
                RaisePropertyChanged("SelectedTicketPrinter");
            }
        }
        private string _selectedPosPrinter;
        public string SelectedPosPrinter
        {
            get { return _selectedPosPrinter; }
            set
            {
                _selectedPosPrinter = value;
                RaisePropertyChanged("SelectedPosPrinter");
            }
        }
        public string _ApiKeyValue;
        public string ApiKeyValue
        {
            get { return _ApiKeyValue; }
            set
            {
                _ApiKeyValue = value;
                RaisePropertyChanged("ApiKeyValue");
            }
        }
        private string _statusUpdate;
        public string StatusUpdate
        {
            get { return _statusUpdate; }
            set
            {
                _statusUpdate = value;
                RaisePropertyChanged("StatusUpdate");
            }
        }
        private List<Config> _configList;
        public List<Config> ConfigList
        {
            get { return _configList; }
            set
            {
                _configList = value;
                RaisePropertyChanged("ConfigList");
            }
        }
        private List<string> _printerList;
        public List<string> PrinterList
        {
            get { return _printerList; }
            set
            {
                _printerList = value;
                RaisePropertyChanged("PrinterList");
            }
        }

        public Action CloseAction { get; set; }

        public void ViewLoaded()
        {
            SetStatus("Loading config...");

            Thread thread = new Thread(() => UpdateConfigList());
            thread.IsBackground = true; // Terminate process if main thread exits
            thread.Priority = ThreadPriority.Highest;
            thread.Start();
        }

        public void UpdateConfigList()
        {
            ConfigList = readConfig.GetAllConfigs();
            List<string> PrinterResult = new List<string>();
            foreach (Printer printerResult in readPrinter.GetAllPrinter())
            {
                PrinterResult.Add(printerResult.Name);
            }
            PrinterList = PrinterResult;
            ServerURLValue = ConfigList[0].server_url;
            IpAddressValue = ConfigList[0].current_ip;
            ApiKeyValue = ConfigList[0].api_key;
            SelectedPosPrinter = ConfigList[0].pos_printer;
            SelectedTicketPrinter = ConfigList[0].ticket_printer;
        }
        private void SetStatus(string status)
        {
            StatusUpdate = status;
        }
        private void ShowLoginWindow()
        {
            Login loginWindow = new Login();
            loginWindow.Show();
        }
        private void SaveConfigClick(object sender)
        {
            List<string> updatedConfig = new List<string>();
            writeConfig = new WriteConfig();
            updatedConfig.Add("server_url");
            updatedConfig.Add("pos_printer");
            updatedConfig.Add("ticket_printer");
            updatedConfig.Add("current_ip");
            if (ApiKeyValue != null)
            {
                updatedConfig.Add("api_key");
                writeConfig.UpdateConfigByField(updatedConfig[4], ApiKeyValue);
            }
            writeConfig.UpdateConfigByField(updatedConfig[0], ServerURLValue);
            writeConfig.UpdateConfigByField(updatedConfig[1], SelectedPosPrinter);
            writeConfig.UpdateConfigByField(updatedConfig[2], SelectedTicketPrinter);
            writeConfig.UpdateConfigByField(updatedConfig[3], IpAddressValue);
            ShowLoginWindow();
            CloseWindow(sender);
        }
    }
}

using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using PosTicket.Repository.Interface;
using PosTicket.Models;


namespace PosTicket.ViewModel
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private bool isBusy;
        private bool sessionOpened;

        private ReadSession readSession { get; set; }
        private ReadConfig readConfig { get; set; }
        private ReadPosSessionResponse readPosSession { get; set; }

        public MainViewModel()
        {
            readSession = new ReadSession();
            readConfig = new ReadConfig();
            readPosSession = new ReadPosSessionResponse();
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
        private PosSession _posSessionList;
        public PosSession PosSessionList
        {
            get { return _posSessionList; }
            set
            {
                _posSessionList = value;
                RaisePropertyChanged("PosSessionList");
            }
        }
        private Session _sessionList;
        public Session SessionList
        {
            get { return _sessionList; }
            set
            {
                _sessionList = value;
                RaisePropertyChanged("SessionList");
            }
        }
        private string _tokenValue;
        public string TokenValue
        {
            get { return _tokenValue; }
            set
            {
                _tokenValue = value;
                RaisePropertyChanged("TokenValue");
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
        public void ViewLoaded()
        {
            Thread thread = new Thread(() => UpdateSessionList());
            thread.IsBackground = true;
            thread.Priority = ThreadPriority.Highest;
            thread.Start();
            GetPosSession();
        }
        public void GetPosSession()
        {
            Thread thread = new Thread(() => UpdatePosSession());
            thread.IsBackground = true;
            thread.Priority = ThreadPriority.Highest;
            thread.Start();
        }
        public void UpdateSessionList()
        {
            SessionList = readSession.GetSession();
        }
        public async void UpdatePosSession()
        {
            ConfigList = readConfig.GetAllConfigs();
            IpAddressValue = ConfigList[0].current_ip;
            PosSessionList = await readPosSession.GetPosSessionAsync(IpAddressValue);
            string message = "";
            if (PosSessionList != null)
            {
                message = "goto main pos";
            }
            else 
            {
                message = "goto set deposit";
            }
            string final = message;
        }
        public bool IsBusy
        {
            get { return isBusy; }
            set
            {
                isBusy = value;
                RaisePropertyChanged("IsBusy");
            }
        }
        public bool SessionOpened
        {
            get { return sessionOpened; }
            set
            {
                sessionOpened = value;
                RaisePropertyChanged("sessionOpened");
            }
        }
        #region INotifyPropertyChanged Members
        public event PropertyChangedEventHandler PropertyChanged;
        private void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}

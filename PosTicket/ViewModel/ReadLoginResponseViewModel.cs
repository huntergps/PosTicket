using System;
using MaterialDesignThemes.Wpf;
using MaterialDesignMessageBox;
using MaterialDesignColors;
using System.Windows.Input;
using PosTicket.Repository.Interface;
using PosTicket.Models;
using PosTicket.Views;
using System.Windows;
using System.Collections.Generic;

namespace PosTicket.ViewModel
{
    public class ReadLoginResponseViewModel : NavigableControlViewModel
    {
        private ReadLoginResponse readLoginResponse { get; set; }
        private WriteSession writeSession { get; set; }
        private ReadConfig readConfig { get; set; }
        private ReadPosSessionResponse readPosSession { get; set; }
        public ICommand GetLoginResponseCommand { get; set; }
        public ICommand CloseWindowCommand { get; set; }

        public ReadLoginResponseViewModel()
        {
            readConfig = new ReadConfig();
            readPosSession = new ReadPosSessionResponse();
            GetLoginResponseCommand = new RelayCommand(GetLoginResponseClick);
            CloseWindowCommand = new RelayCommand(CloseWindow);
            SetDialogHostStatus("False");
        }
        private void CloseWindow(object sender)
        {
            Window winObj = (Window)sender;
            if (winObj != null)
            {
                winObj.Close();
            }
            Application.Current.Shutdown();
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
        public Action CloseAction {
            get;
            set;
        }
        private string _usernameValue;
        public string UsernameValue
        {
            get { return _usernameValue; }
            set
            {
                _usernameValue = value;
                RaisePropertyChanged("UsernameValue");
            }
        }
        public string _passwordValue;
        public string Password
        {
            get { return _passwordValue; }
            set 
            {
                _passwordValue = value;
                RaisePropertyChanged("Password");
            }
        }

        private string _dialogHostStatus;
        public string DialogHostStatus
        {
            get { return _dialogHostStatus; }
            set
            {
                _dialogHostStatus = value;
                RaisePropertyChanged("DialogHostStatus");
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
        private int _progressBarValue;
        public int ProgressBarValue
        {
            get { return _progressBarValue; }
            set
            {
                _progressBarValue = value;
                RaisePropertyChanged("ProgressBarValue");
            }
        }
        private void SetStatusUpdate(string status)
        {
            StatusUpdate = "status: " + status;
        }
        private void SetProgressBarValue(int value)
        {
            ProgressBarValue = value;
        }
        private void SetDialogHostStatus(string status)
        {
            DialogHostStatus = status;
        }
        private void ShowMainWindow(object sender)
        {
            ViewPosMain mainWindow = new ViewPosMain();
            Window winObj = (Window)sender;
            if (winObj != null)
            {
                winObj.Close();
            }
            mainWindow.Show();
            CloseAction();
        }
        private void ShowDepositWindow(object sender)
        {
            ViewSetDeposit depositWindow = new ViewSetDeposit();
            Window winObj = (Window)sender;
            if (winObj != null)
            {
                winObj.Close();
            }
            depositWindow.Show();
            CloseAction();
        }
        private void GetLoginResponseClick(object sender)
        {
            LoginRequest _loginRequest = new LoginRequest
            {
                username = _usernameValue,
                password = Password
            };
            GetLoginData(_loginRequest, sender);
            //Thread thread = new Thread(() => GetLoginData(_loginRequest));
            //thread.IsBackground = true; // Terminate process if main thread exits
            //thread.Priority = ThreadPriority.Highest;
            //thread.SetApartmentState(ApartmentState.STA);
            //thread.Start();
        }
        private async void GetLoginData(LoginRequest _credentialValue, object sender)
        {
            SetDialogHostStatus("True");
            SetStatusUpdate("Logging in...");
            for(int i=0;i<50;i++)
            {
                SetProgressBarValue(i);
            }
            //BeginLoading("Making Dummy Data", "Checking Credentials");
            SetStatusUpdate("Checking Permisson...");
            readLoginResponse = new ReadLoginResponse();
            LoginResponse loginResponse = await readLoginResponse.GetLoginAsync(_credentialValue);
            if (loginResponse.error == null)
            {
                for (int i = 50; i < 100; i++)
                {
                    SetProgressBarValue(i);
                }
                writeSession = new WriteSession();
                Session sessionData = new Session
                {
                    user_id = loginResponse.result.res_user.id.ToString(),
                    user_name = loginResponse.result.res_user.name,
                    user_login = _credentialValue.username,
                    token = loginResponse.result.token,
                    refresh_token = loginResponse.result.refresh_token,
                    token_live = loginResponse.result.token_live.ToString()
                };
                writeSession.UpdateSession(sessionData);
                SetStatusUpdate("Logged in.");
                ConfigList = readConfig.GetAllConfigs();
                IpAddressValue = ConfigList[0].current_ip;
                SetDialogHostStatus("False");
                SetProgressBarValue(0);
                UsernameValue = "";
                if (await readPosSession.GetPosSessionAsync(IpAddressValue) == null)
                {
                    ShowDepositWindow(sender);
                }
                else
                {
                    ShowMainWindow(sender);
                }
            }
            else
            {
                for (int i = 50; i < 100; i++)
                {
                    SetProgressBarValue(i);
                }
                MaterialMessageBox.ShowDialog(loginResponse.error.message, loginResponse.error.code.ToString(), MessageBoxButton.OK, PackIconKind.Error, PrimaryColor.LightBlue);
                UsernameValue = "";
                SetDialogHostStatus("False");
                SetProgressBarValue(0);
            }
            //EndLoading();
        }
    }
}

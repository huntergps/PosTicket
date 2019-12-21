using System;
using MaterialDesignThemes.Wpf;
using MaterialDesignMessageBox;
using MaterialDesignColors;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using PosTicket.Repository.Interface;
using PosTicket.Views;
using PosTicket.Models;
using PosTicket.Repository.PrinterData;
namespace PosTicket.ViewModel
{
    public class SetDepositViewModel : NavigableControlViewModel
    {
        private ReadConfig readConfig { get; set; }
        private ReadDepositResponse readDeposit { get; set; }
        public ICommand OpenSessionCommand { get; set; }
        public Action CloseAction { get; set; }
        public SetDepositViewModel()
        {
            OpenSessionCommand = new RelayCommand(o => OpenSessionClick("OpenSessionCommandButton"));
            readConfig = new ReadConfig();
            readDeposit = new ReadDepositResponse();
        }
        private List<Deposit> _depositList;
        public List<Deposit> DepositList
        {
            get { return _depositList; }
            set
            {
                _depositList = value;
                RaisePropertyChanged("ConfigList");
            }
        }

        private int _hundredValue;
        public int HundredValue
        {
            get { return _hundredValue; }
            set
            {
                _hundredValue = value;
                RaisePropertyChanged("OpeningBalance");
            }
        }
        private int _fiftyValue;
        public int FiftyValue
        {
            get { return _fiftyValue; }
            set
            {
                _fiftyValue = value;
                RaisePropertyChanged("OpeningBalance");
            }
        }
        private int _twentyValue;
        public int TwentyValue
        {
            get { return _twentyValue; }
            set
            {
                _twentyValue = value;
                RaisePropertyChanged("OpeningBalance");
            }
        }
        private int _tenValue;
        public int TenValue
        {
            get { return _tenValue; }
            set
            {
                _tenValue = value;
                RaisePropertyChanged("OpeningBalance");
            }
        }
        private int _fiveValue;
        public int FiveValue
        {
            get { return _fiveValue; }
            set
            {
                _fiveValue = value;
                RaisePropertyChanged("OpeningBalance");
            }
        }
        private int _twoValue;
        public int TwoValue
        {
            get { return _twoValue; }
            set
            {
                _twoValue = value;
                RaisePropertyChanged("OpeningBalance");
            }
        }
        private int _oneValue;
        public int OneValue
        {
            get { return _oneValue; }
            set
            {
                _oneValue = value;
                RaisePropertyChanged("OpeningBalance");
            }
        }
        public string OpeningBalance
        {
            get 
            {
                return ((HundredValue * 100000) + (FiftyValue * 50000) + (TwentyValue * 20000) + (TenValue * 10000) + (FiveValue * 5000)+ (TwoValue * 2000)+ (OneValue * 1000)).ToString("N0");
            }
            set { }
        }
        public List<string> linedata;
        public int OpeningBalanceInt
        {
            get
            {
                if(OpeningBalance != "0")
                {
                    if (OpeningBalance.ToString().Contains(','))
                    {
                        return Convert.ToInt32(OpeningBalance.ToString().Replace(",", ""));
                    }
                    else
                    {
                        return Convert.ToInt32(OpeningBalance.ToString().Replace(".", ""));
                    }
                }
                return Convert.ToInt32(OpeningBalance.ToString());
            }
            set { }
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

        private void OpenSessionClick(object sender)
        {
            List<DepositCashDetail> depositCashDetail = new List<DepositCashDetail>();
            if(HundredValue != 0)
            {
                depositCashDetail.Add(new DepositCashDetail {
                    amount_unit = 100000,
                    qty = HundredValue
                });
            }
            if (FiftyValue != 0)
            {
                depositCashDetail.Add(new DepositCashDetail
                {
                    amount_unit = 50000,
                    qty = FiftyValue
                });
            }
            if (TwentyValue != 0)
            {
                depositCashDetail.Add(new DepositCashDetail
                {
                    amount_unit = 20000,
                    qty = TwentyValue
                });
            }
            if (TenValue != 0)
            {
                depositCashDetail.Add(new DepositCashDetail
                {
                    amount_unit = 10000,
                    qty = TenValue
                });
            }
            if (FiveValue != 0)
            {
                depositCashDetail.Add(new DepositCashDetail
                {
                    amount_unit = 5000,
                    qty = FiveValue
                });
            }
            if (TwoValue != 0)
            {
                depositCashDetail.Add(new DepositCashDetail
                {
                    amount_unit = 2000,
                    qty = TwoValue
                });
            }
            if (OneValue != 0)
            {
                depositCashDetail.Add(new DepositCashDetail
                {
                    amount_unit = 1000,
                    qty = OneValue
                });
            }
            PrinterRepository printerRepository = new PrinterRepository();
            linedata = new List<string>(); 
            linedata.Add( "DEPOSIT KASIR - " + ConfigList[0].current_ip);
            linedata.Add( "garis");
            linedata.Add( "TOTAL DEPOSIT : " + OpeningBalanceInt.ToString());
            linedata.Add("garis");
            linedata.Add("100,000 X " + HundredValue + " = " + (100000 * HundredValue));
            linedata.Add(" 50,000 X " + FiftyValue + " = " + (50000 * FiftyValue));
            linedata.Add(" 20,000 X " + TwentyValue + " = " + (20000 * TwentyValue));
            linedata.Add(" 10,000 X " + TenValue + " = " + (10000 * TenValue));
            linedata.Add("  5,000 X " + FiveValue + " = " + (5000 * FiveValue));
            linedata.Add("  2,000 X " + TwoValue + " = " + (2000 * TwoValue));
            linedata.Add("  1,000 X " + OneValue + " = " + (1000 * OneValue));
            linedata.Add(" ");
            linedata.Add(" ");
            //linedata.Add( (char)27 + "@" + (char)27 + "p" + (char)0 + ".}");

            //printerRepository.CetakReceiptLine(ConfigList[0].pos_printer, linedata);
            printerRepository.PrintReceipt(ConfigList[0].pos_printer, linedata);

            linedata = new List<string>();
            //linedata.Add("\x1b" + "\x69"); cut
            linedata.Add((char)27 + "@" + (char)27 + "p" + (char)0 + ".}");
            printerRepository.CetakReceiptLine(ConfigList[0].pos_printer, linedata);
            //sementara
            DepositData _depositData = new DepositData
            {
                pos_ip = IpAddressValue,
                opening_cash_balance = OpeningBalanceInt,
                cash_detail = depositCashDetail
            };
            Deposit _depositRequest = new Deposit { data = _depositData };
            SendDepositData(_depositRequest);
        }
        private void ShowMainWindow()
        {
            ViewPosMain mainWindow = new ViewPosMain();
            mainWindow.Show();
            CloseAction();
        }
        private async void SendDepositData(Deposit _depositValue)
        {
            readDeposit = new ReadDepositResponse();
            PosSession depositResponse = await readDeposit.GetDepositAsync(_depositValue);
            var error = depositResponse.error != null? depositResponse.error : depositResponse.result.error;
            if (error == null)
            {
                ShowMainWindow();
                CloseAction();
            }
            else
            {
                MaterialMessageBox.ShowDialog(error.message, error.code.ToString(), MessageBoxButton.OK, PackIconKind.Error, PrimaryColor.LightBlue);
            }
        }
        public void ViewLoaded()
        {
            ConfigList = readConfig.GetAllConfigs();
            IpAddressValue = ConfigList[0].current_ip;
        }
    }
}

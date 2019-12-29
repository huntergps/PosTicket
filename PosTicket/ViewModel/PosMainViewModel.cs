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
using System.Collections.ObjectModel;
using PosTicket.Repository.PrinterData;
using System.Threading;
using System.Linq;
using System.Collections;

namespace PosTicket.ViewModel
{
    public class PosMainViewModel : NavigableControlViewModel
    {
        public ICommand CloseSessionCommand { get; set; }
        public ICommand ClosePOSCommand { get; set; }
        public ICommand CancelCloseSessionCommand { get; set; }
        public ICommand GetLockResponseCommand { get; set; }
        private ReadSession readSession { get; set; }
        public ICommand LockPosCommand { get; set; }

        //sales
        public ICommand SalesCommand { get; set; }
        public ICommand PilihSalesCommand { get; set; }
        public ICommand CancelSalesCommand { get; set; }
        private ViewSales ViewSalesWindow { get; set; }
        // sales

        //Pelanggan
        public ICommand PelangganCommand { get; set; }
        public ICommand PilihPelangganCommand { get; set; }
        public ICommand CancelPelangganCommand { get; set; }
        private ViewCustomer ViewCustomerWindow { get; set; }
        //Pelanggan

        //Referensi
        public ICommand ReferensiOkCommand { get; set; }
        public ICommand ReferensiCancelCommand { get; set; }
        
        //Referensi

        public ICommand ReprintTiketCommand { get; set; }
        public ICommand FindTicketCommand { get; set; }
        public ICommand PrintTicketCommand { get; set; }
        public ICommand CancelReprintTicketCommand { get; set; }
        public ICommand GetPermisionCommand { get; set; }
        public ICommand CancelPermisionCommand { get; set; }

        public ICommand ReprintReceiptCommand { get; set; }
        public ICommand FindReceiptCommand { get; set; }
        public ICommand PrintReceiptCommand { get; set; }
        public ICommand CancelReprintReceiptCommand { get; set; }
        public ICommand DeleteCartCommand { get; set; }
        public ICommand KeypadCommand { get; set; }
        public ICommand KeyPayCommand { get; set; }
        public ICommand NumPayCommand { get; set; }
        public ICommand NumClearPayCommand { get; set; }
        public ICommand NumDelPayCommand { get; set; }
        public ICommand DelCommand { get; set; }
        public ICommand PaymentCommand { get; set; }
        public ICommand ClosePaymentCommand { get; set; }
        public ICommand ValidatePaymentCommand { get; set; }
        private ReadConfig readConfig { get; set; }
        private ReadProductResponse readProduct { get; set; }
        private ReadPaymentResponse readPayment { get; set; }
        private PrintTicketResponse RePrintTicket { get; set; }
        private ViewPayment paymentWindow { get; set; }
        private ViewLock LockWindow { get; set; }
        private ViewReprintTicket ReprintTiketWindow { get; set; }
        private ViewReprintReceipt ReprintReceiptWindow { get; set; }
        private ViewPermision PermisionWindow { get; set; }
        private ViewsRefferensi ViewRefferensiWindow { get; set; }
        private ViewCloseSession CloseSessionWindow { get; set; }
        public Action CloseAction { get; set; }
        private ReadLoginResponse readLoginResponse { get; set; }
        private ReadPrintTicketResponse readPrintTicketResponse { get; set; }
        private ReadProductResponse readProductPriceResponse { get; set; }
        private ReadPrintReceiptResponse readPrintReceiptResponse { get; set; }
        public PosMainViewModel()
        {

            CartList = new ObservableCollection<Cart>();
            BayarList = new ObservableCollection<PayCart>();
            CloseSessionCommand = new RelayCommand(CloseSessionClick);
            ClosePOSCommand = new RelayCommand(ClosePOSClick);
            CancelCloseSessionCommand = new RelayCommand(o => CancelCloseSessionClick("CloseSessionCommandButton"));
            LockPosCommand = new RelayCommand(o => LockPosClick("LockPosCommandButton"));
            //sales
            SalesCommand = new RelayCommand(o => SalesClick("SalesCommandButton"));
            PilihSalesCommand = new RelayCommand(o => PilihSalesClick("PilihSalesButton"));
            CancelSalesCommand = new RelayCommand(o => CancelSalesClick("CancelSalesButton"));
            //sales

            //Pelanggan
            PelangganCommand = new RelayCommand(o => PelangganClick("PelangganCommandButton"));
            PilihPelangganCommand = new RelayCommand(o => PilihPelangganClick("PilihPelangganCommandButton"));
            CancelPelangganCommand = new RelayCommand(o => CancelPelangganClick("CancelPelangganButton"));
            //Pelanggan
            ReferensiOkCommand = new RelayCommand(o => ReferensiOkClick("ReferensiOkCommandButton"));
            ReferensiCancelCommand = new RelayCommand(o => ReferensiCancelClick("ReferensiCancelCommandButton"));
            //Referensi

            //Referensi
            ReprintTiketCommand = new RelayCommand(o => ReprintTiketClick("PrintTiketCommandButton"));
            GetPermisionCommand = new RelayCommand(o => GetPermisionClick("PrintTiketCommandButton"));
            FindTicketCommand = new RelayCommand(o => FindTiketClick("FindTiketCommandButton"));
            PrintTicketCommand = new RelayCommand(o => PrintTiketClick("ReprintTiketCommandButton"));
            CancelReprintTicketCommand = new RelayCommand(o => CancelReprintTiketClick("CancelReprintTiketCommandButton"));
            CancelPermisionCommand = new RelayCommand(o => CancelPermisionClick("CancelPermisionCommandButton"));

            ReprintReceiptCommand = new RelayCommand(o => ReprintReceiptClick("ReprintReceiptCommandButton"));
            FindReceiptCommand = new RelayCommand(o => FindReceiptClick());
            PrintReceiptCommand = new RelayCommand(o => ReprintReceiptClick("PrintReceiptCommandButton"));
            CancelReprintReceiptCommand = new RelayCommand(o => CancelReprintReceiptClick("CancelReprintReceiptCommandButton"));


            DeleteCartCommand = new RelayCommand(o => DeleteCartItem("DeleteCartCommandButton"));
            PaymentCommand = new RelayCommand(SetPayment);
            ClosePaymentCommand = new RelayCommand(ClosePayment);
            KeypadCommand = new RelayCommand(UpdateCartQty);
            KeyPayCommand = new RelayCommand(UpdatePayCartBayar);
            NumPayCommand = new RelayCommand(NumUpdatePayCartBayar);
            DelCommand = new RelayCommand(DelCartQty);
            NumDelPayCommand = new RelayCommand(DelPayCartBayar);
            NumClearPayCommand = new RelayCommand(ClearPayCartBayar);
            ValidatePaymentCommand = new RelayCommand(SendPaymentData);
            readConfig = new ReadConfig();
            readProduct = new ReadProductResponse();
            readPayment = new ReadPaymentResponse();
            readPrintTicketResponse = new ReadPrintTicketResponse();
            readProductPriceResponse = new ReadProductResponse();
            paymentWindow = new ViewPayment();
            paymentWindow.DataContext = this;
            GetLockResponseCommand = new RelayCommand(GetLockResponseClick);

            LockWindow = new ViewLock();
            LockWindow.DataContext = this;
            ReprintTiketWindow = new ViewReprintTicket();
            ReprintTiketWindow.DataContext = this;
            ReprintReceiptWindow = new ViewReprintReceipt();
            ReprintReceiptWindow.DataContext = this;
            PermisionWindow = new ViewPermision();
            PermisionWindow.DataContext = this;
            CloseSessionWindow = new ViewCloseSession();
            CloseSessionWindow.DataContext = this;
            ViewSalesWindow = new ViewSales();
            ViewSalesWindow.DataContext = this;
            ViewCustomerWindow = new ViewCustomer();
            ViewCustomerWindow.DataContext = this;
            ViewRefferensiWindow = new ViewsRefferensi();
            ViewRefferensiWindow.DataContext = this;

            readSession = new ReadSession();
            SessionList = readSession.GetSession();
            Username = SessionList.user_name;
            Userlogin = SessionList.user_login;
            PaymentMethodList = new List<PaymentData>();
            TicketList = new ObservableCollection<CartTicket>();
            ReceiptList = new ObservableCollection<ListReceiptResponse>();
            SumCategorylist = new List<PosSessionSaleCategory>();
            GetPaymentMethod();
            SelectedSalesPerson = new SalesPersonData();
            SelectedPelanggan = new CustomerData();
        }

        private int staticnum = -1;
        public string Username { get; set; }
        public string Userlogin { get; set; }

        private ProductCategoryData _selectedCategory;
        public ProductCategoryData SelectedCategory
        {
            get { return _selectedCategory; }
            set
            {
                _selectedCategory = value;
                RaisePropertyChanged("SelectedCategory");
            }
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

        private string _paymentname;
        public string paymentname
        {
            get { return _paymentname; }
            set
            {
                _paymentname = value;
                RaisePropertyChanged("paymentname");
            }
        }

        private string _Referensivalue;
        public string Referensivalue
        {
            get { return _Referensivalue; }
            set
            {
                _Referensivalue = value;
                RaisePropertyChanged("Referensivalue");
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
        public Session SessionList { get; set; }
        public void SetPayment(object sender)
        {
            if (_grandTotal <= 0)
            {
                MaterialMessageBox.ShowDialog("Belum ada transaksi...!");
                return;
            }
            BayarList.Clear();
            BayarList.Add(new PayCart { id = 0, totaljual = _grandTotal, bayar = 0, reff = "", typebayar = "", rowpayment=0,AddReffToPayCartCommand=new RelayCommand (AddReffToPayCartClick), DelToPayCartCommand = new RelayCommand(DelToPayCartClick)});
            RaisePropertyChanged("BayarList");
            paymentWindow.ShowDialog();
        }
        private async void GetPermisionClick(object sender)
        {
            List<int> ticket_ids = new List<int>();
            foreach (CartTicket DataTicket in TicketList)
            {
                if (DataTicket.flagprint == true)
                {
                    ticket_ids.Add(DataTicket.id);
                }
            }

            PrintTicketRequest _TicketRequest = new PrintTicketRequest
            {
                username = UsernameValue,
                password = Password,
                ticket_ids = ticket_ids

            };
            readPrintTicketResponse = new ReadPrintTicketResponse();
            PrintTicketResponse RePrintTicketResponse = await readPrintTicketResponse.PostPrintTicketRequest(_TicketRequest);
            if (RePrintTicketResponse.result[0].error == null)
            {
                PrinterRepository printerRepository = new PrinterRepository();
                await printerRepository.CetakTicket(ConfigList[0].ticket_printer, RePrintTicketResponse.result);
                UsernameValue = "";
                Password = "";
                TicketList.Clear();
                numberPos = "";
                PermisionWindow.Hide();
                ReprintTiketWindow.Hide();
            }
            else
            {
                MaterialMessageBox.ShowDialog(RePrintTicketResponse.result[0].error.message, RePrintTicketResponse.result[0].error.code.ToString(), MessageBoxButton.OK, PackIconKind.Error, PrimaryColor.LightBlue);
                UsernameValue = "";
                Password = "";
            }

        }
        private void GetLockResponseClick(object sender)
        {
            LoginRequest _loginRequest = new LoginRequest
            {
                username = Userlogin,
                password = Password
            };
            GetLoginData(_loginRequest, sender);
        }
        private async void GetLoginData(LoginRequest _credentialValue, object sender)
        {
            readLoginResponse = new ReadLoginResponse();
            LoginResponse loginResponse = await readLoginResponse.GetLoginAsync(_credentialValue);
            if (loginResponse.error == null)
            {
                LockWindow.Hide();
            } else
            {
                MaterialMessageBox.ShowDialog(loginResponse.error.message, loginResponse.error.code.ToString(), MessageBoxButton.OK, PackIconKind.Error, PrimaryColor.LightBlue);
            }
        }
        public async void GetPaymentMethod()
        {
            PaymentMethod paymentMethod = await readPayment.GetPaymentMethodAsync();
            foreach (PaymentData paymentData in paymentMethod.result)
            {
                if (paymentData.AddToCartCommand == null)
                    paymentData.AddToCartCommand = new RelayCommand(o => AddToPayCart(new PayCart { id = paymentData.id, totaljual = 0, bayar = 0, reff = paymentData.name, typebayar = paymentData.type }));
                PaymentMethodList.Add(paymentData);
            }
        }
        private decimal _opening_cash_balance;
        public decimal opening_cash_balance 
        {
            get { return _opening_cash_balance; }
            set
            {
                _opening_cash_balance = value;
                RaisePropertyChanged("opening_cash_balance");
            }
        }
        private decimal _amount_sale_cash;
        public decimal amount_sale_cash 
        {
            get { return _amount_sale_cash; }
            set
            {
                _amount_sale_cash = value;
                RaisePropertyChanged("amount_sale_cash");
            }
        }
        private decimal _total_cash_balance;
        public decimal total_cash_balance 
        {
            get { return _total_cash_balance; }
            set
            {
                _total_cash_balance = value;
                RaisePropertyChanged("total_cash_balance");
            }
        }
        private decimal _amount_sale_non_cash;
        public decimal amount_sale_non_cash 
        {
            get { return _amount_sale_non_cash; }
            set
            {
                _amount_sale_non_cash = value;
                RaisePropertyChanged("amount_sale_non_cash");
            }
        }
        private decimal _amount_sale;
        public decimal amount_sale 
        {
            get { return _amount_sale; }
            set
            {
                _amount_sale = value;
                RaisePropertyChanged("amount_sale");
            }
        }
        private int _count_ticket;
        public int count_ticket 
        {
            get { return _count_ticket; }
            set
            {
                _amount_sale = value;
                RaisePropertyChanged("count_ticket");
            }
        }

        private List<PosSessionSaleCategory> _sumCategorylist;
        public List<PosSessionSaleCategory> SumCategorylist
        {
            get { return _sumCategorylist; }
            set
            {
                _sumCategorylist = value;
                RaisePropertyChanged("SumCategorylist");
            }
        }
        private async void GetPosSessionSummary()
        {
            ReadPosSessionResponse closePosSession = new ReadPosSessionResponse();
            PosSessionClose posSessionCloseResponse = await closePosSession.GetPosSessionSummary(IpAddressValue);
            if (posSessionCloseResponse.result.error == null | posSessionCloseResponse.error == null)
            {
                SumCategorylist.Clear();
                opening_cash_balance = decimal.Parse(posSessionCloseResponse.result.opening_cash_balance.ToString());
                amount_sale_cash = decimal.Parse(posSessionCloseResponse.result.amount_sale_cash.ToString());
                total_cash_balance = decimal.Parse(posSessionCloseResponse.result.amount_sale_cash.ToString()) + decimal.Parse(posSessionCloseResponse.result.opening_cash_balance.ToString());
                amount_sale = decimal.Parse(posSessionCloseResponse.result.amount_sale.ToString());
                amount_sale_non_cash = decimal.Parse(posSessionCloseResponse.result.amount_sale_non_cash.ToString());
                count_ticket = int.Parse(posSessionCloseResponse.result.count_ticket.ToString());
                foreach (PosSessionSaleCategory listCategory in posSessionCloseResponse.result.amount_sale_by_category)
                {
                    SumCategorylist.Add(listCategory);
                    RaisePropertyChanged("SumCategorylist");
                }
                SumCategorylist = SumCategorylist.Where(SumCategorylist => SumCategorylist.sum != 0).ToList();


            }
            else
            {
                MaterialMessageBox.ShowDialog(posSessionCloseResponse.result.error.message, posSessionCloseResponse.result.error.code.ToString(), MessageBoxButton.OK, PackIconKind.Error, PrimaryColor.LightBlue);
            }
        }
        public void  AddReffToPayCartClick(object sender)
        {
            //MaterialMessageBox.ShowDialog("No row :" + sender.ToString() + " " + BayarList[int.Parse(sender.ToString())].typebayar);
            PayCart temp = new PayCart(); 
            temp = BayarList[int.Parse(sender.ToString())];
            paymentname = temp.typebayar;
            Referensivalue = temp.reff;
            if (temp.reff == "cash")
            {
                MaterialMessageBox.ShowDialog("Tidak perlu rubah referensi...!", "Input Referensi", MessageBoxButton.OK, PackIconKind.UserWarning, PrimaryColor.LightBlue);
                return;
            } 
            ViewRefferensiWindow.ShowDialog();
            temp.reff = Referensivalue;
            BayarList.RemoveAt(int.Parse(sender.ToString()));
            BayarList.Insert(int.Parse(sender.ToString()), (PayCart)temp);
            RaisePropertyChanged("BayarList");
        }
        public void ReferensiOkClick(object sender)
        {
            if (Referensivalue == "")
            {
                MaterialMessageBox.ShowDialog("Referensi Harus diisi...!", "Input Referensi", MessageBoxButton.OK, PackIconKind.UserWarning, PrimaryColor.LightBlue);
                return;
            } else
            {
                RaisePropertyChanged("Referensivalue");
            }
            ReferensiCancelClick(sender);
        }
        public void ReferensiCancelClick(object sender)
        {
            ViewRefferensiWindow.Hide();
        }
        public void DelToPayCartClick(object sender)
        {
            //MaterialMessageBox.ShowDialog("No row :" + sender.ToString());
            if (BayarList.Count - 1 == int.Parse(sender.ToString()))
            {
                MaterialMessageBox.ShowDialog("Pilih Payment yang akan dihapus...!");
                return;
            }
            else
            {
                //PayCart temp = BayarList[int.Parse(sender.ToString())];
                BayarList.RemoveAt(int.Parse(sender.ToString()));
                RaisePropertyChanged("BayarList");
                ObservableCollection<PayCart> Datanew = new ObservableCollection<PayCart>();
                //Datanew = BayarList;
                //BayarList.Clear();
                RaisePropertyChanged("BayarList");

                for (var i = 0; i <= BayarList.Count - 1; i++)
                {
                    PayCart temp = BayarList[i];

                    if (i == 0)
                    {
                        temp.totaljual = _grandTotal;
                    }
                    else
                    {
                        temp.totaljual = (float.Parse(BayarList[i - 1].kembalian.ToString()) * -1);
                    }
                    temp.rowpayment = i;
                    Datanew.Add(new PayCart { id =temp.id, totaljual = temp.totaljual, bayar = temp.bayar, reff = temp.reff, typebayar = temp.typebayar, rowpayment = i, AddReffToPayCartCommand = new RelayCommand(AddReffToPayCartClick), DelToPayCartCommand = new RelayCommand(DelToPayCartClick) });
                    RaisePropertyChanged("Datanew");
                }
                BayarList.Clear();
                BayarList = Datanew;
                RaisePropertyChanged("BayarList");
            }
        }
        public void AddToPayCart(PayCart sender)
        {
            int lastrow = BayarList.Count - 1;
            PayCart temp = BayarList[lastrow];
            if (temp.totaljual <= 0)
            {
                return;
            }
            temp.id = sender.id;
            temp.typebayar = sender.reff;
            temp.reff = sender.typebayar;
            if (sender.typebayar != "cash")
            {
                temp.bayar = temp.totaljual;
                temp.reff = sender.reff;
            }
            BayarList.RemoveAt(lastrow);
            BayarList.Insert(lastrow, (PayCart)temp);
            BayarList.Add(new PayCart { id = 0, totaljual = float.Parse(temp.kembalian.ToString()), bayar = 0, reff = "", typebayar = "", rowpayment = lastrow+1, AddReffToPayCartCommand = new RelayCommand(AddReffToPayCartClick), DelToPayCartCommand = new RelayCommand(DelToPayCartClick)});
            RaisePropertyChanged("BayarList");
        }
        public void ClosePayment(object sender)
        {
            paymentWindow.Hide();
        }
        public void CancelReprintTiketClick(object sender)
        {
            TicketList.Clear();
            numberPos = "";
            ReprintTiketWindow.Hide();
        }
        public void CancelPermisionClick(object sender)
        {
            UsernameValue = "";
            PermisionWindow.Hide();
        }
        public void CancelReprintReceiptClick(object sender)
        {
            ReceiptList.Clear();
            numberReceipt = "";
            ReprintReceiptWindow.Hide();
        }
        public void UpdatePayCartBayar(object sender)
        {
            if (BayarList.Count == 1)
            {
                MaterialMessageBox.ShowDialog("Pilih Type Pembayaran terlebih dahulu...!");
                return;
            }
            int lastrow = BayarList.Count - 2;
            double byr = double.Parse(sender.ToString() + "000");
            PayCart temp = new PayCart();
            temp = BayarList[lastrow];
            temp.bayar = temp.bayar + byr;
            //temp.AddReffToPayCartCommand = new RelayCommand(o => AddReffToPayCartClick());
            BayarList.RemoveAt(lastrow);
            BayarList.Insert(lastrow, (PayCart)temp);
            
            lastrow = BayarList.Count - 1;
            PayCart temp2 = new PayCart();
            temp2 = BayarList[lastrow];
            temp2.totaljual = float.Parse(temp.kembalian.ToString()) * -1;
            //temp2.AddReffToPayCartCommand = new RelayCommand(o => AddReffToPayCartClick());
            BayarList.RemoveAt(lastrow);
            BayarList.Insert(lastrow, (PayCart)temp2);
            RaisePropertyChanged("BayarList");
        }
        public void NumUpdatePayCartBayar(object sender)
        {
            if (BayarList.Count == 1)
            {
                MaterialMessageBox.ShowDialog("Pilih Type Pembayaran terlebih dahulu...!");
                return;
            }
            int lastrow = BayarList.Count - 2;
            //float byr = float.Parse(sender.ToString() + "000");
            PayCart temp = new PayCart();
            temp = BayarList[lastrow];
            if (temp.bayar == 0)
            {
                temp.bayar = float.Parse(sender.ToString());
            }
            else
            {

                if (temp.reff != "cash")
                {
                    if (temp.totaljual < double.Parse(temp.bayar + sender.ToString()))
                    {
                        MaterialMessageBox.ShowDialog("Jika Non Tunai Nila Bayar Tidak Boleh Besar Dari total Jual...!", "Input Referensi", MessageBoxButton.OK, PackIconKind.UserWarning, PrimaryColor.LightBlue);
                        return;
                    }
                }
                temp.bayar = double.Parse(temp.bayar + sender.ToString());
            }

            //temp.AddReffToPayCartCommand = new RelayCommand(o => AddReffToPayCartClick());
            temp.rowpayment = lastrow;
            BayarList.RemoveAt(lastrow);
            BayarList.Insert(lastrow, (PayCart)temp);

            lastrow = BayarList.Count - 1;
            PayCart temp2 = new PayCart();
            temp2 = BayarList[lastrow];
            temp2.totaljual = float.Parse(temp.kembalian.ToString()) * -1;
            //temp2.AddReffToPayCartCommand = new RelayCommand(o => AddReffToPayCartClick());
            BayarList.RemoveAt(lastrow);
            BayarList.Insert(lastrow, (PayCart)temp2);
            RaisePropertyChanged("BayarList");
        }
        public void ClearPayCartBayar(object sender)
        {
            if (BayarList.Count == 1)
            {
                MaterialMessageBox.ShowDialog("Pilih Type Pembayaran terlebih dahulu...!");
                return;
            }
            int lastrow = BayarList.Count - 2;
            //float byr = float.Parse(sender.ToString() + "000");
            PayCart temp = new PayCart();
            temp = BayarList[lastrow];
            temp.bayar = 0;
            temp.rowpayment = lastrow;
            BayarList.RemoveAt(lastrow);
            BayarList.Insert(lastrow, (PayCart)temp);

            lastrow = BayarList.Count - 1;
            PayCart temp2 = new PayCart();
            temp2 = BayarList[lastrow];
            temp2.totaljual = float.Parse(temp.kembalian.ToString()) * -1;
            BayarList.RemoveAt(lastrow);
            BayarList.Insert(lastrow, (PayCart)temp2);
            RaisePropertyChanged("BayarList");
        }
        public void UpdateCartQty(object sender)
        {
            if (SelectedItem != null)
            {
                int index = CartList.IndexOf((Cart)SelectedItem);
                Cart temp = new Cart();
                temp = SelectedItem;
                if (staticnum != index)
                {
                    SelectedItem.qty = int.Parse(sender.ToString());
                } else
                {
                    SelectedItem.qty = int.Parse(SelectedItem.qty.ToString() + sender.ToString());
                }
                staticnum = index;
                CartList.RemoveAt(index);
                CartList.Insert(index, (Cart)temp);
                SelectedItem = temp;
                RaisePropertyChanged("CartList");
                RaisePropertyChanged("GrandTotal");
                RaisePropertyChanged("TotalTiket");
            }
        }
        public void DelPayCartBayar(object sender)
        {
            if (BayarList.Count == 1)
            {
                MaterialMessageBox.ShowDialog("Pilih Type Pembayaran terlebih dahulu...!");
                return;
            }
            int lastrow = BayarList.Count - 2;
            PayCart temp = new PayCart();
            temp = BayarList[lastrow];
            if (temp.bayar.ToString().Length == 1)
            {
                temp.bayar = 0;
            }
            else
            {
                temp.bayar = double.Parse(temp.bayar.ToString().Substring(0, temp.bayar.ToString().Length - 1));
            }
            temp.rowpayment = lastrow;
            BayarList.RemoveAt(lastrow);
            BayarList.Insert(lastrow, (PayCart)temp);

            lastrow = BayarList.Count - 1;
            PayCart temp2 = new PayCart();
            temp2 = BayarList[lastrow];
            temp2.totaljual = float.Parse(temp.kembalian.ToString()) * -1;
            BayarList.RemoveAt(lastrow);
            BayarList.Insert(lastrow, (PayCart)temp2);
            RaisePropertyChanged("BayarList");
        }
        public void DelCartQty(object sender)
        {
            if (SelectedItem != null)
            {
                int index = CartList.IndexOf((Cart)SelectedItem);
                Cart temp = new Cart();
                temp = SelectedItem;
                if (SelectedItem.qty.ToString().Length == 1)
                {
                    SelectedItem.qty = 0;
                } else
                {
                    SelectedItem.qty = int.Parse(SelectedItem.qty.ToString().Substring(0, SelectedItem.qty.ToString().Length - 1));
                }

                CartList.RemoveAt(index);
                CartList.Insert(index, (Cart)temp);
                SelectedItem = temp;
                RaisePropertyChanged("CartList");
                RaisePropertyChanged("GrandTotal");
                RaisePropertyChanged("TotalTiket");
            }
        }
        public void DeleteCartItem(object sender)
        {
            if (CartList != null)
            {
                CartList.Remove(SelectedItem);
            }
            RaisePropertyChanged("CartList");
            RaisePropertyChanged("GrandTotal");
            RaisePropertyChanged("TotalTiket");
        }
        public void AddToCart(Cart sender)
        {
            
            bool IsNew = true;
            int Dataindex = 0;
            if (CartList != null)
            {
                foreach (Cart cartItem in CartList)
                {

                    if (cartItem.id == sender.id)
                    {
                        IsNew = false;
                        cartItem.qty += 1;
                        int index = Dataindex;
                        Cart temp = new Cart();
                        temp = cartItem;
                        CartList.RemoveAt(index);
                        CartList.Insert(index, (Cart)temp);
                        break;
                    }
                    else
                    {
                        IsNew = true;
                    }
                    Dataindex += 1;
                }
                if (IsNew == true)
                {
                    if (sender.qty_minimum != 0)
                    {
                        sender.qty = sender.qty_minimum;
                    }
                    ProductPriceRequest paramRequest = new ProductPriceRequest();
                    paramRequest.product_id = sender.id;
                    paramRequest.qty = int.Parse(sender.qty.ToString());
                    GetProductPrice(paramRequest);
                    sender.qty_bonus = int.Parse(dataprice.qty_bonus.ToString());
                    CartList.Add(sender);
                }
            }
            RaisePropertyChanged("CartList");
            RaisePropertyChanged("GrandTotal");
            RaisePropertyChanged("TotalTiket");
        }
        private PayCart _SelectedPayment;
        public PayCart SelectedPayment
        {
            get
            {
                return _SelectedPayment;
            }
            set
            {
                _SelectedPayment = value;
                RaisePropertyChanged("SelectedPayment");
                RaisePropertyChanged("BayarList");
            }
        }
        private Cart _selectedItem;
        public Cart SelectedItem
        {
            get
            {
                return _selectedItem;
            }
            set
            {
                _selectedItem = value;
                RaisePropertyChanged("SelectedItem");
                RaisePropertyChanged("CartList");
            }
        }
        private ObservableCollection<ProductCategoryData> _productCategoryList;
        public ObservableCollection<ProductCategoryData> ProductCategoryList
        {
            get { return _productCategoryList; }
            set
            {
                _productCategoryList = value;
                RaisePropertyChanged("ProductCategoryList");
            }
        }

        private ObservableCollection<Cart> _cartList;
        public ObservableCollection<Cart> CartList
        {
            get { return _cartList; }
            set
            {
                _cartList = value;
                RaisePropertyChanged("CartList");
                RaisePropertyChanged("GrandTotal");
                RaisePropertyChanged("TotalTiket");
            }
        }
        private ObservableCollection<PayCart> _BayarList;
        public ObservableCollection<PayCart> BayarList
        {
            get { return _BayarList; }
            set
            {
                _BayarList = value;
                RaisePropertyChanged("BayarList");
            }
        }

        private float _grandTotal;
        public string GrandTotal
        {
            get
            {
                _grandTotal = 0;
                foreach (Cart cartItem in CartList)
                {
                    _grandTotal += cartItem.total;
                }
                return _grandTotal.ToString("N0");
            }
            set
            {
                RaisePropertyChanged("GrandTotal");
            }
        }
        private float _TotalTiket;
        public string TotalTiket
        {
            get
            {
                _TotalTiket = 0;
                foreach (Cart cartItem in CartList)
                {
                    _TotalTiket += cartItem.qty;
                }
                return _TotalTiket.ToString("N0");
            }
            set
            {
                RaisePropertyChanged("TotalTiket");
            }
        }

        private List<PaymentData> _paymentMethodList;
        public List<PaymentData> PaymentMethodList
        {
            get { return _paymentMethodList; }
            set
            {
                _paymentMethodList = value;
                RaisePropertyChanged("PaymentMethodList");
            }
        }
        private List<ProductData> _productList;
        public List<ProductData> ProductList
        {
            get { return _productList; }
            set
            {
                _productList = value;
                RaisePropertyChanged("ProductList");
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
        private PosSessionCloseRequest _posSessionCloseData;
        public PosSessionCloseRequest PosSessionCloseData
        {
            get { return _posSessionCloseData; }
            set
            {
                _posSessionCloseData = value;
                RaisePropertyChanged("PosSessionCloseRequest");
            }
        }


        private PosSessionClose _posSessionCloseResponse;
        public PosSessionClose PosSessionCloseResponse
        {
            get { return _posSessionCloseResponse; }
            set
            {
                _posSessionCloseResponse = value;
                RaisePropertyChanged("PosSessionCloseResponse");
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
        private void LockPosClick(object sender)
        {
            LockWindow.ShowDialog();
        }
        private string _numberPos;
        public string numberPos
        {
            get { return _numberPos; }
            set
            {
                _numberPos = value;
                RaisePropertyChanged("numberPos");
            }
        }
        private string _numberReceipt;
        public string numberReceipt
        {
            get { return _numberReceipt; }
            set
            {
                _numberReceipt = value;
                RaisePropertyChanged("numberReceipt");
            }
        }
        private void FindTiketClick(object sender)
        {
            GetTicketList(numberPos);
        }
        private PrintTicketRequest _printTicketRequest;
        public PrintTicketRequest PrintTicketRequestData
        {
            get { return _printTicketRequest; }
            set
            {
                _printTicketRequest = value;
                RaisePropertyChanged("PrintTicketRequestData");
            }
        }
        private void GetPrintTiketClick(object sender)
        {
            PrintTicketRequestData = new PrintTicketRequest();

            GetTicketList(numberPos);
        }
        private ObservableCollection<SalesPersonData> _SalesList;
        public ObservableCollection<SalesPersonData> SalesList
        {
            get { return _SalesList; }
            set
            {
                _SalesList = value;
                RaisePropertyChanged("_SalesList");
            }
        }
        private ObservableCollection<CustomerData> _PelangganList;
        public ObservableCollection<CustomerData> PelangganList
        {
            get { return _PelangganList; }
            set
            {
                _PelangganList = value;
                RaisePropertyChanged("_PelangganList");
            }
        }
        private async void SalesClick(object sender)
        {
            ReadPartnerResponse readSalesResponse = new ReadPartnerResponse();
            SalesPerson salesPerson = await readSalesResponse.GetSalesPersonAsync();
            SalesList = new ObservableCollection<SalesPersonData>();
            if (salesPerson.result[0].error == null)
            {
                foreach (SalesPersonData ListsalesPerson in salesPerson.result)
                {
                    SalesList.Add(ListsalesPerson);
                }
                ViewSalesWindow.ShowDialog();
            } else
            {
                MaterialMessageBox.ShowDialog(salesPerson.result[0].error.message, salesPerson.result[0].error.code.ToString(), MessageBoxButton.OK, PackIconKind.Error, PrimaryColor.LightBlue);
            }


        }
        private async void PelangganClick(object sender)
        {
            ReadPartnerResponse readCostumerRespone = new ReadPartnerResponse();
            Customer Pelanggan = await readCostumerRespone.GetCustomerAsync();
            PelangganList = new ObservableCollection<CustomerData>();
            if (Pelanggan.result[0].error == null)
            {
                foreach (CustomerData ListdataPelanggan in Pelanggan.result)
                {
                    PelangganList.Add(ListdataPelanggan);
                }
                ViewCustomerWindow.ShowDialog();
            }
            else
            {
                MaterialMessageBox.ShowDialog(Pelanggan.result[0].error.message, Pelanggan.result[0].error.code.ToString(), MessageBoxButton.OK, PackIconKind.Error, PrimaryColor.LightBlue);
            }


        }
        private void PilihSalesClick(object sender)
        {
            
            ViewSalesWindow.Hide();
        }
        private void PilihPelangganClick(object sender)
        {

            ViewCustomerWindow.Hide();
        }
        private void CancelSalesClick(object sender)
        {
            SelectedSalesPerson= null;
            ViewSalesWindow.Hide();
        }
        private void CancelPelangganClick(object sender)
        {
            SelectedPelanggan = null;
            ViewCustomerWindow.Hide();
        }
        private void PrintTiketClick(object sender)
        {
            PermisionWindow.ShowDialog();
        }
        private void ReprintTiketClick(object sender)
        {
            ReprintTiketWindow.ShowDialog();
        }
        private void FindReceiptClick()
        {
            GetReceiptList(numberReceipt);
        }
        private void ReprintReceiptClick(object sender)
        {
            ReprintReceiptWindow.ShowDialog();
        }
        
        private void CloseSessionClick(object sender)
        {
            Thread thread = new Thread(() => GetPosSessionSummary());
            thread.IsBackground = true;
            thread.Priority = ThreadPriority.Highest;
            thread.Start();
            CloseSessionWindow.ShowDialog();
        }
        public List<string> linedata;
        private void ClosePOSClick(object sender)
        {
            PrinterRepository printerRepository = new PrinterRepository();
            linedata = new List<string>();
            linedata.Add("CLOSSING POS - " + ConfigList[0].current_ip);
            linedata.Add("garis");
            linedata.Add("Opening Cash Balance : " + opening_cash_balance);
            linedata.Add("Sale Cash Balance : " + amount_sale_cash);
            linedata.Add("Total Cash Balance : " + (opening_cash_balance + amount_sale_cash));
            linedata.Add(" ");
            linedata.Add("Sales Non Cash :" + amount_sale_non_cash);
            linedata.Add(" ");
            linedata.Add("Total Sales : " + amount_sale);
            linedata.Add(" ");
            linedata.Add("Total Tiket by Category : ");
            foreach (PosSessionSaleCategory datacategory in SumCategorylist)
            {
                linedata.Add(datacategory.name + " : " + datacategory.sum );
            }
            linedata.Add(" ");
            linedata.Add(" ");
            printerRepository.PrintReceipt(ConfigList[0].pos_printer, linedata);

            linedata = new List<string>();
            //linedata.Add("\x1b" + "\x69"); cut
            linedata.Add((char)27 + "@" + (char)27 + "p" + (char)0 + ".}");
            printerRepository.CetakReceiptLine(ConfigList[0].pos_printer, linedata);

            ClosePosSession(PosSessionCloseData);
        }
        private void CancelCloseSessionClick(object sender)
        {
            CloseSessionWindow.Hide();
        }

        private async void ClosePosSession(PosSessionCloseRequest _posSessionCloseRequest)
        {
            ClosingFinal posSessionCloseResponse = new ClosingFinal();
            ReadPosSessionResponse closePosSession = new ReadPosSessionResponse();
            posSessionCloseResponse      = await closePosSession.ClosePosSessionAsync(_posSessionCloseRequest);
            if (posSessionCloseResponse.result.error == null)
            {
                System.Windows.Application.Current.Shutdown();
            }
            else
            {
                MaterialMessageBox.ShowDialog(posSessionCloseResponse.result.error.message, posSessionCloseResponse.result.error.code.ToString(), MessageBoxButton.OK, PackIconKind.Error, PrimaryColor.LightBlue);
            }
        }
        private async void LoadProduct()
        {
            ObservableCollection<ProductCategoryData> productCategoryDatas = new ObservableCollection<ProductCategoryData>();
            List<ProductData> productDatas = new List<ProductData>();
            ProductCategory productCategory = await readProduct.GetProductCategoryAsync();
            Product product = await readProduct.GetProductAsync();
            foreach (ProductData productData in product.result)
            {
                productDatas.Add(productData);
            }
            ProductList = productDatas;
            foreach (ProductCategoryData productCategoryData in productCategory.result)
            {
                foreach (ProductData productData in ProductList)
                {
                    if (productCategoryData.id == productData.category_id)
                    {
                        if (productData.AddToCartCommand == null)
                            productData.AddToCartCommand = new RelayCommand(o => AddToCart(new Cart {
                                id = productData.id,
                                name = productData.name,
                                qty = 1,
                                price = productData.price,
                                qty_minimum =productData.qty_minimum,
                                product_price_id = productData.product_price_id
                            }));
                        if (productCategoryData.products == null) productCategoryData.products = new List<ProductData>();
                        productCategoryData.products.Add(productData);
                    }
                }
                if (productCategoryData.products != null)
                {
                    productCategoryDatas.Add(productCategoryData);
                }
            }
            ProductCategoryList = productCategoryDatas;
            SelectedCategory = ProductCategoryList[0];
        }
        public void ViewLoaded()
        {
            ConfigList = readConfig.GetAllConfigs();
            LoadProduct();
            IpAddressValue = ConfigList[0].current_ip;
            PosSessionCloseData = new PosSessionCloseRequest { data = new PosSessionCloseRequestData { pos_ip = IpAddressValue } };
        }
        public string salesname {
            get 
            {
                if (SelectedSalesPerson != null)
                { 
                    return SelectedSalesPerson.name; 
                } else
                {
                    return "POS Sales";
                }

            }
            set { } }

        private SalesPersonData _SelectedSalesPerson;
        public SalesPersonData SelectedSalesPerson
        {
            get
            {
                return _SelectedSalesPerson;
            }
            set
            {
                _SelectedSalesPerson = value;
                RaisePropertyChanged("SelectedSalesPerson");
            }
        }
        public string namepelanggan
        {
            get
            {
                if (SelectedPelanggan != null)
                {
                    return SelectedPelanggan.name;
                }
                else
                {
                    return "Pelanggan Umum";
                }

            }
            set { }
        }
        private CustomerData _SelectedPelanggan;
        public CustomerData SelectedPelanggan
        {
            get
            {
                return _SelectedPelanggan;
            }
            set
            {
                _SelectedPelanggan = value;
                RaisePropertyChanged("SelectedPelanggan");
            }
        }
        private ListTicketResponse _SelectedTicketList;
        public ListTicketResponse SelectedTicketList
        {
            get
            {
                return _SelectedTicketList;
            }
            set
            {
                _SelectedTicketList = value;
                RaisePropertyChanged("SelectedTicketList");
                RaisePropertyChanged("TicketList");
            }
        }

        private ObservableCollection<CartTicket> _TicketList;
        public ObservableCollection<CartTicket> TicketList
        {
            get { return _TicketList; }
            set
            {
                _TicketList = value;
                RaisePropertyChanged("_TicketList");
            }
        }
        private async void GetTicketList(string _salesnumber)
        {
            readPrintTicketResponse = new ReadPrintTicketResponse();
            ReprintTicketResponse PrintTicketResponse = await readPrintTicketResponse.GetListTicketBySale(_salesnumber);
            if (PrintTicketResponse.result[0].error == null)
            {
                
                foreach(ListTicketResponse listTicketResponse in PrintTicketResponse.result)
                {
                    TicketList.Add( new CartTicket { id = listTicketResponse.id, barcode = listTicketResponse.barcode,
                                                    product_name = listTicketResponse.product_name,date_plan = listTicketResponse.date_plan,
                                                    state = listTicketResponse.state,flagprint=false
                                                    });
                }
                
            }
            else
            {
                MaterialMessageBox.ShowDialog(PrintTicketResponse.result[0].error.message.ToString(), PrintTicketResponse.result[0].error.code.ToString(), MessageBoxButton.OK, PackIconKind.Error, PrimaryColor.LightBlue);
            }


        }
        private ProductPriceData _dataprice;
        public ProductPriceData dataprice
        {
            get { return _dataprice; }
            set
            {
                _dataprice = value;
                RaisePropertyChanged("dataprice");
            }
        }
        private async void GetProductPrice(ProductPriceRequest ParamProduct)
        {   
            readProductPriceResponse = new ReadProductResponse();
            dataprice = new ProductPriceData();
            ProductPrice ProductPriceResponse = await readProductPriceResponse.GetProductPrice(ParamProduct);
            if (ProductPriceResponse.result.error == null)
            {
                dataprice.price_unit = ProductPriceResponse.result.price_unit;
                dataprice.product_price_id = ProductPriceResponse.result.product_price_id;
                dataprice.qty_bonus = ProductPriceResponse.result.qty_bonus;
            }
            else
            {
                MaterialMessageBox.ShowDialog(ProductPriceResponse.result.error.message.ToString(), ProductPriceResponse.result.error.code.ToString(), MessageBoxButton.OK, PackIconKind.Error, PrimaryColor.LightBlue);
            }

        }
        private ObservableCollection<ListReceiptResponse> _ReceiptList;
        public ObservableCollection<ListReceiptResponse> ReceiptList
        {
            get { return _ReceiptList; }
            set
            {
                _ReceiptList = value;
                RaisePropertyChanged("ReceiptList");
            }
        }
        private async void GetReceiptList(string _receiptNumber)
        {
            readPrintReceiptResponse = new ReadPrintReceiptResponse();
            ReprintReceiptResponse PrintReceiptResponse = await readPrintReceiptResponse.GetReceiptBySale(_receiptNumber);
            if (PrintReceiptResponse.result[0].error == null)
            {

                foreach (ListReceiptResponse listReceiptResponse in PrintReceiptResponse.result)
                {
                    ReceiptList.Add(listReceiptResponse);
                }

            }
            else
            {
                MaterialMessageBox.ShowDialog(PrintReceiptResponse.result[0].error.message.ToString(), PrintReceiptResponse.result[0].error.code.ToString(), MessageBoxButton.OK, PackIconKind.Error, PrimaryColor.LightBlue);
            }

        }
        private async void SendPaymentData(object sender)
        {
            if (BayarList.Count - 1== 0)
            {
                MaterialMessageBox.ShowDialog("Lakukan Pembayaran terlebih dahulu...!");
                return;
            }
            PaymentTransactionRequest paymentTransactionRequest = new PaymentTransactionRequest();

            List<PaymentTransactionLineData> lineTransaksi = new List<PaymentTransactionLineData>();
            foreach (Cart transactionLine in CartList)
            {
                lineTransaksi.Add(new PaymentTransactionLineData {
                    sequence = CartList.IndexOf(transactionLine) + 1,
                    product_id = transactionLine.id,
                    product_price_id = transactionLine.product_price_id,
                    qty = transactionLine.qty,
                    qty_bonus = 0,
                    price_unit = transactionLine.price,
                });
            }

            BayarList.Remove(BayarList[BayarList.Count - 1]);
            List<PaymentTransactionPaymentData> linePayment = new List<PaymentTransactionPaymentData>();

            foreach (PayCart paymentLine in BayarList)
            {
                float jmlbayar;
                if (paymentLine.kembalian > 0)
                {
                    jmlbayar = paymentLine.totaljual;
                } else
                {
                    jmlbayar = float.Parse(paymentLine.bayar.ToString());
                }

                linePayment.Add(new PaymentTransactionPaymentData { payment_method_id = paymentLine.id, amount = jmlbayar, reference = paymentLine.reff });
            }
            paymentTransactionRequest.data = new PaymentTransactionRequestData {
                pos_ip = IpAddressValue,
                date_plan = DateTime.Now.ToString("yyyy-MM-dd"),
                line_ids = lineTransaksi,
                payment_ids = linePayment,
                salesperson_id = SelectedSalesPerson != null ? SelectedSalesPerson.id:0 ,
                partner_id = SelectedPelanggan != null ? SelectedPelanggan.id:0
            };
            PaymentTransactionResponse paymentResponse = await readPayment.PostTransactionPayment(paymentTransactionRequest);
            if (paymentResponse.result != null || paymentResponse.error == null)
            {
                if(paymentResponse.result.error != null)
                {
                    MaterialMessageBox.ShowDialog(paymentResponse.result.error.message, paymentResponse.result.error.code.ToString(), MessageBoxButton.OK, PackIconKind.Error, PrimaryColor.LightBlue);
                }
                else
                {
                    PrinterRepository printerRepository = new PrinterRepository();
                    //print receipt 
                    linedata = new List<string>();
                    linedata.Add("Produk          Qty     Price      Total");
                    linedata.Add("garis");
                    foreach (Cart Datajual in CartList)
                    {
                        linedata.Add(Datajual.name + "   " + Datajual.qty + "   " + Datajual.price + "    " + Datajual.total);
                    }
                    linedata.Add("garis");
                    linedata.Add(" ");
                    linedata.Add(" ");
                    //linedata.Add( (char)27 + "@" + (char)27 + "p" + (char)0 + ".}");

                    //printerRepository.CetakReceiptLine(ConfigList[0].pos_printer, linedata);
                    printerRepository.PrintReceipt(ConfigList[0].pos_printer, linedata);

                    linedata = new List<string>();
                    //linedata.Add("\x1b" + "\x69"); cut
                    linedata.Add((char)27 + "@" + (char)27 + "p" + (char)0 + ".}");
                    printerRepository.CetakReceiptLine(ConfigList[0].pos_printer, linedata);


                    await printerRepository.CetakTicket(ConfigList[0].ticket_printer, paymentResponse.result.tickets);
                    //close payment window
                    CartList.Clear();

                    paymentWindow.Hide();
                    RaisePropertyChanged("GrandTotal");
                    RaisePropertyChanged("TotalTiket");
                }
            }
            else
            {
                MaterialMessageBox.ShowDialog(paymentResponse.error.message, paymentResponse.error.code.ToString(), MessageBoxButton.OK, PackIconKind.Error, PrimaryColor.LightBlue);
            }
        }
    }
}

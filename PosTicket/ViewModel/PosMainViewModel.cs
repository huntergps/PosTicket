﻿using System;
using MaterialDesignThemes.Wpf;
using MaterialDesignMessageBox;
using MaterialDesignColors;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using PosTicket.Repository.Interface;
using PosTicket.Views;
using System.Linq;
using PosTicket.Models;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using PosTicket.Repository.PrinterData;
using PosTicket.Repository.UserSession;

namespace PosTicket.ViewModel
{
    public class PosMainViewModel : NavigableControlViewModel
    {
        public ICommand CloseSessionCommand { get; set; }
        public ICommand GetLockResponseCommand { get; set; }
        private ReadSession readSession { get; set; }
        public ICommand LockPosCommand { get; set; }
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
        private ViewPayment paymentWindow { get; set; }
        private ViewLock LockWindow { get; set; }
        public Action CloseAction { get; set; }
        private ReadLoginResponse readLoginResponse { get; set; }
        public PosMainViewModel()
        {

            CartList = new ObservableCollection<Cart>();
            BayarList = new ObservableCollection<PayCart>();
            CloseSessionCommand = new RelayCommand(o => CloseSessionClick("CloseSessionCommandButton"));
            LockPosCommand = new RelayCommand(o => LockPosClick("LockPosCommandButton"));
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
            paymentWindow = new ViewPayment();
            paymentWindow.DataContext = this;
            GetLockResponseCommand = new RelayCommand(GetLockResponseClick);
            LockWindow = new ViewLock();
            LockWindow.DataContext = this;
            readSession = new ReadSession();
            SessionList = readSession.GetSession();
            Username = SessionList.user_name;
            PaymentMethodList = new List<PaymentData>();
            GetPaymentMethod();
        }

        private int staticnum = -1;
        public string Username { get; set; }

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
            BayarList.Add(new PayCart { id = 0, totaljual = _grandTotal, bayar = 0, reff = "", typebayar = "" });
            RaisePropertyChanged("BayarList");
            paymentWindow.ShowDialog();
        }
        private void GetLockResponseClick(object sender)
        {
            LoginRequest _loginRequest = new LoginRequest
            {
                username = Username,
                password = Password
            };
            GetLoginData(_loginRequest, sender);
            readLoginResponse = new ReadLoginResponse();
        }
        private async void GetLoginData(LoginRequest _credentialValue, object sender)
        {
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
            if (sender.typebayar != "cash")
            {
                temp.bayar = temp.totaljual;
            }
            BayarList.RemoveAt(lastrow);
            BayarList.Insert(lastrow, (PayCart)temp);
            BayarList.Add(new PayCart { id = 0, totaljual = float.Parse(temp.kembalian.ToString()), bayar = 0, reff = "", typebayar = "" });
            RaisePropertyChanged("BayarList");
        }
        public void ClosePayment(object sender)
        {
            paymentWindow.Hide();
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
            } else
            {
                temp.bayar = double.Parse(temp.bayar + sender.ToString());
            }

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
                RaisePropertyChanged("_BayarList");
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
        private void CloseSessionClick(object sender)
        {
            ClosePosSession(PosSessionCloseData);
        }
        private async void ClosePosSession(PosSessionCloseRequest _posSessionCloseRequest)
        {
            PosSessionClose posSessionCloseResponse = new PosSessionClose();
            ReadPosSessionResponse closePosSession = new ReadPosSessionResponse();
            posSessionCloseResponse = await closePosSession.ClosePosSessionAsync(_posSessionCloseRequest);
            if (posSessionCloseResponse.result.error == null)
            {
                CloseAction();
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
        }
        public void ViewLoaded()
        {
            ConfigList = readConfig.GetAllConfigs();
            LoadProduct();
            IpAddressValue = ConfigList[0].current_ip;
            PosSessionCloseData = new PosSessionCloseRequest { data = new PosSessionCloseRequestData { pos_ip = IpAddressValue } };
        }
        private async void SendPaymentData(object sender)
        {

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
                    price_unit = transactionLine.price
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
                payment_ids = linePayment
            };
            PaymentTransactionResponse paymentResponse = await readPayment.PostTransactionPayment(paymentTransactionRequest);
            if (paymentResponse.result != null || paymentResponse.error == null)
            {
                PrinterRepository printerRepository = new PrinterRepository();
                printerRepository.CetakReceipt(ConfigList[0].pos_printer, new Receipt
                {
                    line1 = "========================================",
                    line2 = "            Saloka Theme Park             ",
                    line3 = "========================================",
                    line4 = "           Struk Pembelian Ticket         ",
                    line5 = "========================================",
                    line6 = (char)27+"@"+ (char)27+"p"+(char)0+".}"
                }) ;
                await printerRepository.CetakTicket(ConfigList[0].ticket_printer, paymentResponse.result.tickets);
                //close payment window
                CartList.Clear();
                paymentWindow.Hide();
                RaisePropertyChanged("GrandTotal");
                RaisePropertyChanged("TotalTiket");
            }
            else
            {
                MaterialMessageBox.ShowDialog(paymentResponse.error.message, paymentResponse.error.code.ToString(), MessageBoxButton.OK, PackIconKind.Error, PrimaryColor.LightBlue);
            }
        }
    }
}

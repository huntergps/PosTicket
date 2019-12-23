using System;
using System.ComponentModel;
using System.Windows.Input;

namespace PosTicket.Repository.Interface
{
    public class PayCart
    {
        public int id { get; set; }
        public float totaljual { get; set; }
        public double bayar { get; set; }
        public string reff { get; set; }
        public string typebayar { get; set; }
        public int rowpayment { get; set; }
        public double kembalian
        {
            get
            {
                return  bayar - totaljual ;
            }
            set { }
        }
        public ICommand AddReffToPayCartCommand { get; set; }
        public ICommand DelToPayCartCommand { get; set; }
    }
}
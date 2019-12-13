using System;
using System.Collections.Generic;
using System.Windows.Input;
using System.Text;

namespace PosTicket.Repository.Interface
{
    public class PaymentMethod
    {
        public List<PaymentData> result { get; set; }
        public string version { get; set; }
    }
    public class PaymentData
    {
        public ErrorMessage error { get; set; }
        public int id { get; set; }
        public string name { get; set; }
        public string type { get; set; }
        public ICommand AddToCartCommand { get; set; }
    }
}
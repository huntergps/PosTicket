using System;
using System.ComponentModel;

namespace PosTicket.Repository.Interface
{
    public class Cart
    {
        public int id { get; set; }
        public int product_price_id { get; set; }
        public string name { get; set; }
        public int qty { get; set; }
        public float price { get; set; }
        public float total {
            get
            {
                return qty * price;
            }
            set{} 
        }

        public Action<object, PropertyChangedEventArgs> PropertyChanged { get; internal set; }
    }
}

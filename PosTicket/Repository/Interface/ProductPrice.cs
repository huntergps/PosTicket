using System;
using System.Collections.Generic;
using System.Windows.Input;
using System.Text;

namespace PosTicket.Repository.Interface
{
    public class ProductPrice
    {
        public ProductPriceData result { get; set; }
        public string version { get; set; }
    }
    public class ProductPriceData
    {
        public ErrorMessage error { get; set; }
        public List<AllowedPaymentMethod> allowed_payment_method_ids { get; set; }
        public bool success { get; set; }
        public int product_price_id { get; set; }
        public float price_unit { get; set; }
        public int qty_bonus { get; set; }
        
    }
    public class AllowedPaymentMethod
    {
        public int id { get; set; }
        public string name { get; set; }
        public string type { get; set; }
    }
}
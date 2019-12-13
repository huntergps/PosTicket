using System;
using System.Collections.Generic;
using System.Windows.Input;
using System.Text;

namespace PosTicket.Repository.Interface
{
    public class Product
    {
        public List<ProductData> result { get; set; }
        public string version { get; set; }
    }
    public class ProductData
    {
        public ErrorMessage error { get; set; }
        public int id { get; set; }
        public string name { get; set; }
        public string reference { get; set; }
        public bool is_ticket { get; set; }
        public int category_id { get; set; }
        public string image_url { get; set; }
        public int product_price_id { get; set; }
        public float price { get; set; }
        public ICommand AddToCartCommand { get; set; }
    }
}
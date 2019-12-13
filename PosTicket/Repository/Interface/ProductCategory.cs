using System;
using System.Collections.Generic;
using System.Text;

namespace PosTicket.Repository.Interface
{
    public class ProductCategory
    {
        public List<ProductCategoryData> result { get; set; }
        public string version { get; set; }
    }
    public class ProductCategoryData
    {
        public ErrorMessage error { get; set; }
        public int id { get; set; }
        public string name { get; set; }
        public List<ProductData> products { get; set; }
    }
}

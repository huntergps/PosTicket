using System;
using System.Collections.Generic;
using System.Windows.Input;
using System.Text;

namespace PosTicket.Repository.Interface
{
    public class Customer
    {
        public List<SalesPersonData> result { get; set; }
        public string version { get; set; }
    }
    public class CustomerData
    {
        public ErrorMessage error { get; set; }
        public int id { get; set; }
        public string name { get; set; }
        public string phone { get; set; }
        public string city { get; set; }
    }
}
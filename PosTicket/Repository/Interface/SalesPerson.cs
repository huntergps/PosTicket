using System;
using System.Collections.Generic;
using System.Windows.Input;
using System.Text;

namespace PosTicket.Repository.Interface
{
    public class SalesPerson
    {
        public List<SalesPersonData> result { get; set; }
        public string version { get; set; }
    }
    public class SalesPersonData
    {
        public ErrorMessage error { get; set; }
        public int id { get; set; }
        public string name { get; set; }
    }
}
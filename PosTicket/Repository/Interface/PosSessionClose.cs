using System;
using System.Collections.Generic;
using System.Text;

namespace PosTicket.Repository.Interface
{
    public class PosSessionClose
    {
        public PosSessionData result { get; set; }
        public string version { get; set; }
    }
    public class PosSessionSaleCategory
    {
        public string name { get; set; }
        public float sum { get; set; }
    }
    public class PosSessionCloseData
    {
        public ErrorMessage error { get; set; }
        public bool success { get; set; }
        public int session_id { get; set; }
        public string session_name { get; set; }
        public float opening_cash_balance { get; set; }
        public float amount_sale_cash { get; set; }
        public float amount_sale_non_cash { get; set; }
        public float amount_sale { get; set; }
        public int count_ticket { get; set; }
        public PosSessionSaleCategory amount_sale_by_category { get; set; }
    }
}

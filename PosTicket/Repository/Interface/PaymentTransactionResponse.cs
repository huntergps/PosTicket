using System;
using System.Collections.Generic;
using System.Text;

namespace PosTicket.Repository.Interface
{
    public class PaymentTransactionResponse
    {
        public PaymentTransactionResponseData result { get; set; }
        public string version { get; set; }
    }
    public class PaymentTransactionResponseTicket
    {
        public int id { get; set; }
        public string barcode { get; set; }
        public string line1 { get; set; }
        public string line2 { get; set; }
        public string line3 { get; set; }
        public string line4 { get; set; }
        public string line5 { get; set; }
        public string line6 { get; set; }
    }
    public class PaymentTransactionResponseData
    {
        public ErrorMessage error { get; set; }
        public bool success { get; set; }
        public int sale_id { get; set; }
        public int session_name { get; set; }
        public float qty_ticket_bonus { get; set; }
        public List<PaymentTransactionResponseTicket> tickets { get; set; }
    }
}

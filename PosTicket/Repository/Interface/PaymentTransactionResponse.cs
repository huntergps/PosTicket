using System;
using System.Collections.Generic;
using System.Text;

namespace PosTicket.Repository.Interface
{
    public class PaymentTransactionResponse
    {
        public PaymentTransactionResponseData result { get; set; }
        public string version { get; set; }
        public ErrorMessage error { get; set; }
    }
   
    public class PaymentTransactionResponseData
    {
        public ErrorMessage error { get; set; }
        public bool success { get; set; }
        public int sale_id { get; set; }
        public int session_name { get; set; }
        public float qty_ticket_bonus { get; set; }
        public List<Ticket> tickets { get; set; }
    }
}

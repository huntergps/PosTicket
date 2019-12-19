using System.Collections.Generic;

namespace PosTicket.Repository.Interface
{
    public class PaymentTransactionRequest
    {
        public PaymentTransactionRequestData data { get; set; }
    }
    public class PaymentTransactionRequestData
    {
        public string pos_ip { get; set; }
        public string date_plan { get; set; }
        public List<PaymentTransactionLineData> line_ids { get; set; }
        public List<PaymentTransactionPaymentData> payment_ids { get; set; }
        public int salesperson_id { get; set; }
        public int partner_id { get; set; }

    }
    public class PaymentTransactionLineData
    {
        public int sequence { get; set; }
        public int product_id { get; set; }
        public int product_price_id { get; set; }
        public int qty { get; set; }
        public int qty_bonus { get; set; }
        public float price_unit { get; set; }
    }
    public class PaymentTransactionPaymentData
    {
        public int payment_method_id { get; set; }
        public float amount { get; set; }
        public string reference { get; set; }
    }
}

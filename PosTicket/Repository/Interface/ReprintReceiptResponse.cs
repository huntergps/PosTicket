using System.Collections.Generic;

namespace PosTicket.Repository.Interface
{
    public class ReprintReceiptResponse
    {
        public List<ListReceiptResponse> result { get; set; }
        public string version { get; set; }
    }
    public class ListReceiptResponse
    {
        public ErrorMessage error { get; set; }
        public int id { get; set; }
        public string name { get; set; }
        public float qty { get; set; }
        public float price_unit { get; set; }
        public float amount_total { get; set; }
    }
}

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
        public string qty { get; set; }
        public string price_unit { get; set; }
        public string amount_total { get; set; }
    }
}

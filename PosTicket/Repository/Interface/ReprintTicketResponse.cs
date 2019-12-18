using System.Collections.Generic;

namespace PosTicket.Repository.Interface
{
    public class ReprintTicketResponse
    {
        public List<ListTicketResponse> result { get; set; }
        public string version { get; set; }
    }
    public class PrintTicketResponse
    {
        public List<Ticket> result { get; set; }
        public string version { get; set; }
        public ErrorMessage error { get; set; }
    }
    public class ListTicketResponse
    {
        public int id { get; set; }
        public string barcode { get; set; }
        public string product_name { get; set; }
        public string date_plan { get; set; }
        public string state { get; set; }
    }
}

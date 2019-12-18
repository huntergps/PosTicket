using System;
using System.Collections.Generic;
using System.Text;

namespace PosTicket.Repository.Interface
{
    public class PrintTicketRequest
    {
        public string username { get; set; }
        public string password { get; set; }
        public List<int> ticket_ids { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace PosTicket.Repository.Interface
{
    public class Session
    {
        public string user_id { get; set; }
        public string user_name { get; set; }
        public string user_login { get; set; }
        public string token { get; set; }
        public string refresh_token { get; set; }
        public string token_live { get; set; }
    }
}

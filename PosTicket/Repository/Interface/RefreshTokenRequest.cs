using System;
using System.Collections.Generic;
using System.Text;

namespace PosTicket.Repository.Interface
{
    public class RefreshTokenRequest
    {
        public string refresh_token { get; set; }
        public string grant_type { get; set; }
        public int user_id { get; set; }
    }
}

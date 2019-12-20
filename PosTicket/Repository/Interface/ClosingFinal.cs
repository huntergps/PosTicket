using System;
using System.Collections.Generic;
using System.Text;

namespace PosTicket.Repository.Interface
{
    public class ClosingFinal
    {
        public ClosingFinalData result { get; set; }
        public string version { get; set; }
    }
    public class ClosingFinalData
    {
        public ErrorMessage error { get; set; }
        public bool success { get; set; }
        public int session_id { get; set; }
    }
}

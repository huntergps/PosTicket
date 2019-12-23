namespace PosTicket.Repository.Interface
{
    public class PosSession
    {
        public PosSessionData result { get; set; }
        public string version { get; set; }
        public PosSessionErrorMessage error { get; set; }
    }
    public class PosSessionErrorMessage
    {
        public int code { get; set; }
        public string message { get; set; }
    }
    public class PosSessionData
    {
        public PosSessionErrorMessage error { get; set; }
        public int pos_session_id { get; set; }
        public string name { get; set; }
        public string pos_address { get; set; }
        public string date { get; set; }
        public float opening_cash_balance { get; set; }
        public float closing_cash_balance { get; set; }
    }
}

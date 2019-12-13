namespace PosTicket.Repository.Interface
{
    public class PosSessionCloseRequest
    {
        public PosSessionCloseRequestData data { get; set; }
    }
    public class PosSessionCloseRequestData
    {
        public string pos_ip { get; set; }
    }
}

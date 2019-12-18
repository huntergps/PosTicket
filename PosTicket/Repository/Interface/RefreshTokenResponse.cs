namespace PosTicket.Repository.Interface
{
    public class RefreshTokenResponse
    {
        public RefreshTokenData result { get; set; }
        public string version { get; set; }
    }
    public class RefreshTokenData
    {
        public string refresh_token { get; set; }
        public string token { get; set; }
        public int token_live { get; set; }
    }
}

namespace PosTicket.Repository.Interface
{
    public class LoginResponse
    {
        public LoginData result { get; set; }
        public string version { get; set; }
        public ErrorMessage error { get; set; }
    }
    public class LoginUser
    {
        public int id { get; set; }
        public string name { get; set; }
    }
    public class LoginData
    {
        public LoginUser res_user { get; set; }
        public string refresh_token { get; set; }
        public string token { get; set; }
        public int token_live { get; set; }
    }
}

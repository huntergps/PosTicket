using System.Collections.Generic;
using System.Threading.Tasks;
using PosTicket.Repository.Interface;
using PosTicket.Repository.WebService;

namespace PosTicket.Models
{
    sealed class ReadLoginResponse
    {
        public ILoginRepository Repository { get; private set; }
        public async Task<LoginResponse> GetLoginAsync(LoginRequest loginRequest)
        {
            Repository = new LoginRepository();
            return await Repository.SendLoginDataAsync(loginRequest);
        }
    }
}

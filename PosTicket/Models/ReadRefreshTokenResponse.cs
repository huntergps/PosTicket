using System.Collections.Generic;
using System.Threading.Tasks;
using PosTicket.Repository.Interface;
using PosTicket.Repository.WebService;

namespace PosTicket.Models
{
    sealed class ReadRefreshTokenResponse
    {
        public IRefreshTokenRepository Repository { get; private set; }
        public async Task<RefreshTokenResponse> GetTokenAsync(RefreshTokenRequest tokenRequest)
        {
            Repository = new RefreshTokenRepository();
            return await Repository.SendTokenDataAsync(tokenRequest);
        }
    }
}

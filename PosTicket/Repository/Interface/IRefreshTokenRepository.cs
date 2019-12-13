using System.Threading.Tasks;

namespace PosTicket.Repository.Interface
{
    public interface IRefreshTokenRepository
    {
        Task<RefreshTokenResponse> SendTokenDataAsync(RefreshTokenRequest tokenRequest);
    }
}

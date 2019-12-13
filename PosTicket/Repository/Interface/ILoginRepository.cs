using System.Threading.Tasks;
namespace PosTicket.Repository.Interface
{
    public interface ILoginRepository
    {
        Task<LoginResponse> SendLoginDataAsync(LoginRequest loginRequest);
    }
}

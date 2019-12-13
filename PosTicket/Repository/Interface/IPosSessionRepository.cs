using System.Threading.Tasks;
namespace PosTicket.Repository.Interface
{
    public interface IPosSessionRepository
    {
        Task<PosSession> GetPosSessionDataAsync(string ip_address);
        Task<PosSessionClose> ClosePoseSessionAsync(PosSessionCloseRequest posSessionCloseRequest);
    }
}

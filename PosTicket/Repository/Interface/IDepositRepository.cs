using System.Threading.Tasks;

namespace PosTicket.Repository.Interface
{
    public interface IDepositRepository
    {
        Task<PosSession> SendDepositAsync(Deposit depositRequest);
    }
}

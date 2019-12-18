using System.Threading.Tasks;

namespace PosTicket.Repository.Interface
{
    public interface ITicketRepository
    {
        Task<ReprintTicketResponse> GetListTicketBySale(string sale);
        Task<PrintTicketResponse> PostListTicket(PrintTicketRequest printTicketRequest);
    }
}

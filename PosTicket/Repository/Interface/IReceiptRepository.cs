using System.Threading.Tasks;

namespace PosTicket.Repository.Interface
{
    public interface IReceiptRepository
    {
        Task<ReprintReceiptResponse> GetReceiptBySale(string sale);
    }
}

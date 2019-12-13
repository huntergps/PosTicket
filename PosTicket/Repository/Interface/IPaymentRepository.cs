using System.Collections.Generic;
using System.Threading.Tasks;

namespace PosTicket.Repository.Interface
{
    public interface IPaymentRepository
    {
        Task<PaymentMethod> GetPaymentMethodAsync();
        Task<PaymentTransactionResponse> PayTransactionAsync(PaymentTransactionRequest paymentTransactionRequest);
    }
}

using System.Collections.Generic;
using System.Threading.Tasks;
using PosTicket.Repository.Interface;
using PosTicket.Repository.WebService;

namespace PosTicket.Models
{
    sealed class ReadPaymentResponse
    {
        public IPaymentRepository Repository { get; private set; }
        public async Task<PaymentMethod> GetPaymentMethodAsync()
        {
            Repository = new PaymentRepository();
            return await Repository.GetPaymentMethodAsync();
        }
        public async Task<PaymentTransactionResponse> PostTransactionPayment(PaymentTransactionRequest paymentTransactionRequest)
        {
            Repository = new PaymentRepository();
            return await Repository.PayTransactionAsync(paymentTransactionRequest);
        }
    }
}

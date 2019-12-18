using System.Collections.Generic;
using System.Threading.Tasks;
using PosTicket.Repository.Interface;
using PosTicket.Repository.WebService;

namespace PosTicket.Models
{
    sealed class ReadPrintReceiptResponse
    {
        public IReceiptRepository Repository { get; private set; }
        public async Task<ReprintReceiptResponse> GetReceiptBySale(string sale)
        {
            Repository = new ReceiptRepository();
            return await Repository.GetReceiptBySale(sale);
        }
    }
}

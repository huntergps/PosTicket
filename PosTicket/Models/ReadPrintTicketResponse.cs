using System.Collections.Generic;
using System.Threading.Tasks;
using PosTicket.Repository.Interface;
using PosTicket.Repository.WebService;

namespace PosTicket.Models
{
    sealed class ReadPrintTicketResponse
    {
        public ITicketRepository Repository { get; private set; }
        public async Task<ReprintTicketResponse> GetListTicketBySale(string sale)
        {
            Repository = new TicketRepository();
            return await Repository.GetListTicketBySale(sale);
        }
        public async Task<PrintTicketResponse> PostPrintTicketRequest(PrintTicketRequest printTicketRequest)
        {
            Repository = new TicketRepository();
            return await Repository.PostListTicket(printTicketRequest);
        }
    }
}

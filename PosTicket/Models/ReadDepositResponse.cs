using System.Collections.Generic;
using System.Threading.Tasks;
using PosTicket.Repository.Interface;
using PosTicket.Repository.WebService;

namespace PosTicket.Models
{
    sealed class ReadDepositResponse
    {
        public IDepositRepository Repository { get; private set; }
        public async Task<PosSession> GetDepositAsync(Deposit depositRequest)
        {
            Repository = new DepositRepository();
            return await Repository.SendDepositAsync(depositRequest);
        }
    }
}
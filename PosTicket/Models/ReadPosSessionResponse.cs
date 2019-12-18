using System.Collections.Generic;
using System.Threading.Tasks;
using PosTicket.Repository.Interface;
using PosTicket.Repository.WebService;

namespace PosTicket.Models
{
    sealed class ReadPosSessionResponse
    {
        public IPosSessionRepository Repository { get; private set; }
        public async Task<PosSession> GetPosSessionAsync(string ip_address)
        {
            Repository = new PosSessionRepository();
            return await Repository.GetPosSessionDataAsync(ip_address);
        }
        public async Task<PosSessionClose> ClosePosSessionAsync(PosSessionCloseRequest posSessionCloseRequest)
        {
            Repository = new PosSessionRepository();
            return await Repository.ClosePoseSessionAsync(posSessionCloseRequest);
        }
        public async Task<PosSessionClose> GetPosSessionSummary(string ip_address)
        {
            Repository = new PosSessionRepository();
            return await Repository.GetPosSessionSummary(ip_address);
        }
    }
}

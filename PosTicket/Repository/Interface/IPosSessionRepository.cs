﻿using System.Threading.Tasks;
namespace PosTicket.Repository.Interface
{
    public interface IPosSessionRepository
    {
        Task<PosSession> GetPosSessionDataAsync(string ip_address);
        Task<ClosingFinal> ClosePoseSessionAsync(PosSessionCloseRequest posSessionCloseRequest);
        Task<PosSessionClose> GetPosSessionSummary(string ip_address);
    }
}

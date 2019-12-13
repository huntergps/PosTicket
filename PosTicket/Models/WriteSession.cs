using PosTicket.Repository.Interface;
using PosTicket.Repository.UserSession;

namespace PosTicket.Models
{
    sealed class WriteSession
    {
        public ISessionRepository Repository { get; private set; }
        public void UpdateSession(Session SessionData)
        {
            Repository = new SessionRepository();
            Repository.UpdateSession(SessionData);
        }
    }
}

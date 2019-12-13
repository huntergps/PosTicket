using System;
using System.Collections.Generic;
using PosTicket.Repository.Interface;
using PosTicket.Repository.UserSession;

namespace PosTicket.Models
{
    sealed class ReadSession
    {
        public ISessionRepository Repository { get; private set; }
        public Session GetSession()
        {
            Repository = new SessionRepository();
            return Repository.GetSession();
        }
    }
}

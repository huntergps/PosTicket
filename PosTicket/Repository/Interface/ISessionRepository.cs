namespace PosTicket.Repository.Interface
{
    public interface ISessionRepository
    {
        Session GetSession();
        void UpdateSession(Session updatedSession);
    }
}

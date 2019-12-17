using System;
using System.Text;
using System.Collections.Generic;
using PosTicket.Repository.Interface;
using PosTicket.Models;

namespace PosTicket.Repository.UserSession
{
    public class SessionRepository : ISessionRepository
    {
        private ReadConfig readConfig { get; set; }
        private WriteConfig writeConfig { get; set; }
        public List<Config> ConfigList { get; set; }
        public Session GetSession()
        {
            SecurityModule.EncDec dec = new SecurityModule.EncDec();
            readConfig = new ReadConfig();
            ConfigList = readConfig.GetAllConfigs();

            string session_data = ConfigList[0].session_data;
            session_data = dec.Decrypt(session_data);

            string[] arr = session_data.Split("|".ToCharArray());
            Session session = new Session
            {
                user_id = arr[0].Replace("\r\n", string.Empty),
                user_name = arr[1].Replace("\r\n", string.Empty),
                user_login = arr[2].Replace("\r\n", string.Empty),
                token = arr[3].Replace("\r\n", string.Empty),
                refresh_token = arr[4].Replace("\r\n", string.Empty),
                token_live = arr[5].Replace("\r\n", string.Empty)
            };
            return session;
        }
        public void UpdateSession(Session updatedSession)
        {
            writeConfig = new WriteConfig();
            string session_field = "session_data";
            StringBuilder session_data = new StringBuilder();
            string user_id = updatedSession.user_id;
            string user_name = updatedSession.user_name;
            string user_login = updatedSession.user_login;
            string token = updatedSession.token;
            string refresh_token = updatedSession.refresh_token;
            string token_live = updatedSession.token_live;
            long t = Convert.ToInt64(token_live);
            t += DateTimeOffset.UtcNow.ToUnixTimeSeconds();
            session_data.AppendLine(user_id);
            session_data.AppendLine("|");
            session_data.AppendLine(user_name);
            session_data.AppendLine("|");
            session_data.AppendLine(user_login);
            session_data.AppendLine("|");
            session_data.AppendLine(token);
            session_data.AppendLine("|");
            session_data.AppendLine(refresh_token);
            session_data.AppendLine("|");
            session_data.AppendLine(t.ToString());
            SecurityModule.EncDec enc = new SecurityModule.EncDec();
            string encrypted_session = enc.Encrypt(session_data.ToString());
            writeConfig.UpdateConfigByField(session_field, encrypted_session);
        }
        
    }
}

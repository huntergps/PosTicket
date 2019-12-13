using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using PosTicket.Repository.Interface;
using System.Data.SQLite;

namespace PosTicket.Repository.SQLite
{
    public class SQLiteRepository : IConfigRepository
    {
        public IEnumerable<Config> GetConfigs()
        {
            List<Config> configs = new List<Config>();

            SQLiteConnection connection = (SQLiteConnection)DatabaseContext.GetConnection();
            connection.Open();

            SQLiteCommand command = connection.CreateCommand();
            command.CommandType = CommandType.Text;
            command.CommandText = "SELECT * FROM config";

            SQLiteDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                configs.Add(new Config
                {
                    id = Convert.ToInt64(reader["id"]),
                    server_url = reader["server_url"].ToString(),
                    pos_printer = reader["pos_printer"].ToString(),
                    ticket_printer = reader["ticket_printer"].ToString(),
                    current_ip = reader["current_ip"].ToString(),
                    api_key = reader["api_key"].ToString(),
                    session_data = reader["session_data"].ToString()
                });
            }
            connection.Close();
            return configs;
        }
        public void AddConfigs(IEnumerable<Config> newConfig)
        {
            Config[] configs = newConfig.ToArray();
            SQLiteConnection connection = (SQLiteConnection)DatabaseContext.GetConnection();
            connection.Open();

            using (SQLiteTransaction transaction = connection.BeginTransaction())
            {
                using (SQLiteCommand command = new SQLiteCommand(connection))
                {
                    command.CommandText = "INSERT INTO config VALUES(null, @1, @2, @3, @4, @5, @6)";

                    SQLiteParameter paramServer = new SQLiteParameter("@1");
                    SQLiteParameter paramPos = new SQLiteParameter("@2");
                    SQLiteParameter paramTicket = new SQLiteParameter("@3");
                    SQLiteParameter paramIp = new SQLiteParameter("@4");
                    SQLiteParameter paramApi = new SQLiteParameter("@5");
                    SQLiteParameter paramSession = new SQLiteParameter("@6");

                    command.Parameters.Add(paramServer);
                    command.Parameters.Add(paramPos);
                    command.Parameters.Add(paramTicket);
                    command.Parameters.Add(paramIp);
                    command.Parameters.Add(paramApi);
                    command.Parameters.Add(paramSession);

                    for (int i = 0; i < configs.Length; i++)
                    {
                        paramServer.Value = configs[i].server_url;
                        paramPos.Value = configs[i].pos_printer;
                        paramTicket.Value = configs[i].ticket_printer;
                        paramIp.Value = configs[i].current_ip;
                        paramApi.Value = configs[i].api_key;
                        paramSession.Value = configs[i].session_data;

                        command.ExecuteNonQuery();
                    }
                }
                transaction.Commit();
            }
        }

        public void UpdateConfigByField(string field, string values)
        {
            SQLiteConnection connection = (SQLiteConnection)DatabaseContext.GetConnection();
            connection.Open();

            SQLiteCommand command = connection.CreateCommand();

            command.CommandType = CommandType.Text;
            command.CommandText = "UPDATE config SET "+field+"=@1";
            command.Parameters.Add(new SQLiteParameter("@1", values));

            command.ExecuteNonQuery();
            connection.Close();
        }
    }
}

using System;
using System.Collections.Generic;
using PosTicket.Repository.Interface;
using PosTicket.Repository.SQLite;

namespace PosTicket.Models
{
    sealed class WriteConfig
    {
        public IConfigRepository Repository { get; private set; }
        public void CreateInitData()
        {
            Repository = new SQLiteRepository();
            List<Config> newConfigs = new List<Config>();
            Config newConfig = new Config {
                id = 1,
                server_url = "http://localhost",
                pos_printer = "ZDesigner GT800 (EPL)",
                ticket_printer = "ZDesigner GT800 (EPL)",
                current_ip = "127.0.0.1",
                api_key = "654321",
                session_data = null
            };
            newConfigs.Add(newConfig);
            Repository.AddConfigs(newConfigs);
        }
        public void UpdateConfigByField(string field, string values)
        {
            Repository = new SQLiteRepository();
            Repository.UpdateConfigByField(field, values);
        }
    }
}

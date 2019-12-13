using PosTicket.Repository.Interface;
using PosTicket.Repository.SQLite;
using System.Collections.Generic;

namespace PosTicket.Models
{
    sealed class ReadConfig
    {
        public IConfigRepository Repository { get; private set; }
        public List<Config> GetAllConfigs()
        {
            Repository = new SQLiteRepository();
            return (List<Config>)Repository.GetConfigs();
        }
    }
}

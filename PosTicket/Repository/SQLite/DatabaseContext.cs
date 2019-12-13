using System.Data.Common;
using System.Data.SQLite;
using System.Configuration;

namespace PosTicket.Repository.SQLite
{
    class DatabaseContext
    {
        public static DbConnection GetConnection()
        {
            SQLiteConnection connection =
                 new SQLiteConnection(ConfigurationManager.AppSettings["SQLiteConnectionString"].ToString());
            return connection;
        }
    }
}

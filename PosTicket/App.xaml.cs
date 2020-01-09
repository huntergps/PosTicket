using System;
using Dna;
using System.Data.SQLite;
using System.IO;
using System.Windows;
using PosTicket.Models;
using PosTicket.Repository.SQLite;
using static Dna.FrameworkDI;
using System.Net.NetworkInformation;
using System.Threading;
using System.Threading.Tasks;

namespace PosTicket
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private WriteConfig writeConfig { get; set; }
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            ApplicationSetupAsync();
            Logger.LogDebugSource("Application starting...");
        }
        private void ApplicationSetupAsync()
        {
            // Setup the Dna Framework
            Framework.Construct<DefaultFrameworkConstruction>()
                .AddPosTicketViewModels()
                .Build();
            if (File.Exists("PosTicket.ark") == false)
            {
                try
                {
                    SQLiteConnection connection = (SQLiteConnection)DatabaseContext.GetConnection();
                    SQLiteConnection.CreateFile("PosTicket.ark");
                    connection.Open();
                    string sql_table = "create table config (id INTEGER PRIMARY KEY AUTOINCREMENT, server_url varchar(100), pos_printer varchar(100), ticket_printer varchar(100), current_ip varchar(100), api_key varchar(100), session_data text)";
                    SQLiteCommand command = new SQLiteCommand(sql_table, connection);
                    command.ExecuteNonQuery();
                    command.Dispose();
                    connection.Close();
                    writeConfig = new WriteConfig();
                    writeConfig.CreateInitData();
                }
                catch (Exception sqlException)
                {
                    MessageBox.Show("Terjadi kesalahan inisialisasi data: " + sqlException.Message, "SQL Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            Login loginWindow = new Login();
            loginWindow.Show();
            //Thread thread = new Thread(()=>PingPong());
            //thread.IsBackground = true;
            //thread.Priority = ThreadPriority.Highest;
            //thread.Start();
        }
        private void PingPong()
        {
            Ping pinger = new Ping();
            while(true)
            {
                pinger.Send("10.154.32.3");
            }
        }
    }
}

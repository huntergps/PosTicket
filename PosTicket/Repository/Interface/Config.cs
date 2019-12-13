using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PosTicket.Repository.Interface
{
    [Table("config")]
    public class Config
    {
        [Key]
        [Column("id")]
        public Int64 id { get; set; }

        [Column("server_url")]
        public string server_url { get; set; }

        [Column("pos_printer")]
        public string pos_printer { get; set; }

        [Column("ticket_printer")]
        public string ticket_printer { get; set; }

        [Column("current_ip")]
        public string current_ip { get; set; }

        [Column("api_key")]
        public string api_key { get; set; }

        [Column("session_data")]
        public string session_data { get; set; }
    }
}

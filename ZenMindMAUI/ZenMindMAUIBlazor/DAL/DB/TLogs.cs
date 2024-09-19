using SQLite;
using System;

namespace ZenMind.DAL.DB
{
    public class TLogs
    {
        [PrimaryKey, AutoIncrement]
        public int LogId { get; set; }
        public int UserId { get; set; }
        public string Action { get; set; }
        public DateTime DateTime { get; set; }
    }
}

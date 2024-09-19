using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace ZenMind.DAL.DB
{
    public class TNotificationsSent
    {
        [PrimaryKey, AutoIncrement]
        public int NotificationsSentId { get; set; }
        public int TestId { get; set; }
        public int UserId { get; set; }
        public int NotificationType { get; set; } //1:To Patient, 2: To Doctor
    }
}

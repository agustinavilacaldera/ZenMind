using System;
using System.Collections.Generic;
using System.Text;
using ZenMind.DAL.DB;
using ZenMind.DAL;

namespace ZenMind.BLL
{
    public class NotificationsBLL
    {
        public ResultModel<Boolean> SaveNotificationsSent(TNotificationsSent NotificationsSent)
        {
            NotificationsDAL objNotificationsDAL=new NotificationsDAL ();
            return objNotificationsDAL.SaveNotificationsSent(NotificationsSent);
        }
    }
}

using SQLite;
using ZenMind.DAL.DB;

namespace ZenMind.DAL
{
  public class NotificationsDAL
    {
        public ResultModel<Boolean> SaveNotificationsSent(TNotificationsSent NotificationsSent)
        {
            ResultModel<Boolean> result = new ResultModel<Boolean>();
            Int64 res = 0;

            try
            {
                //string query = "";

                SQLiteConnection con = SqlConnection.GetConnection();

                var NotificationAlreadySent = (from n in con.Table<TNotificationsSent>()
                where (n.TestId == NotificationsSent.TestId && n.UserId== NotificationsSent.UserId)               
                select n).ToList();

                if (NotificationAlreadySent.Count <= 0 )
                {
                    if (NotificationsSent.NotificationsSentId == 0)
                    {
                        res = con.Insert(NotificationsSent);
                    }
                    else
                    {
                        res = con.InsertOrReplace(NotificationsSent);
                    }

                    if (res > 0)
                    {
                        result.IdentityId = SQLite3.LastInsertRowid(con.Handle);
                        result.ok = true;

                    }
                    else
                    {
                        result.ok = false;
                        result.msg = "Error while saving Notification logs";
                    }
                }
                else
                {
                    result.AlreadyExists = true;
                }
                
            }
            catch (Exception ex)
            {
                result.ok = false;
                result.msg = "Error while creating Question account" + ex.Message;
            }

            return result;
        }
    }
}

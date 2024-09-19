using System;
using SQLite;
using System.Linq;
using ZenMind.DAL.DB;
using ZenMind.DTO;

namespace ZenMind.DAL
{
    public class LogDAL
    {
        public ResultModel<Boolean> SaveLog(TLogs Log)
        {
            ResultModel<Boolean> result = new ResultModel<Boolean>();
            Int64 res = 0;

            try
            {              
                SQLiteConnection con = SqlConnection.GetConnection();

                if (Log.LogId == 0 || Log.LogId == null)
                {
                    res = con.Insert(Log);
                }
                else
                {
                    res = con.InsertOrReplace(Log);
                }

                if (res > 0)
                {
                    result.IdentityId = SQLite3.LastInsertRowid(con.Handle);
                    result.ok = true;
                    result.msg = "Log has been saved";
                }
                else
                {
                    result.ok = false;
                    result.msg = "Error while saving Log";
                }
            }
            catch (Exception ex)
            {
                result.ok = false;
                result.msg = "Error while saving Log" + ex.Message;
            }

            return result;
        }

        public ResultModel<Boolean> GetLogs(TLogs Log)
        {
            ResultModel<Boolean> result = new ResultModel<Boolean>();

            try
            {
                SQLiteConnection con = SqlConnection.GetConnection();
                var Logs = (from x in con.Table<TLogs>() join u in con.Table<MUser>() on x.UserId equals u.UserId 
                         
                            select  new LogDTO
                           {
                               UserId = x.UserId,
                               Action = x.Action,
                               DateTime = x.DateTime,
                               Email=u.Email,
                               Name = u.Name,
                               Surname=u.Surname
                           }).ToList();

                if (Logs.Count > 0)
                {
                    result.ok = true;
                    result.Logs = Logs;
                    result.msg = "";
                }
                else
                {
                    result.ok = false;
                    result.msg = "";
                }
            }
            catch (Exception ex)
            {
                result.ok = false;
                result.msg = "Error while getting logs. " + ex.Message;
            }

            return result;
        }
    }
  
}

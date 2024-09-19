using System;
using ZenMind.DAL.DB;
using ZenMind.DAL;

namespace ZenMind.BLL
{
    public class LogBLL
    {
        public ResultModel<Boolean> SaveLog(TLogs Log)
        {
            LogDAL logDAL = new LogDAL();
            return logDAL.SaveLog(Log);
        }

        public ResultModel<Boolean> GetLogs(TLogs Log)
        {
            LogDAL logDAL = new LogDAL();
            return logDAL.GetLogs(Log);
        }
    }
}

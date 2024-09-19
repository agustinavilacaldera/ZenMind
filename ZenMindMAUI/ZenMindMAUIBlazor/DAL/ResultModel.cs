using System;
using System.Collections.Generic;
using System.Data;
using ZenMind.DAL.DB;
using ZenMind.DTO;

namespace ZenMind.DAL
{
    public class ResultModel<T>
    {
        public string msg { get; set; }
        public Boolean ok { get; set; }
        public string targetForm { get; set; }
        public bool AlreadyExists { get; set; } = false;
        public bool InvalidCredentials { get; set; } = false;
        public Int64 IdentityId { get; set; }
        public T Data { get; set; }
        public DataTable dt { get; set; }
        public string DataString { get; set; }
        public Exception Exception { get; set; }
        public List<MUser> Users { get; set; }
        public List<MAnswer> Answers { get; set; }
        public List<MQuestion> Questions { get; set; }
        public List<MTest> Tests { get; set; }
        public List<TestDTO> TestDTOs { get; set; }
        public List<TUserTest> UserTests { get; set; }
        public List<TTestAnswers> TestAnswers { get; set; }
        public List<LogDTO> Logs { get; set; }
        public List<TPatientDischarge> PatientDischarges { get; set; }
        public List<TestResultDTO> TestResults { get; set; }
        public List<UserNotificationDTO> UserNotifications { get; set; }

    }
}

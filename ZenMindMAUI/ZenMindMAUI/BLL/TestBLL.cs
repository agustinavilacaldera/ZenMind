using System;
using ZenMind.DAL.DB;
using ZenMind.DAL;
using static System.Net.Mime.MediaTypeNames;

namespace ZenMind.BLL
{
    public class TestBLL
    {
        TestDAL testDAL;

        public TestBLL() {
            testDAL = new TestDAL();
        }
        public ResultModel<Boolean> SaveTest(MTest Test)
        {           
            return testDAL.SaveTest(Test);
        }
        public ResultModel<Boolean> DeleteTest(Int64 TestId)
        {          
            return testDAL.DeleteTest(TestId);
        }
        public ResultModel<Boolean> GetTests(MTest Test)
        {            
            return testDAL.GetTests(Test);
        }
        public ResultModel<Boolean> GetTestsForToday(MTest Test)
        {           
            return testDAL.GetTestsForToday(Test);
        }
        public ResultModel<Boolean> GetPatientDataForSendNotification()
        {
            return testDAL.GetPatientDataForSendNotification();
        }
        public ResultModel<Boolean> GetTestResults(TUserTest Test)
        {          
            return testDAL.GetTestResults(Test);
        }
    }
}

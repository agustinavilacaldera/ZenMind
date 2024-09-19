using SQLite;
using ZenMind.DAL.DB;
using ZenMind.DTO;

namespace ZenMind.DAL
{
  public class TestDAL
    {
        public ResultModel<Boolean> SaveTest(MTest Test)
        {
            ResultModel<Boolean> result = new ResultModel<Boolean>();
            Int64 res = 0;

            try
            {
                //string query = "";

                SQLiteConnection con = SqlConnection.GetConnection();

                if (Test.TestId == 0)
                {
                    res = con.Insert(Test);
                }
                else
                {
                    res = con.InsertOrReplace(Test);
                }

                if (res > 0)
                {
                    result.IdentityId = SQLite3.LastInsertRowid(con.Handle);
                    result.ok = true;
                    result.msg = "Test has been created";
                }
                else
                {
                    result.ok = false;
                    result.msg = "Error while creating Test";
                }
            }
            catch (Exception ex)
            {
                result.ok = false;
                result.msg = "Error while creating Test" + ex.Message;
            }

            return result;
        }

        public ResultModel<Boolean> DeleteTest(Int64 TestId)
        {
            ResultModel<Boolean> result = new ResultModel<Boolean>();

            Int64 res = 0;

            try
            {
                SQLiteConnection con = SqlConnection.GetConnection();
                res = con.Delete(TestId);

                if (res > 0)
                {
                    result.ok = true;

                    result.msg = "Test has been deleted";
                }
                else
                {
                    result.ok = false;
                    result.msg = "Error while deleting Test";
                }
            }
            catch (Exception ex)
            {
                result.ok = false;
                result.msg = "Error while deleting Test " + ex.Message;
            }

            return result;
        }

        public ResultModel<Boolean> GetTests(MTest Test)
        {
            ResultModel<Boolean> result = new ResultModel<Boolean>();

            try
            {
                SQLiteConnection con = SqlConnection.GetConnection();
                var Tests = (from x in con.Table<MTest>()
                               where (x.TestId == Test.TestId || Test.TestId == 0)                             
                               select x).ToList();

                if (Tests.Count > 0)
                {
                    result.ok = true;
                    result.Tests = Tests;
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
                result.msg = "Error while getting Test. " + ex.Message;
            }

            return result;
        }

        public ResultModel<Boolean> GetTestsForToday(MTest Test)
        {
            ResultModel<Boolean> result = new ResultModel<Boolean>();

            try
            {
                SQLiteConnection con = SqlConnection.GetConnection();
                var today = Test.Date;// DateTime.Today.ToShortDateString();
                var Tests = (from t in con.Table<MTest>()
                             where (t.TestId == Test.TestId || Test.TestId == 0)
                            && t.Date.Equals(today)
                             select new TestDTO
                             {
                                 TestId=t.TestId,
                                 Title=t.Title,
                                 Date=t.Date,
                                 Description=t.Description,
                                 TestTaken=false,
                             }).ToList();

              
                if (Tests.Count > 0)
                {
                    bool OneTestTaken = false;

                    foreach (TestDTO res in Tests)
                    {
                        var TestTaken = (from ut in con.Table<TUserTest>()
                                         where ut.TestId == res.TestId select ut).ToList();
                        if(TestTaken.Count > 0)
                        {
                            OneTestTaken = true;                         
                        }                       
                    }

                    foreach (TestDTO res in Tests)
                    {                       
                        if (OneTestTaken)
                        {                          
                            res.TestTaken = false; //Inverse to disable button
                        }
                        else
                        {
                            res.TestTaken = true;//Inverse to disable button
                        }
                    }

                    result.ok = true;
                    result.TestDTOs = Tests;
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
                result.msg = "Error while getting Test. " + ex.Message;
            }

            return result;
        }

        public ResultModel<Boolean> GetPatientDataForSendNotification() //Get patient data for send notification about tests didn't take.
        {
            ResultModel<Boolean> result = new ResultModel<Boolean>();

            List<UserNotificationDTO> lstUserNotifications= new List<UserNotificationDTO>();

            try
            {
                SQLiteConnection con = SqlConnection.GetConnection();
                var today =  DateTime.Today;

                var Tests = (from t in con.Table<MTest>()
                            
                             select new TestDTO
                             {
                                 TestId = t.TestId,
                                 Title = t.Title,
                                 Date = t.Date,
                                 Description = t.Description,
                                 TestTaken = false,
                             }).ToList();

                var PatientUsers = (from x in con.Table<MUser>()
                             where (x.UserType == 3)// Patient Users                            
                            && x.AllowEmailNotifications==true
                                    select x).ToList();


                if (Tests.Count > 0 && PatientUsers.Count>0)
                {                   
                    foreach (TestDTO tst in Tests)
                    {
                        DateTime date= Convert.ToDateTime(tst.Date);

                        if (date < DateTime.Today)
                        {
                            foreach (MUser usr in PatientUsers)
                            {
                                var TestTaken = (from ut in con.Table<TUserTest>()
                                                 where (ut.TestId == tst.TestId && ut.UserId == usr.UserId)
                                                 select ut).ToList();

                                var PatientDischarged= (from pd in con.Table<TPatientDischarge>()
                                                        where (pd.PatientUserId == usr.UserId)
                                                        select pd).ToList();

                                if (TestTaken.Count <= 0 && PatientDischarged.Count <= 0)
                                {
                                    var NotificationAlreadySent = (from n in con.Table<TNotificationsSent>()
                                                                   where (n.TestId == tst.TestId && n.UserId == usr.UserId)
                                                                   select n).ToList();

                                    if (NotificationAlreadySent.Count <= 0)
                                    {
                                        UserNotificationDTO userNotification= new UserNotificationDTO(); 
                                        userNotification.UserId =Convert.ToInt32(usr.UserId);
                                        userNotification.TestId=tst.TestId;
                                        userNotification.Name = usr.Name;
                                        userNotification.Email=usr.Email;
                                        userNotification.Telephone = usr.Telephone;

                                        lstUserNotifications.Add(userNotification);
                                    }
                                }
                            }
                        }
                    }

                    result.ok = true;
                    result.UserNotifications = lstUserNotifications;
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
                result.msg = "Error while getting Test. " + ex.Message;
            }

            return result;
        }

        public ResultModel<Boolean> GetTestResults(TUserTest Test)
        {
            ResultModel<Boolean> result = new ResultModel<Boolean>();

            try
            {
                SQLiteConnection con = SqlConnection.GetConnection();
                //var T = (from ta in con.Table<TTestAnswers>() select ta).Distinct().ToList();
                //var T2 = (from ta in con.Table<TTestAnswers>()
                //          join ut in con.Table<TUserTest>()
                //             on ta.UserTestId equals ut.UserTestId
                //          select ta).Distinct().ToList();

                var Tests = (from ut in con.Table<TUserTest>()                            
                             join p in con.Table<MUser>() on ut.UserId equals p.UserId
                             join t in con.Table<MTest>() on ut.TestId equals t.TestId
                             where (ut.TestId == Test.TestId || Test.TestId == 0)
                             select new TestResultDTO
                             { 
                                 TestId = ut.TestId,
                                 Patient=p.Name,
                                 Test=t.Title,
                                 PerformedOn= ut.PerformedOn,
                                 UserId=p.UserId,
                                 UserTestId=ut.UserTestId,                               
                             }).Distinct().ToList();


                if (Tests.Count > 0)
                {
                    foreach(TestResultDTO res in Tests)
                    {
                        var CorrectAns= (from ta in con.Table<TTestAnswers>()
                                         join ut in con.Table<TUserTest>()
                           on ta.UserTestId equals ut.UserTestId
                                         join an in con.Table<MAnswer>()
                                  on ta.AnswerId equals an.AnswerId
                                         join t in con.Table<MTest>() on ut.TestId equals t.TestId
                                         where an.IsCorrectAnswer==true
                                         && an.AnswerId==ta.AnswerId
                                 && ut.TestId==res.TestId 
                                 && ta.UserTestId==res.UserTestId
                                 && ut.TestId== res.TestId
                                         select ta.AnswerId
                                  ).Count();

                        var InCorrectAns = (from ta in con.Table<TTestAnswers>()
                                          join an in con.Table<MAnswer>()
                                          on ta.AnswerId equals an.AnswerId
                                          where an.IsCorrectAnswer == false
                                          select ta.AnswerId
                                 ).Count();

                        res.CorrectAnswers = CorrectAns;
                        res.InCorrectAnswers= InCorrectAns;
                    }

                    result.ok = true;
                    result.TestResults = Tests;
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
                result.msg = "Error while getting Test. " + ex.Message;
            }

            return result;
        }
    }
}

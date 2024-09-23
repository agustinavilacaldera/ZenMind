using System;
using SQLite;
using System.Linq;
using ZenMind.DAL.DB;

namespace ZenMind.DAL
{
    public class UserTestDAL
    {
        public ResultModel<Boolean> SaveUserTest(TUserTest UserTest)
        {
            ResultModel<Boolean> result = new ResultModel<Boolean>();
            Int64 res = 0;

            try
            {
                
                SQLiteConnection con = SqlConnection.GetConnection();

                if (UserTest.UserTestId == 0)
                {
                    res = con.Insert(UserTest);
                }
                else
                {
                    res = con.InsertOrReplace(UserTest);
                }

                if (res > 0)
                {
                    result.IdentityId = SQLite3.LastInsertRowid(con.Handle);
                    result.ok = true;
                    result.msg = "Test  has been saved";
                }
                else
                {
                    result.ok = false;
                    result.msg = "Error while saving Test ";
                }
            }
            catch (Exception ex)
            {
                result.ok = false;
                result.msg = "Error while saving Test " + ex.Message;
            }

            return result;
        }

        public ResultModel<Boolean> DeleteUserTest(Int64 UserTestId)
        {
            ResultModel<Boolean> result = new ResultModel<Boolean>();

            Int64 res = 0;

            try
            {
                SQLiteConnection con = SqlConnection.GetConnection();
                res = con.Delete(UserTestId);

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

        public ResultModel<Boolean> GetUserTests(TUserTest Test)
        {
            ResultModel<Boolean> result = new ResultModel<Boolean>();

            try
            {
                SQLiteConnection con = SqlConnection.GetConnection();

                var Tests = (from x in con.Table<TUserTest>()
                             where (x.TestId == Test.TestId || Test.TestId == 0)
                             && (x.UserTestId == Test.UserTestId || Test.UserTestId == 0)
                              && (x.UserId == Test.UserId || Test.UserId == 0)
                             select x).ToList();

                if (Tests.Count > 0)
                {
                    result.ok = true;
                    result.UserTests = Tests;
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

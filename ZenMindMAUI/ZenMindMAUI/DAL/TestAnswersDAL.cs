using System;
using SQLite;
using System.Linq;
using ZenMind.DAL.DB;

namespace ZenMind.DAL
{
    public class TestAnswersDAL
    {
        public ResultModel<Boolean> SaveTestAnswer(TTestAnswers TestAnswer)
        {
            ResultModel<Boolean> result = new ResultModel<Boolean>();
            Int64 res = 0;

            try
            {
                string query = "";

                SQLiteConnection con = SqlConnection.GetConnection();

                if (TestAnswer.TestAnswerId == 0 )
                {
                    res = con.Insert(TestAnswer);
                }
                else
                {
                    res = con.InsertOrReplace(TestAnswer);
                }

                if (res > 0)
                {
                    result.IdentityId = SQLite3.LastInsertRowid(con.Handle);
                    result.ok = true;
                    result.msg = "Test Answer has been created";
                }
                else
                {
                    result.ok = false;
                    result.msg = "Error while creating Test Answer ";
                }
            }
            catch (Exception ex)
            {
                result.ok = false;
                result.msg = "Error while creating Test Answer" + ex.Message;
            }

            return result;
        }

        public ResultModel<Boolean> DeleteTestAnswer(Int64 TestAnswerId)
        {
            ResultModel<Boolean> result = new ResultModel<Boolean>();

            Int64 res = 0;

            try
            {
                SQLiteConnection con = SqlConnection.GetConnection();
                res = con.Delete(TestAnswerId);

                if (res > 0)
                {
                    result.ok = true;

                    result.msg = "Test Answer has been deleted";
                }
                else
                {
                    result.ok = false;
                    result.msg = "Error while deleting Test Answer";
                }
            }
            catch (Exception ex)
            {
                result.ok = false;
                result.msg = "Error while deleting Test Answer " + ex.Message;
            }

            return result;
        }

        public ResultModel<Boolean> GetTestAnswers(TTestAnswers TestAnswer)
        {
            ResultModel<Boolean> result = new ResultModel<Boolean>();

            try
            {
                SQLiteConnection con = SqlConnection.GetConnection();
                var TestAnswers = (from x in con.Table<TTestAnswers>()
                             where (x.TestAnswerId == TestAnswer.TestAnswerId || TestAnswer.TestAnswerId == 0)
                             select x).ToList();

                if (TestAnswers.Count > 0)
                {
                    result.ok = true;
                    result.TestAnswers = TestAnswers;
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
                result.msg = "Error while getting Test Answer. " + ex.Message;
            }

            return result;
        }
    }
}

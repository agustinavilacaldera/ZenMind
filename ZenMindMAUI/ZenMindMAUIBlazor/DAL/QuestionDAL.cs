using System;
using SQLite;
using System.Linq;
using ZenMind.DAL.DB;

namespace ZenMind.DAL
{
    public class QuestionDAL
    {
        public ResultModel<Boolean> SaveQuestion(MQuestion Question)
        {
            ResultModel<Boolean> result = new ResultModel<Boolean>();
            Int64 res = 0;

            try
            {
                

                SQLiteConnection con = SqlConnection.GetConnection();

                if (Question.QuestionId == 0 )
                {
                    res = con.Insert(Question);
                }
                else
                {
                    res = con.InsertOrReplace(Question);
                }

                if (res > 0)
                {
                    result.IdentityId = SQLite3.LastInsertRowid(con.Handle);
                    result.ok = true;
                    result.msg = "Question has been created";
                }
                else
                {
                    result.ok = false;
                    result.msg = "Error while creating Question";
                }
            }
            catch (Exception ex)
            {
                result.ok = false;
                result.msg = "Error while creating Question account" + ex.Message;
            }

            return result;
        }

        public ResultModel<Boolean> DeleteQuestion(Int64 QuestionId)
        {
            ResultModel<Boolean> result = new ResultModel<Boolean>();

            Int64 res = 0;

            try
            {
                SQLiteConnection con = SqlConnection.GetConnection();
                res = con.Delete(QuestionId);

                if (res > 0)
                {
                    result.ok = true;

                    result.msg = "Question has been deleted";
                }
                else
                {
                    result.ok = false;
                    result.msg = "Error while deleting Question";
                }
            }
            catch (Exception ex)
            {
                result.ok = false;
                result.msg = "Error while deleting Question " + ex.Message;
            }

            return result;
        }

        public ResultModel<Boolean> GetQuestions(MQuestion Question)
        {
            ResultModel<Boolean> result = new ResultModel<Boolean>();

            try
            {
                SQLiteConnection con = SqlConnection.GetConnection();
                var Questions = (from x in con.Table<MQuestion>()
                             where (x.QuestionId == Question.QuestionId || Question.QuestionId == 0)
                             && (x.TestId == Question.TestId || Question.TestId == 0)
                                 select x).ToList();

                if (Questions.Count > 0)
                {
                    result.ok = true;
                    result.Questions = Questions;
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
                result.msg = "Error while getting Question. " + ex.Message;
            }

            return result;
        }
    }
}

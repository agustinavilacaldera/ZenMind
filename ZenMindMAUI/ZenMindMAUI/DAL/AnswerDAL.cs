using System;
using SQLite;
using System.Linq;
using ZenMind.DAL.DB;

namespace ZenMind.DAL
{
    public class AnswerDAL
    {
        public ResultModel<Boolean> SaveAnswer(MAnswer Answer)
        {
            ResultModel<Boolean> result = new ResultModel<Boolean>();
            Int64 res = 0;

            try
            {
                //string query = "";

                SQLiteConnection con = SqlConnection.GetConnection();

                if (Answer.AnswerId == 0)
                {
                    res = con.Insert(Answer);
                }
                else
                {
                    res = con.InsertOrReplace(Answer);
                }

                if (res > 0)
                {
                    result.IdentityId = SQLite3.LastInsertRowid(con.Handle);
                    result.ok = true;
                    result.msg = "Answer has been created";
                }
                else
                {
                    result.ok = false;
                    result.msg = "Error while creating Answer";
                }
            }
            catch (Exception ex)
            {
                result.ok = false;
                result.msg = "Error while creating Answer" + ex.Message;
            }

            return result;
        }

        public ResultModel<Boolean> DeleteAnswer(Int64 AnswerId)
        {
            ResultModel<Boolean> result = new ResultModel<Boolean>();

            Int64 res = 0;

            try
            {
                SQLiteConnection con = SqlConnection.GetConnection();
                res = con.Delete(AnswerId);

                if (res > 0)
                {
                    result.ok = true;

                    result.msg = "Answer has been deleted";
                }
                else
                {
                    result.ok = false;
                    result.msg = "Error while deleting Answer";
                }
            }
            catch (Exception ex)
            {
                result.ok = false;
                result.msg = "Error while deleting Answer " + ex.Message;
            }

            return result;
        }

        public ResultModel<Boolean> GetAnswers(MAnswer Answer)
        {
            ResultModel<Boolean> result = new ResultModel<Boolean>();

            try
            {
                SQLiteConnection con = SqlConnection.GetConnection();
                var Answers = (from x in con.Table<MAnswer>()
                                 where (x.AnswerId == Answer.AnswerId || Answer.AnswerId == 0)
                                 &&(x.QuestionId == Answer.QuestionId || Answer.QuestionId == 0)
                               select x).ToList();

                if (Answers.Count > 0)
                {
                    result.ok = true;
                    result.Answers = Answers;
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
                result.msg = "Error while getting Answer. " + ex.Message;
            }

            return result;
        }
    }
}

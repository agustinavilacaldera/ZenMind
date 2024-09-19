using System;
using ZenMind.DAL.DB;
using ZenMind.DAL;

namespace ZenMind.BLL
{
    public class QuestionBLL
    {
        public ResultModel<Boolean> SaveQuestion(MQuestion Question)
        {
            QuestionDAL QuestionDAL = new QuestionDAL();
            return QuestionDAL.SaveQuestion(Question);
        }

        public ResultModel<Boolean> DeleteQuestion(Int64 QuestionId)
        {
            QuestionDAL QuestionDAL = new QuestionDAL();
            return QuestionDAL.DeleteQuestion(QuestionId);
        }

        public ResultModel<Boolean> GetQuestions(MQuestion Question)
        {
            QuestionDAL QuestionDAL = new QuestionDAL();
            return QuestionDAL.GetQuestions(Question);
        }
    }
}

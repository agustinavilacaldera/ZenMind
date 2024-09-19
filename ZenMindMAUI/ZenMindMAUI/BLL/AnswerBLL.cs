using ZenMind.DAL;
using ZenMind.DAL.DB;

namespace ZenMind.BLL
{
  public class AnswerBLL
    {
        public ResultModel<Boolean> SaveAnswer(MAnswer Answer)
        {
            AnswerDAL answerDAL = new AnswerDAL();
            return answerDAL.SaveAnswer(Answer);
        }

        public ResultModel<Boolean> DeleteAnswer(Int64 AnswerId)
        {
            AnswerDAL answerDAL = new AnswerDAL();
            return answerDAL.DeleteAnswer(AnswerId);
        }

        public ResultModel<Boolean> GetAnswers(MAnswer Answer)
        {
            AnswerDAL answerDAL = new AnswerDAL();
            return answerDAL.GetAnswers(Answer);
        }
    }
}

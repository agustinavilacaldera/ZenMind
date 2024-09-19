using System;
using ZenMind.DAL.DB;
using ZenMind.DAL;

namespace ZenMind.BLL
{
    internal class TestAnswersBLL
    {
        public ResultModel<Boolean> SaveTestAnswer(TTestAnswers TestAnswer)
        {
            TestAnswersDAL TestAnswersDAL = new TestAnswersDAL();
            return TestAnswersDAL.SaveTestAnswer(TestAnswer);
        }

        public ResultModel<Boolean> DeleteTestAnswer(Int64 TestAnswerId)
        {
            TestAnswersDAL TestAnswersDAL = new TestAnswersDAL();
            return TestAnswersDAL.DeleteTestAnswer(TestAnswerId);
        }

        public ResultModel<Boolean> GetTestAnswers(TTestAnswers TestAnswer)
        {
            TestAnswersDAL TestAnswersDAL = new TestAnswersDAL();
            return TestAnswersDAL.GetTestAnswers(TestAnswer);
        }
    }
}

using SQLite;

namespace ZenMind.DAL.DB
{
    public class TTestAnswers
    {
        [PrimaryKey, AutoIncrement]
        public int TestAnswerId { get; set; }
        public int UserTestId { get; set; }
        public int QuestionId { get; set; }
        public int AnswerId { get; set; }
    }
}

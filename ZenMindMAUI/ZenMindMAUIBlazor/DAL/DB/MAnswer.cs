using SQLite;

namespace ZenMind.DAL.DB
{
    public class MAnswer
    {
        [PrimaryKey, AutoIncrement]
        public int AnswerId { get; set; }
        public int QuestionId { get; set; }
        public string Answer { get; set; }
        public bool IsCorrectAnswer { get; set; }
    }
}

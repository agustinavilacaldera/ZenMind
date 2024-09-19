using SQLite;

namespace ZenMind.DAL.DB
{
    public class MQuestion
    {
        [PrimaryKey, AutoIncrement]
        public int QuestionId { get; set; }
        public int TestId { get; set; }
        public string Question { get; set; }
    }
}

using SQLite;

namespace ZenMind.DAL.DB
{
    public class TUserTest
    {
        [PrimaryKey, AutoIncrement]
        public int UserTestId { get; set; }
        public int TestId { get; set; }
        public int UserId { get; set; }
        public string PerformedOn { get; set; }
    }
}

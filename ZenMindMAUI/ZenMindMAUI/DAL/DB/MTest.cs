using SQLite;

namespace ZenMind.DAL.DB
{
  public class MTest
    {
        [PrimaryKey, AutoIncrement]
        public int TestId { get; set; }
        public string Title { get; set; }
        public string Date { get; set; }
        public string Description { get; set; }
    }
}

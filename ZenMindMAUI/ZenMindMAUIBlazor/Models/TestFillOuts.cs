using SQLite;

namespace ZenMindMAUIBlazor.Models
{
  internal class TestFillOuts
  {
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    [Indexed]
    public int TestAssignmentId { get; set; }
    [Indexed]
    public int QuestionsId { get; set; }
    public int Answer { get; set; }
  }
}

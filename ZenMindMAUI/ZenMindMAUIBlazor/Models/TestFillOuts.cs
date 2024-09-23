using SQLite;

namespace ZenMindMAUIBlazor.Models
{
  internal class TestFillOuts
  {
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    public int PacienteId { get; set; }
    public int TestId { get; set; }
    [Indexed]
    public int QuestionsId { get; set; }
    public int Answer { get; set; }
  }
}

using SQLite;

namespace ZenMindMAUIBlazor.Models.Data
{
    internal class TestFillOuts
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; } = -1;
        [Indexed]
        public int TestAssignmentId { get; set; }
        [Indexed]
        public int QuestionsId { get; set; }
        public int Answer { get; set; }
    }
}

using SQLite;

namespace ZenMind.DAL.DB
{
    public class MUserType
    {
        [PrimaryKey, AutoIncrement]
        public int UserTypeId { get; set; }
        public string UserType { get; set; }
    }
}

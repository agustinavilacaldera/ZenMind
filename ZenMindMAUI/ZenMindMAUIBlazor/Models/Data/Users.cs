using SQLite;

namespace ZenMindMAUIBlazor.Models.Data
{
    internal class Users
    {
        [PrimaryKey]
        public string UserName { get; set; }
        public string Password { get; set; }
        public int UserType { get; set; }
    }
}

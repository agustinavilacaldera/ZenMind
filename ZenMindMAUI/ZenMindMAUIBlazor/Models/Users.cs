using SQLite;

namespace ZenMindMAUIBlazor.Models
{
  internal class Users
  {
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    [Unique]
    public string UserName { get; set; }
    public string Password { get; set; }
    public int UserType { get; set; }
  }
}

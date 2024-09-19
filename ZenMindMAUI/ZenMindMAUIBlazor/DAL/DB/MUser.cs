using SQLite;

namespace ZenMind.DAL.DB
{
  public class MUser
  {
    [PrimaryKey, AutoIncrement]
    public int UserId { get; set; }
    public int UserType { get; set; }
    public string Name { get; set; } = "";
    public string Password { get; set; } = "";
    [Unique]
    public string Email { get; set; } = "";
    public string Surname { get; set; } = "";
    public string Birthdate { get; set; } = "";
    public string Telephone { get; set; } = "";
    public bool AllowEmailNotifications { get; set; } = true;
    public bool PlayBackgroundMusic { get; set; } = false;
    public string ProfileImage { get; set; } = "";
  }
}

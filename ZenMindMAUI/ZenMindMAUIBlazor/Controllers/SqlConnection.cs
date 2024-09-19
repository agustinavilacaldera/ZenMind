using SQLite;

namespace ZenMindMAUIBlazor.Controllers;

public class SqlConnection
{
  public static SQLiteConnection GetConnection()
  {
    string AppPath = AppDomain.CurrentDomain.BaseDirectory;
    
    var path = Path.Combine(AppPath, "ZenMindDB.db");

    SQLiteConnection _SQLiteConnection = new SQLiteConnection(path);

    return _SQLiteConnection;
  }
}

using SQLite;

namespace ZenMind.DAL
{
  public class SqlConnection
  {
    public static SQLiteConnection GetConnection()
    {
      string AppPath = AppDomain.CurrentDomain.BaseDirectory;
      //var documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData);// Xamarin.Essentials.FileSystem.AppDataDirectory;//Environment.SpecialFolder.MyDocuments  System.Environment.SpecialFolder.LocalApplicationData
      var path = Path.Combine(AppPath, "ZenMindDB.db");
      // var path = "/storage/emulated/0/Android/data/com.companyname.zenmind/files/ZenMindDB.db";

      SQLiteConnection _SQLiteConnection = new SQLiteConnection(path);

      return _SQLiteConnection;
    }
  }
}

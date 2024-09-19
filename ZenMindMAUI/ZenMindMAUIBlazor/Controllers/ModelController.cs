
using SQLite;
using ZenMindMAUIBlazor.Models;
using static SQLite.SQLite3;

namespace ZenMindMAUIBlazor.Controllers;

internal class ModelController
{
  public Users User { get; set; }

  public ModelController()
  {
    initDatabase();
  }

  private void initDatabase()
  {
    try
    {
      SQLiteConnection con = SqlConnection.GetConnection();

      con.CreateTable<Users>();
      con.CreateTable<TestFillOuts>();
      con.CreateTable<TestAssignments>();
      con.CreateTable<Tests>();
      con.CreateTable<Questions>();
      con.CreateTable<Pacientes>();
      con.CreateTable<MedicoPaciente>();
      con.CreateTable<Medicos>();
      con.CreateTable<Administrativos>();
    }
    catch (Exception ex) { }
    
    
  }
  public bool Login(Login l)
  {
    try
    {
      SQLiteConnection con = SqlConnection.GetConnection();

      List<Users> Users = (from x in con.Table<Users>() where x.UserName.Equals(l.Usuario) && x.Password.Equals(l.Password) select x).ToList();

      if (Users.Count > 0)
      {
        this.User= Users[0];
        return true;
      }
    }
    catch (Exception ex)
    {
      
    }
    return false;
  }

  public bool tablaUserVacia()
  {
    SQLiteConnection connection = SqlConnection.GetConnection();
    List<Users> users=(from x in connection.Table<Users>()  select x).ToList();
    if (users.Count > 0) {
      return false;
    }
    return true;
  }
  /*
  public bool Login(Login l)
  {

    MUser usr = new MUser(); //User Data object class object creation
    usr.Email = l.Usuario;//Sets values for pass to the DB for check
    usr.Password = l.Password;

    UserBLL userBLL = new UserBLL();

    ZenMind.DAL.ResultModel<bool> result = userBLL.CheckLogin(usr);

    if (result.ok)
    {
      if (result.Users != null)
      {
        if (result.Users.Count > 0)
        {
          this.User = result.Users[0];


          TLogs log = new TLogs();
          LogBLL logBLL = new LogBLL();

          log.Action = "Login";
          log.DateTime = DateTime.Now;
          log.UserId = this.User.UserId;

          ResultModel<bool> logResult = new ResultModel<bool>();
          logResult = logBLL.SaveLog(log);
          return true;
        }
      }
    }
    return false;
  }
  */
  /*
  public List<MUser> listarUsuarios()
  {
    List<MUser> lstUsers=new();
    UserBLL userBLL = new UserBLL();

    ResultModel<bool> result = userBLL.GetUsers();
    //lstUsers.ItemsSource = null;

    if (result.ok)
    {
      if (result.Users != null)
      {
        if (result.Users.Count > 0)
        {
          lstUsers = result.Users;
        }
      }
    }
    return lstUsers;
  }*/

}

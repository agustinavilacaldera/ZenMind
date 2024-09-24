
using SQLite;
using ZenMindMAUIBlazor.Models;


namespace ZenMindMAUIBlazor.Controllers;

internal class ModelController
{
  public Users User { get; set; }
  public string message { get; set; }
  private SQLiteConnection connection;

  public void RemoverMedicoPaciente(MedicoPaciente mp)
  {
    connection.Delete(mp);
  }
  public void ActualizarMedicoPaciente(MedicoPaciente mp)
  {

    if (existeMedicoPaciente(mp.Id))
    {
      connection.InsertOrReplace(mp);
    }
    else
    {
      mp.Id = nextMedicoPaciente();
      connection.InsertOrReplace(mp);
    }

  }
  private bool existeMedicoPaciente(int id)
  {
    if ((from x in connection.Table<Pacientes>()
         where x.Id == id
         select x).ToList().Count() > 0)
      return true;
    return false;
  }
  private int nextMedicoPaciente()
  {
    try
    {
      return (from x in connection.Table<MedicoPaciente>()
              select x).ToList().Max(x => x.Id) + 1;
    }
    catch { return 0; }
  }
  public List<Pacientes> ListarPacientesNoAsignados(int mId)
  {
    try
    {
      List<MedicoPaciente> lm = ListarMedicoPacientes(mId);
      List<Pacientes> lp = ListarPacientes();
      List<Pacientes> r = new();
      foreach (Pacientes p in lp)
      {
        if(!lm.Exists(x=>x.PacientesId == p.Id))
          r.Add(p);
      }
      return r;
    }
    catch { return null; }

  }
  public List<Pacientes> ListarPacientes()
  {
    try
    {
      return (from x in connection.Table<Pacientes>()
              select x).ToList();
    }
    catch { return null; }
  }
  public Pacientes CargarPaciente(int pId)
  {
    try
    {
      return (from x in connection.Table<Pacientes>()
              where x.Id == pId
              select x).FirstOrDefault();
    }
    catch { return null; }
  }
  public List<MedicoPaciente> ListarMedicoPacientes(int mId)
  {
    try
    {
      return (from x in connection.Table<MedicoPaciente>()
              where x.MedicosId == mId
              select x).ToList();
    }
    catch { return null; }
  }
  public List<Medicos> ListarMedicos()
  {
    try
    {
      return (from x in connection.Table<Medicos>()
              select x).ToList();
    }
    catch { return null; }
  }
  public Medicos CargarMedico(int mId)
  {
    try
    {
      return (from x in connection.Table<Medicos>()
              where x.Id == mId
              select x).FirstOrDefault();
    }
    catch { return null; }
  }
  public void BorrarQuestion(Questions q)
  {
    connection.Delete(q);
  }
  public void ActualizarQuestion(Questions q)
  {
    if (existeQuestion(q.Id))
    {
      connection.InsertOrReplace(q);
    }
    else
    {
      q.Id = nextQuestion();
      connection.Insert(q);
    }
  }
  private int nextQuestion()
  {
    try
    {
      return (from x in connection.Table<Questions>()
              select x).ToList().Max(x => x.Id) + 1;
    }
    catch { return 0; }
  }
  private bool existeQuestion(int qId)
  {
    if ((from x in connection.Table<Questions>()
         where x.Id == qId
         select x).ToList().Count() > 0)
      return true;
    return false;
  }
  public Questions CargarQuestion(int qId)
  {
    try
    {
      return (from x in connection.Table<Questions>()
              where x.Id == qId
              select x).FirstOrDefault();
    }
    catch { return null; }
  }
  public List<Questions> ListarQuestion(int tId)
  {
    try
    {
      return (from x in connection.Table<Questions>()
              where x.TestsId == tId
              select x).ToList();
    }
    catch { return null; }

  }
  public void BorrarTest(Tests t)
  {
    connection.Delete(t);
  }
  public Tests NuevoTest()
  {
    Tests t = new Tests();
    t.Id = nextTest();
    ActualizarTest(t);
    return t;
  }
  public void ActualizarTest(Tests t)
  {
    connection.InsertOrReplace(t);
  }
  private int nextTest()
  {
    try
    {
      return (from x in connection.Table<Tests>()
              select x).ToList().Max(x => x.Id) + 1;
    }
    catch { return 0; }
  }
  public Tests CargarTest(int tId)
  {
    try
    {
      return (from x in connection.Table<Tests>()
              where x.Id == tId
              select x).ToList()[0];
    }
    catch { return null; }
  }
  public void ActualizarPaciente(Pacientes p)
  {
    if (existePaciente(p.Id))
    {
      connection.InsertOrReplace(p);
    }
    else
    {
      p.Id = nextPaciente();
      connection.Insert(p);
    }
  }
  private int nextPaciente()
  {
    try
    {
      return (from x in connection.Table<Pacientes>()
              select x).ToList().Max(x => x.Id) + 1;
    }
    catch { return 0; }
  }
  private bool existePaciente(int id)
  {
    if ((from x in connection.Table<Pacientes>()
         where x.Id == id
         select x).ToList().Count() > 0)
      return true;
    return false;
  }
  public void ActualizarMedico(Medicos m)
  {
    if (existeMedico(m.Id))
    {
      connection.InsertOrReplace(m);
    }
    else
    {
      m.Id = nextMedico();
      connection.Insert(m);
    }

  }
  private bool existeMedico(int mId)
  {
    if ((from x in connection.Table<Medicos>()
         where x.Id == mId
         select x).ToList().Count() > 0)
      return true;
    return false;
  }
  private int nextMedico()
  {
    try
    {
      return (from x in connection.Table<Medicos>()
              select x).ToList().Max(x => x.Id) + 1;
    }
    catch { return 0; }
  }
  public List<Tests> ListarTest()
  {
    return (from x in connection.Table<Tests>()
            select x).ToList();
  }
  public void ActualizarUsuarios(Users u)
  {
    if (ExisteUsuario(u.UserName))
    {
      var res = connection.InsertOrReplace(u);
    }
    else
    {
      var res = connection.Insert(u);
    }
  }
  public int GetIdPersona(string userId)
  {
    Users user = CargarUsuario(userId);
    switch (user.UserType)
    {
      case 1:
        Administrativos a = CargarAdministradorPorUserId(userId);
        if (a != null)
          return a.Id;
        else
          return -1;
      //break;
      case 2:
        Medicos m = CargarMedicoPorUserId(userId);
        if (m != null)
          return m.Id;
        else
          return -1;
      //break;
      case 3:
        Pacientes p = CargarPacientePorUserId(userId);
        if (p != null)
          return p.Id;
        else
          return -1;
        //break;
    }
    return -1;
  }
  public string GetNombrePersona(string userId)
  {
    Users user = CargarUsuario(userId);
    switch (user.UserType)
    {
      case 1:
        Administrativos a = CargarAdministradorPorUserId(userId);
        if (a != null)
          return a.Name + " " + a.SurName;
        else
          return "Administrador No Creado";
      //break;
      case 2:
        Medicos m = CargarMedicoPorUserId(userId);
        if (m != null)
          return m.Name + " " + m.SurName;
        else
          return "Medico No Creado";
      //break;
      case 3:
        Pacientes p = CargarPacientePorUserId(userId);
        if (p != null)
          return p.Name + " " + p.SurName;
        else
          return "Paciente No Creado";
        //break;
    }
    return "";
  }
  public Pacientes CargarPacientePorUserId(string uid)
  {
    try
    {
      return (from x in connection.Table<Pacientes>()
              where x.UsersId == uid
              select x).ToList()[0];
    }
    catch { return null; }

  }
  public Medicos CargarMedicoPorUserId(string uid)
  {
    try
    {
      return (from x in connection.Table<Medicos>()
              where x.UsersId == uid
              select x).ToList()[0];
    }
    catch { return null; }

  }
  public Administrativos CargarAdministradorPorUserId(string uid)
  {
    try
    {
      return (from x in connection.Table<Administrativos>()
              where x.UsersId.Equals(uid)
              select x).ToList()[0];
    }
    catch
    {
      return null;
    }

  }
  public Users CargarUsuario(string userId)
  {
    try
    {
      return (from x in connection.Table<Users>()
              where x.UserName.Equals(userId)
              select x).ToList()[0];
    }
    catch { return null; }


  }
  public List<Users> ListarUsuarios()
  {
    try
    {
      List<Users> list = (from x in connection.Table<Users>()
                          select x).ToList();
      return list;
    }
    catch (Exception ex)
    {
      message = ex.Message;
    }
    return null;
  }
  public List<Administrativos> ListarAdministrativos()
  {
    try
    {
      List<Administrativos> list = (from x in connection.Table<Administrativos>()
                                    select x).ToList();
      return list;
    }
    catch (Exception ex)
    {
      message = ex.Message;
    }

    return null;
  }
  public void NuevoAdminsitrativo(Administrativos administrativos)
  {
    try
    {
      administrativos.Id = NextAdministrativo();
      var r = connection.Insert(administrativos);
    }
    catch { }
  }
  private int NextAdministrativo()
  {
    try
    {
      return (from x in connection.Table<Administrativos>()
              select x).ToList().Max(x => x.Id) + 1;
    }
    catch
    {
      return 0;
    }
  }
  public void ActualizarAdministrativo(Administrativos administrativos)
  {
    try
    {
      if (ExisteAdministrativo(administrativos.Id))
      {
        var r = connection.InsertOrReplace(administrativos);
      }
      else
        NuevoAdminsitrativo(administrativos);
    }
    catch (Exception ex)
    {
      message = ex.Message;
    }
  }
  public bool ExisteAdministrativo(int aId)
  {
    int l = (from x in connection.Table<Administrativos>()
             where x.Id == aId
             select x).ToList().Count();
    if (l > 0)
      return true;
    return false;
  }
  public bool ExisteUsuario(string username)
  {
    int u = (from x in connection.Table<Users>()
             where x.UserName.Equals(username)
             select x).ToList().Count();
    if (u > 0)
      return true;
    return false;
  }
  public ModelController()
  {
    initDatabase();
  }
  private void initDatabase()
  {
    try
    {
      connection = SqlConnection.GetConnection();
      connection.CreateTable<Users>();
      connection.CreateTable<TestFillOuts>();
      connection.CreateTable<TestAssignments>();
      connection.CreateTable<Tests>();
      connection.CreateTable<Questions>();
      connection.CreateTable<Pacientes>();
      connection.CreateTable<MedicoPaciente>();
      connection.CreateTable<Medicos>();
      connection.CreateTable<Administrativos>();
    }
    catch { }
  }
  public bool Login(Login l)
  {
    try
    {
      List<Users> Users = (from x in connection.Table<Users>()
                           where x.UserName.Equals(l.Usuario) && x.Password.Equals(l.Password)
                           select x).ToList();

      if (Users.Count > 0)
      {
        this.User = Users[0];
        return true;
      }
    }
    catch (Exception ex)
    {
      message = ex.Message;
    }
    return false;
  }
  public bool TablaUserVacia()
  {
    try
    {
      List<Users> users = (from x in connection.Table<Users>() select x).ToList();
      if (users.Count > 0)
      {
        return false;
      }
    }
    catch (Exception ex)
    {
      message = ex.Message;
    }

    return true;
  }


}

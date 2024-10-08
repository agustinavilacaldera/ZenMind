
using Newtonsoft.Json;
using SQLite;
using System.Net.Mail;
using System.Net;
using ZenMindMAUIBlazor.Models.Config;
using ZenMindMAUIBlazor.Models.Miscelanea;
using ZenMindMAUIBlazor.Models.Data;
using ZenMindMAUIBlazor.Models.Report;


namespace ZenMindMAUIBlazor.Controllers;

internal class ModelController
{
  public Configuraciones Settings { get; set; }
  public Users User { get; set; }
  public string Ruta { get; set; }
  public string Message { get; set; }
  private SQLiteConnection connection;

  public ReportePaciente CargarReportePaciente(int pId)
  {
    return new ReportePaciente()
    {
      Pacientes = CargarPaciente(pId),
      TestAssignments = ListarTestAssignmentPorPaciente(pId)
    };
  }
  private void enviarCorreo(string toEmail, string subject, string body)
  {
    //string StatusMessage = string.Empty;
    string email = this.Settings.Email.Email;
    string host = this.Settings.Email.Host;
    int port=this.Settings.Email.Port;
    string pwd = this.Settings.Email.Password;
    try
    {
      using (MailMessage mail = new MailMessage())
      {
        mail.From = new MailAddress(email);
        mail.To.Add(toEmail);
        mail.Subject = subject;
        mail.Body = body;
        mail.IsBodyHtml = true;

        using (SmtpClient smtp = new SmtpClient(host, port))
        {
          smtp.Credentials = new NetworkCredential(email, pwd);
          smtp.EnableSsl = true;
          
          smtp.Send(mail);
        }
      }
    }
    catch (SmtpException ex)
    {
      Message = $"SMTP error: {ex.Message}";
    }
    catch (Exception ex)
    {
      Message = $"Error while sending email: {ex.Message}";
    }
  }
  public void notificarAlMedico(TestAssignments ta)
  {
    ta = CargarTestAssignment(ta.Id);
    if (ta.ObtenerCalificacion() <= 1)
    {
      Pacientes p = CargarPaciente(ta.PacientesId);
      enviarCorreo(CargarMedico(ta.MedicosId).Email,"Paciente en Rojo", $"El paciente {p.SurName} {p.Name} ha obtenido una muy mala valoración");
    }
  }
  public void notificarAlPaciente(TestAssignments ta)
  {
    Pacientes p = CargarPaciente(ta.PacientesId);
    Medicos m = CargarMedico(ta.MedicosId);
    //ta = CargarTestAssignment(ta.Id);
    //if (ta.ObtenerCalificacion() <= 1)
    //{
    //  Pacientes p = CargarPaciente(ta.PacientesId);
      enviarCorreo(p.Email, "Nuevo Test", $"El Dr {m.SurName} {m.Name} le ha asignado un nuevo test para el día {ta.Date.ToShortDateString()}");
    //}
  }
  public void LoadSettings()
  {
    try
    {
      LoadSettingFromFile();
      //apiController = new(Configuraciones.ApiServer);
    }
    catch (Exception)
    {
      Message = "The settings file is empty, please record the settings";

    }
  }
  private void LoadSettingFromFile()
  {
    //string dir = Directory.GetCurrentDirectory();

    string f = Ruta + "\\Configuraciones.json";
    string r = "";
    try
    {
      StreamReader st = new StreamReader(f);
      r = st.ReadLine();
      st.Close();
      Settings = JsonConvert.DeserializeObject<Configuraciones>(r);
    }
    catch (Exception)
    {
      Settings = new Configuraciones();
      SaveSettings();
      //throw;
    }
    //return r;
  }
  public void SaveSettings()
  {
    //string dir = Directory.GetCurrentDirectory();

    string f = Ruta + "\\Configuraciones.json";
    //StringContent sc = new StringContent(JsonConvert.SerializeObject(setting), Encoding.UTF8, "application/json");
    string sc = JsonConvert.SerializeObject(Settings);
    try
    {
      StreamWriter sw = new StreamWriter(f);
      string s = sc;
      sw.WriteLine(s);
      sw.Close();
    }
    catch (Exception)
    {
      //throw;
    }
  }
  public TestFillOuts CargarTestFillOuts(int tfoId)
  {
    try
    {
      return (from x in connection.Table<TestFillOuts>()
              where x.Id == tfoId
              select x).FirstOrDefault();
    }
    catch { return null; }
  }
  public string GetColor(float eval)
  {
    if (eval > 0)
    {
      if (eval <= 1f)
        return "color-r1";
      if (eval <= 2f)
        return "color-r2";
      if (eval <= 3f)
        return "color-r3";
      if (eval <= 4f)
        return "color-r4";
      if (eval <= 5f)
        return "color-r5";
    }
    else
      return "color-r0";
    return "color-r0";
  }
  public string GetColor(int eval)
  {
    if (eval == 1)
      return "color-r1";
    if (eval == 2)
      return "color-r2";
    if (eval == 3)
      return "color-r3";
    if (eval == 4)
      return "color-r4";
    if (eval == 5)
      return "color-r5";
    return "color-r0";
  }
  public List<TestAssignments> ListarTestAssignmentPorPaciente(int pId)
  {
    try
    {
      return (from x in connection.Table<TestAssignments>()
              where x.PacientesId == pId
              select x).ToList();
    }
    catch { return null; }
  }
  public List<TestAssignments> ListarTestAssignmentRespondidosPorPaciente(int pId)
  {
    try
    {
      return (from x in connection.Table<TestAssignments>()
              where x.PacientesId == pId && x.Respondido
              select x).ToList();
    }
    catch { return null; }
  }
  public void ActualizarTestFillOut(TestFillOuts fillOuts)
  {
    if (existeTestFillOut(fillOuts.Id))
    {
      connection.InsertOrReplace(fillOuts);
    }
    else
    {
      fillOuts.Id = nextTestFillOut();
      connection.InsertOrReplace(fillOuts);
    }
  }
  private int nextTestFillOut()
  {
    try
    {
      return (from x in connection.Table<TestFillOuts>()
              select x).ToList().Max(x => x.Id) + 1;
    }
    catch { return 0; }
  }
  private bool existeTestFillOut(int id)
  {
    if ((from x in connection.Table<TestFillOuts>()
         where x.Id == id
         select x).ToList().Count() > 0)
      return true;
    return false;
  }
  public List<TestAssignments> ListarTestAssignmentNoResueltosPorPaciente(int pId)
  {
    try
    {
      return (from x in connection.Table<TestAssignments>()
              where x.Date >= DateTime.Today && !x.Respondido
              select x).ToList();
    }
    catch { return null; }
  }
  public TestAssignments ActualizarTestAssignment(TestAssignments ta)
  {
    if (existeTestAssignment(ta.Id))
    {
      connection.InsertOrReplace(ta);
    }
    else
    {
      ta.Id = nextTestAssignment();
      connection.InsertOrReplace(ta);
      enviarCorreo(CargarPaciente(ta.PacientesId).Email,"Nuevo test para responder", $"Se le a asignado un nuevo test para el día {ta.Date.ToShortDateString()}");
    }
    return ta;
  }
  private int nextTestAssignment()
  {
    try
    {
      return (from x in connection.Table<TestAssignments>()
              select x).ToList().Max(x => x.Id) + 1;
    }
    catch { return 0; }
  }
  private bool existeTestAssignment(int id)
  {
    if ((from x in connection.Table<TestAssignments>()
         where x.Id == id
         select x).ToList().Count() > 0)
      return true;
    return false;
  }
  public TestAssignments CargarTestAssignment(int taId)
  {
    try
    {
      TestAssignments t = (from x in connection.Table<TestAssignments>()
                           where x.Id == taId
                           select x).FirstOrDefault();
      if (t != null)
        t.TestFillOuts = listarTestFillOutsPorTestAssignment(t.Id);
      return t;
    }
    catch { return null; }
  }
  private List<TestFillOuts> listarTestFillOutsPorTestAssignment(int taId)
  {
    try
    {
      return (from x in connection.Table<TestFillOuts>()
              where x.TestAssignmentId == taId
              select x).ToList();
    }
    catch { return null; }
  }
  public List<TestAssignments> ListarTestAssignmentByMedicoNoRespondido(int mId)
  {
    try
    {
      return (from x in connection.Table<TestAssignments>()
              where x.MedicosId == mId && !x.Respondido
              select x).ToList();
    }
    catch { return null; }
  }
  public MedicoPaciente CargarMedicoPaciente(int mpId)
  {
    try
    {
      return (from x in connection.Table<MedicoPaciente>()
              where x.Id == mpId
              select x).FirstOrDefault();
    }
    catch { return null; }
  }
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
        if (!lm.Exists(x => x.PacientesId == p.Id))
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
      Pacientes p = (from x in connection.Table<Pacientes>()
                     where x.Id == pId
                     select x).FirstOrDefault();
      p.TestFillOuts = ListarTestFillOutsPorPaciente(p.Id);
      return p;
    }
    catch { return null; }
  }
  private List<TestFillOuts> ListarTestFillOutsPorPaciente(int pId)
  {
    try
    {
      return (from tf in connection.Table<TestFillOuts>()
              join ta in connection.Table<TestAssignments>() on tf.TestAssignmentId equals ta.Id
              where ta.PacientesId == pId
              select tf).ToList();

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
  public List<Questions> ListarQuestionPorTest(int tId)
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
      Tests t = (from x in connection.Table<Tests>()
                 where x.Id == tId
                 select x).FirstOrDefault();
      if (t != null)
        t.Questions = ListarQuestionPorTest(t.Id);
      return t;
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
      Message = ex.Message;
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
      Message = ex.Message;
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
      Message = ex.Message;
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
    Ruta = AppDomain.CurrentDomain.BaseDirectory;
    
    LoadSettings();
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
      Message = ex.Message;
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
      Message = ex.Message;
    }

    return true;
  }


}
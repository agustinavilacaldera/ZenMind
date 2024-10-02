using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZenMindMAUIBlazor.Models
{
  internal class TestAssignments
  {
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; } = -1;
    public DateTime Date { get; set; } = DateTime.Today;//Fecha en que el paciente debe realizar el test
    [Indexed]
    public int MedicosId { get; set; }
    [Indexed]
    public int TestId { get; set; }
    [Indexed]
    public int PacientesId { get; set; }
    public bool Respondido { get; set; }=false;
    [Ignore]
    public List<TestFillOuts> TestFillOuts { get; set; }
    public float ObtenerCalificacion()
    {
      ResultadoTest rt = new(TestFillOuts);
      return rt.ObtenerPonderado();
    }
  }
}

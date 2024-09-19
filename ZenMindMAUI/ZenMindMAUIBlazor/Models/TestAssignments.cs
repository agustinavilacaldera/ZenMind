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
    public int Id { get; set; }
    public DateTime Date { get; set; }
    [Indexed]
    public int MedicosId { get; set; }
    [Indexed]
    public int PacientesId { get; set; }
  }
}
